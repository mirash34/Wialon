using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace Wialion_Forms
{
    public partial class Options : Form
    {
        DataContext db = new DataContext(ConfigurationManager.ConnectionStrings["DBGroup"].ConnectionString);
        public Options()
        {
            InitializeComponent();
            /*AppDomain domain = AppDomain.CurrentDomain;
            string XmlAdress = domain.BaseDirectory.ToString() + "OptionsInformation.xml";

            try { XmlDocument XDoc = new XmlDocument(); XDoc.Load(XmlAdress); }
            catch
            {
                XDocument XmlDoc = new XDocument
           (new XElement("Options",
            new XElement("Login", "А.Морозов"),
            new XElement("Password", "sucden"),
            new XElement("UserName", "А.Морозов"),

            new XElement("Proxy", "activex-wln.sucden.ru"),
            new XElement("Port", "80")
            
                   )) ;


                XmlDoc.Save(XmlAdress);
            }*/

            this.FormClosing += new FormClosingEventHandler(this.Options_FormClosing);
            this.UserComboBox.DropDown += new System.EventHandler(this.UserComboBox_DropDown);
            Table<OptionsForm> OptionsD = db.GetTable<OptionsForm>();
            // List<GroupData> GroupD = new List<GroupData>();
            var Options = OptionsD.Select(x => new { UserName = x.UserName, Id = x.Id });

            UserComboBox.DataSource = Options.ToList();
            UserComboBox.DisplayMember = "UserName";
            UserComboBox.ValueMember = "Id";
            this.UserComboBox.SelectedIndexChanged += new System.EventHandler(this.UserComboBox_SelectedIndexChanged);

            if (UserComboBox.Text != "")
            {
                Table<OptionsForm> OptionsD2 = db.GetTable<OptionsForm>();
                // List<GroupData> GroupD = new List<GroupData>();
                var NewOption = OptionsD2.Where(x => x.Id == Convert.ToInt32(UserComboBox.SelectedValue.ToString())).FirstOrDefault();
                if (NewOption != null)
                {
                    LoginTextBox.Text = NewOption.Login;
                    PasswordTextBox.Text = NewOption.Password;
                    ProxyTextBox.Text = NewOption.Proxy;
                    PortTextBox.Text = NewOption.Port.ToString();
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Table<OptionsForm> OptionsD = db.GetTable<OptionsForm>();
            // var Options = OptionsD.Select(x => new { Login = x.Login, Password = x.Password, Proxy = x.Proxy, Port = x.Port, UserName = x.UserName, Id = x.Id });

            //UserComboBox.DataSource = Options.ToList();
            //UserComboBox.DisplayMember = "UserName";
            //UserComboBox.ValueMember = "Id";
            // System.Windows.Forms.MessageBox.Show(UserComboBox.Text);
            var NewOption = OptionsD.Where(x => x.UserName == UserComboBox.Text).FirstOrDefault();
            if (LoginTextBox.Text != "" & PasswordTextBox.Text != "" & ProxyTextBox.Text != "" & PortTextBox.Text != "" & UserComboBox.Text != "")
            {

                if (NewOption == null)
                {
                    OptionsD.InsertOnSubmit(new OptionsForm { Login = LoginTextBox.Text, Password = PasswordTextBox.Text, Port = Convert.ToInt32(PortTextBox.Text), Proxy = ProxyTextBox.Text, UserName = UserComboBox.Text });
                    db.SubmitChanges();
                }
                else
                {
                    OptionsD.DeleteOnSubmit(NewOption);
                    OptionsD.InsertOnSubmit(new OptionsForm { Login = LoginTextBox.Text, Password = PasswordTextBox.Text, Port = Convert.ToInt32(PortTextBox.Text), Proxy = ProxyTextBox.Text, UserName = UserComboBox.Text });
                    db.SubmitChanges();

                }

            }
            else
                System.Windows.Forms.MessageBox.Show("Заполните все поля, чтобы отредактировать данные, или добавить нового пользователя");

        }

        private void UserDeleteBtn_Click(object sender, EventArgs e)
        {
            Table<OptionsForm> OptionsD = db.GetTable<OptionsForm>();
            var NewOption = OptionsD.Where(x => x.Id == Convert.ToInt32(UserComboBox.SelectedValue.ToString())).FirstOrDefault();
            OptionsD.DeleteOnSubmit(NewOption);
            db.SubmitChanges();
        }
        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void UserComboBox_DropDown(object sender, EventArgs e)
        {
            Table<OptionsForm> OptionsD = db.GetTable<OptionsForm>();
            // List<GroupData> GroupD = new List<GroupData>();
            var Options = OptionsD.Select(x => new { UserName = x.UserName, Id = x.Id });

            UserComboBox.DataSource = Options.ToList();
            UserComboBox.DisplayMember = "UserName";
            UserComboBox.ValueMember = "Id";
        }

        private void UserComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserComboBox.Text != "")
            {
                Table<OptionsForm> OptionsD = db.GetTable<OptionsForm>();
                // List<GroupData> GroupD = new List<GroupData>();
                var NewOption = OptionsD.Where(x => x.Id == Convert.ToInt32(UserComboBox.SelectedValue.ToString())).FirstOrDefault();
                if (NewOption != null)
                {
                    LoginTextBox.Text = NewOption.Login;
                    PasswordTextBox.Text = NewOption.Password;
                    ProxyTextBox.Text = NewOption.Proxy;
                    PortTextBox.Text = NewOption.Port.ToString();
                }
            }
        }

       
    }



    [Table(Name = "UserOption")]
    public class OptionsForm
    {
        [Column(Name = "Login")]
        public string Login { get; set; }
        [Column(Name = "Password")]
        public string Password { get; set; }
        [Column(Name = "Proxy")]
        public string Proxy { get; set; }
        [Column(Name = "Port")]
        public int Port { get; set; }
        [Column(Name = "UserName")]
        public string UserName { get; set; }
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Id")]
        public int Id { get; set; }

    }
}
