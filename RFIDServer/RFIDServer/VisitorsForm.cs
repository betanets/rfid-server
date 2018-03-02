using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFIDServer
{
    public partial class VisitorsForm : Form
    {
        public VisitorsForm()
        {
            InitializeComponent();
        }

        private void button_access_settings_Click(object sender, EventArgs e)
        {

        }

        private void VisitorsForm_Load(object sender, EventArgs e)
        {
            //Thread dbThread = new Thread(DBThreadInitiator);
            //dbThread.Start();
            DBThreadInitiator();
        }

        private async void DBThreadInitiator()
        {
            await Task.Delay(1); //Delay for async initialization
            SQLiteConnection conn = new SQLiteConnection("Data Source=access.db; Version=3; Foreign Keys=True;");
            try
            {
                conn.Open();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Произошла ошибка при подключении к базе данных: " + ex.Message);
                toolStripStatusLabel_dbStatus.Text = "Ошибка подключения к базе данных";
            }

            if (conn.State == ConnectionState.Open)
            {
                toolStripStatusLabel_dbStatus.Text = "База данных доступна";
                SQLiteCommand cardsCommand = conn.CreateCommand();
                cardsCommand.CommandText = "CREATE TABLE IF NOT EXISTS cards(" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "card_serial TEXT, " +
                    "owner TEXT, " +
                    "access_status INTEGER DEFAULT 0" + //0 - forbidden, 1 - allowed
                    ");";
                try
                {
                    cardsCommand.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при создании таблицы cards: " + ex.Message);
                }

                SQLiteCommand visitorsCommand = conn.CreateCommand();
                visitorsCommand.CommandText = "CREATE TABLE IF NOT EXISTS visitors(" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "card_id INTEGER NOT NULL, " +
                    "datetime INTEGER, " + //UNIX time
                    "CONSTRAINT fk_card_id FOREIGN KEY(card_id) REFERENCES cards(id)" + 
                    ");";
                try
                {
                    visitorsCommand.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Произошла ошибка при создании таблицы visitors: " + ex.Message);
                }
            }

            conn.Dispose();
        }
    }
}
