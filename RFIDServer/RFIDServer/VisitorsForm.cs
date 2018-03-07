using System;
using System.Data;
using System.Data.SQLite;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace RFIDServer
{
    public partial class VisitorsForm : Form
    {
        private SQLiteConnection conn;
        private SerialPort port;
        private readonly SynchronizationContext synchronizationContext;

        public VisitorsForm(SQLiteConnection connection, SerialPort serialPort)
        {
            InitializeComponent();
            conn = connection;
            port = serialPort;
            DBThreadInitiator();
            COMThreadInitiator();
            synchronizationContext = SynchronizationContext.Current;
        }

        private void button_access_settings_Click(object sender, EventArgs e)
        {
            AccessSettingsForm accessSettingsForm = new AccessSettingsForm(conn);
            accessSettingsForm.ShowDialog();
            //loadVisitors();
            //VisitorsForm_Load(sender, EventArgs.Empty);
        }

        private void VisitorsForm_Load(object sender, EventArgs e)
        {
            synchronizationContext.Send(new SendOrPostCallback(o =>
            {
                loadVisitors();
            }), 0);
        }

        private void DBThreadInitiator()
        {
           // await Task.Delay(1); //Delay for async initialization
            try
            {
                conn.Open();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Произошла ошибка при подключении к базе данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel_dbStatus.Text = "Ошибка подключения к базе данных";
                return;
            }

            if (conn.State == ConnectionState.Open)
            {
                toolStripStatusLabel_dbStatus.Text = "База данных доступна";
                SQLiteCommand cardsCommand = conn.CreateCommand();
                cardsCommand.CommandText = "CREATE TABLE IF NOT EXISTS cards(" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "card_serial TEXT, " +
                    "owner TEXT, " +
                    "access_status INTEGER DEFAULT 0, " + //0 - forbidden, 1 - allowed
                    "CONSTRAINT unique_card_serial UNIQUE (card_serial)" +
                    ");";
                try
                {
                    cardsCommand.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при создании таблицы cards: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SQLiteCommand visitorsCommand = conn.CreateCommand();
                visitorsCommand.CommandText = "CREATE TABLE IF NOT EXISTS visitors(" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "card_id INTEGER NOT NULL, " +
                    "datetime INTEGER, " + //UNIX time
                    "access_status_hist INTEGER DEFAULT 0, " +  //0 - forbidden, 1 - allowed. Copied from cards table for making history
                    "CONSTRAINT fk_card_id FOREIGN KEY(card_id) REFERENCES cards(id)" + 
                    ");";
                try
                {
                    visitorsCommand.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при создании таблицы visitors: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Нет соединения с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void COMThreadInitiator()
        {
           // await Task.Delay(1); //Delay for async initialization
            port.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceivedHandler);
            port.Open();
            
            toolStripStatusLabel_comStatus.Text = "COM-порт доступен";
            //TODO: close COM port
        }

        //TODO: is async?
        private void SerialPort_DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string comData = ((SerialPort)sender).ReadExisting();

            if (conn.State == ConnectionState.Open)
            {
                SQLiteCommand checkCardCommand = conn.CreateCommand();
                checkCardCommand.CommandText = "SELECT id FROM cards WHERE card_serial = '" + comData + "';";
                object checkCardCommandResult = null;
                try
                {
                    checkCardCommandResult = checkCardCommand.ExecuteScalar();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при получении карты из таблицы cards: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Add new card if no results found
                if(checkCardCommandResult == null)
                {
                    SQLiteCommand addNewCardCommand = conn.CreateCommand();
                    addNewCardCommand.CommandText = "INSERT INTO cards VALUES(NULL, '" + comData + "', NULL, 0);";
                    try
                    {
                        addNewCardCommand.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Произошла ошибка при добавлении новой карты: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //Get card once again
                SQLiteCommand checkCardCommand_status = conn.CreateCommand();
                checkCardCommand_status.CommandText = "SELECT access_status FROM cards WHERE card_serial = '" + comData + "';";
                object checkCardCommandStatusResult = null;
                try
                {
                    checkCardCommandResult = checkCardCommand.ExecuteScalar();
                    checkCardCommandStatusResult = checkCardCommand_status.ExecuteScalar();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при повторном получении карты из таблицы cards: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Add entry to visitors table
                if(checkCardCommandResult != null && checkCardCommandStatusResult != null)
                {
                    SQLiteCommand addNewVisitorCommand = conn.CreateCommand();
                    addNewVisitorCommand.CommandText = "INSERT INTO visitors VALUES(NULL, '" + 
                        Convert.ToInt32(checkCardCommandResult) + "', '" + ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() + "', '" + Convert.ToInt32(checkCardCommandStatusResult) + "');";
                    try
                    {
                        addNewVisitorCommand.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Произошла ошибка при добавлении новой записи в таблицу visitors: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось добавить новую карту", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Нет соединения с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            VisitorsForm_Load(sender, EventArgs.Empty);
            //loadVisitors();
        }

        private void loadVisitors()
        {
            //await Task.Delay(1); //Delay for async initialization
            dataGridView_visitors.Enabled = true;
            button_access_settings.Enabled = true;
            
            dataGridView_visitors.Rows.Clear();

            if (conn.State == ConnectionState.Open)
            {
                SQLiteCommand cardsCommand = conn.CreateCommand();
                cardsCommand.CommandText = "SELECT v.id, c.card_serial, c.owner, v.datetime, v.access_status_hist FROM visitors v, cards c WHERE c.id = v.card_id;";
                try
                {
                    using (SQLiteDataReader reader = cardsCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView_visitors.Rows.Add(new object[] {
                                reader.GetValue(reader.GetOrdinal("id")),
                                reader.GetValue(reader.GetOrdinal("card_serial")),
                                reader.GetValue(reader.GetOrdinal("owner")),
                                new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc).AddSeconds(Convert.ToInt64(reader.GetValue(reader.GetOrdinal("datetime")))).AddHours(7),
                                Convert.ToInt32(reader.GetValue(reader.GetOrdinal("access_status_hist"))) == 0 ? "Запрещено" : "Разрешено" //TODO: color selection?
                            });
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при чтении списка посетителей: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Нет соединения с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //TODO: check event
        private void VisitorsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Dispose();
        }
    }
}
