using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
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
            AddCardForm addCardForm = new AddCardForm(conn, -1);
            DialogResult dialogResult = addCardForm.ShowDialog();
            if(dialogResult != DialogResult.Cancel)
            {
                loadCards();
            }
        }

        private void dataGridView_cards_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var mouseOverRow = dataGridView_cards.HitTest(e.X, e.Y).RowIndex;

                if (mouseOverRow >= 0)
                {
                    ContextMenu menu = new ContextMenu();
                    MenuItem editMenuItem = new MenuItem("Редактировать карту");
                    editMenuItem.Click += new EventHandler(editMenuItem_Click);
                    menu.MenuItems.Add(editMenuItem);

                    MenuItem deleteMenuItem = new MenuItem("Удалить карту");
                    deleteMenuItem.Click += new EventHandler(deleteMenuItem_Click);
                    menu.MenuItems.Add(deleteMenuItem);

                    dataGridView_cards.Rows[mouseOverRow].Selected = true;
                    menu.Show(dataGridView_cards, new Point(e.X, e.Y));
                }
            }
        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            AddCardForm addCardForm = new AddCardForm(conn, Convert.ToInt32(dataGridView_cards.SelectedRows[0].Cells["id"].Value));
            DialogResult dialogResult = addCardForm.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                loadCards();
            }
        }

        //TODO: async task?
        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить эту карту?\nВсе события, связанные с этой картой, будут удалены", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SQLiteCommand deleteVisitors = conn.CreateCommand();
                deleteVisitors.CommandText = "DELETE FROM visitors WHERE card_id = '" + dataGridView_cards.SelectedRows[0].Cells["id"].Value + "';";
                try
                {
                    deleteVisitors.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при удалении из таблицы visitors: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SQLiteCommand deleteCard = conn.CreateCommand();
                deleteCard.CommandText = "DELETE FROM cards WHERE id = '" + dataGridView_cards.SelectedRows[0].Cells["id"].Value + "';";
                try
                {
                    deleteCard.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при удалении из таблицы cards: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                loadCards();
            }
        }
    }
}
