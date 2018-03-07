using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFIDServer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(
                new VisitorsForm(
                    new SQLiteConnection("Data Source=access.db; Version=3; Foreign Keys=True;"),
                    new SerialPort("COM4", 9600)
                )
            );
        }
    }
}
