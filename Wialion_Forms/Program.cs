using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WialonActiveXLib;
using System.Xml;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace Wialion_Forms
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

            Application.Run(new Form1());


            // Главный компонент системы, обеспечивающий подключение к Wialon

        }
    }
}