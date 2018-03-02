using System;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFIDServer
{
    public partial class AccessSettingsForm : Form
    {
        private SQLiteConnection conn;

        public AccessSettingsForm(SQLiteConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }

        private void AccessSettingsForm_Load(object sender, EventArgs e)
        {
            loadCards();
        }

        private async void loadCards()
        {
            await Task.Delay(1);
            dataGridView_cards.Rows.Clear();
            if (conn.State == ConnectionState.Open)
            {
                SQLiteCommand cardsCommand = conn.CreateCommand();
                cardsCommand.CommandText = "SELECT * FROM cards";
                try
                {
                    using (SQLiteDataReader reader = cardsCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView_cards.Rows.Add(new object[] {
                                reader.GetValue(reader.GetOrdinal("id")),
                                reader.GetValue(reader.GetOrdinal("card_serial")),
                                reader.GetValue(reader.GetOrdinal("owner")),
                                Convert.ToInt32(reader.GetValue(reader.GetOrdinal("access_status"))) == 0 ? "Запрещено" : "Разрешено" //TODO: color selection?
                            });
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при чтении таблицы cards: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Нет соединения с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_add_card_Click(object sender, EventArgs e)
        {
            AddCardForm addCardForm = new AddCardForm(conn);
            DialogResult dialogResult = addCardForm.ShowDialog();
            if(dialogResult != DialogResult.Cancel)
            {
                loadCards();
            }
        }
    }
}
