using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace RFIDServer
{
    public partial class AddCardForm : Form
    {
        private static SQLiteConnection conn;
        private static Int32 cardId;

        public AddCardForm(SQLiteConnection connection, Int32 cardIdToEdit)
        {
            InitializeComponent();
            conn = connection;
            cardId = cardIdToEdit;
        }

        //TODO: async task?
        private void AddCardForm_Load(object sender, EventArgs e)
        {
            if (cardId == -1)
            {
                comboBox_access_status.SelectedIndex = 0;
            }
            else
            {
                textBox_serial_number.Enabled = false;

                if(conn.State == ConnectionState.Open)
                {
                    SQLiteCommand getCardCommand = conn.CreateCommand();
                    getCardCommand.CommandText = "SELECT card_serial, owner, access_status FROM cards where id = '" + cardId + "';";
                    try
                    {
                        using (SQLiteDataReader reader = getCardCommand.ExecuteReader())
                        {
                            reader.Read();
                            textBox_serial_number.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("card_serial")));
                            textBox_owner.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("owner")));
                            comboBox_access_status.SelectedIndex = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("access_status")));
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Произошла ошибка при получении записи из таблицы cards: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Нет соединения с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                if (textBox_serial_number.Text.Length <= 0)
                {
                    MessageBox.Show("Необходимо указать серийный номер", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SQLiteCommand checkCardCommand = conn.CreateCommand();
                    checkCardCommand.CommandText = "SELECT count(id) FROM cards where card_serial = '" + textBox_serial_number.Text + "';";
                    Int32 existingCardCount;
                    try
                    {
                        existingCardCount = Convert.ToInt32(checkCardCommand.ExecuteScalar());
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Произошла ошибка при проверке серийного номера карты: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (cardId == -1)
                    {
                        if (existingCardCount == 0)
                        {
                            SQLiteCommand addCardCommand = conn.CreateCommand();
                            addCardCommand.CommandText = "INSERT INTO cards VALUES(" +
                                "NULL, '" + textBox_serial_number.Text + "', '" + textBox_owner.Text + "', '" + comboBox_access_status.SelectedIndex + "');";
                            try
                            {
                                addCardCommand.ExecuteNonQuery();
                            }
                            catch (SQLiteException ex)
                            {
                                MessageBox.Show("Произошла ошибка при добавлении новой карты: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Карта с таким серийным номером уже находится в базе данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        SQLiteCommand editCardCommand = conn.CreateCommand();
                        editCardCommand.CommandText = "UPDATE cards SET owner = '" + textBox_owner.Text + 
                            "', access_status = '" + comboBox_access_status.SelectedIndex + "' WHERE id = '" + cardId + "';";
                        try
                        {
                            editCardCommand.ExecuteNonQuery();
                        }
                        catch (SQLiteException ex)
                        {
                            MessageBox.Show("Произошла ошибка при редактировании карты: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    DialogResult = DialogResult.OK;
                    Close(); //Form close
                }
            }
            else
            {
                MessageBox.Show("Нет соединения с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
