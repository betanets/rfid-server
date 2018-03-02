using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace RFIDServer
{
    public partial class AddCardForm : Form
    {
        private SQLiteConnection conn;

        public AddCardForm(SQLiteConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }

        private void AddCardForm_Load(object sender, EventArgs e)
        {
            comboBox_access_status.SelectedIndex = 0;
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

                    if(existingCardCount == 0)
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
                        DialogResult = DialogResult.OK;
                        Close(); //Form close
                    }
                    else
                    {
                        MessageBox.Show("Карта с таким серийным номером уже находится в базе данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет соединения с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
