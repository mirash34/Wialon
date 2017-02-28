using System;
using System.Linq;
using System.Windows.Forms;

namespace Wialion_Forms
{
    public partial class Options : Form
    {
        
        public Options()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.Options_FormClosing);
            this.UserComboBox.DropDown += new System.EventHandler(this.UserComboBox_DropDown);
            using (DbOption db = new DbOption())
            {                   
                var Options = db.UserOptions.Select(x => new { UserName = x.UserName, Id = x.Id });
                UserComboBox.DataSource = Options.ToList();
                UserComboBox.DisplayMember = "UserName";
                UserComboBox.ValueMember = "Id";
                this.UserComboBox.SelectedIndexChanged += new System.EventHandler(this.UserComboBox_SelectedIndexChanged);
                if (UserComboBox.Text != "")
                {
                    int a = Convert.ToInt32(UserComboBox.SelectedValue.ToString());          
                    var NewOption = db.UserOptions.Where(x => x.Id == a).FirstOrDefault();
                    if (NewOption != null)
                    {
                        LoginTextBox.Text = NewOption.Login;
                        PasswordTextBox.Text = NewOption.Password;
                        ProxyTextBox.Text = NewOption.Proxy;
                        PortTextBox.Text = NewOption.Port.ToString();
                        DirectoryTextBox.Text = NewOption.Directory;
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            using (DbOption db = new DbOption())
            {
                var NewOption = db.UserOptions.Where(x => x.UserName == UserComboBox.Text).FirstOrDefault();
                if (LoginTextBox.Text != "" & PasswordTextBox.Text != "" & ProxyTextBox.Text != "" & PortTextBox.Text != "" & UserComboBox.Text != "" & DirectoryTextBox.Text != "")
                {
                    if (NewOption == null)
                    {
                        db.UserOptions.Add(new UserOptions { Login = LoginTextBox.Text, Password = PasswordTextBox.Text, Port = Convert.ToInt32(PortTextBox.Text), Proxy = ProxyTextBox.Text, UserName = UserComboBox.Text, Directory = DirectoryTextBox.Text });

                        db.SaveChanges();
                    }
                    else
                    {
                        db.UserOptions.Remove(NewOption);
                        db.UserOptions.Add(new UserOptions { Login = LoginTextBox.Text, Password = PasswordTextBox.Text, Port = Convert.ToInt32(PortTextBox.Text), Proxy = ProxyTextBox.Text, UserName = UserComboBox.Text, Directory = DirectoryTextBox.Text });
                        db.SaveChanges();
                    }
                }
                else
                    System.Windows.Forms.MessageBox.Show("Заполните все поля, чтобы отредактировать данные, или добавить нового пользователя");
            }
        }
        private void UserDeleteBtn_Click(object sender, EventArgs e)
        {
           
            using (DbOption db = new DbOption())
            {
                int a = Convert.ToInt32(UserComboBox.SelectedValue.ToString());
                var NewOption = db.UserOptions.Where(x => x.Id == a).FirstOrDefault();
                db.UserOptions.Remove(NewOption);
                db.SaveChanges();
                
                this.UserComboBox.DataSource = db.UserOptions;
                this.UserComboBox.DisplayMember = "UserName";
                this.UserComboBox.ValueMember = "Id";
            }
        }
        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void UserComboBox_DropDown(object sender, EventArgs e)
        {using (DbOption db = new DbOption())
            {           
                var Options = db.UserOptions.Select(x => new { UserName = x.UserName, Id = x.Id });
                UserComboBox.DataSource = Options.ToList();
                UserComboBox.DisplayMember = "UserName";
                UserComboBox.ValueMember = "Id";
            }
        }

        private void UserComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserComboBox.Text != "")
            {
                using (DbOption db = new DbOption())
                {
                    int a = Convert.ToInt32(UserComboBox.SelectedValue.ToString());
                    var NewOption = db.UserOptions.Where(x => x.Id == a).FirstOrDefault();
                    if (NewOption != null)
                    {
                        LoginTextBox.Text = NewOption.Login;
                        PasswordTextBox.Text = NewOption.Password;
                        ProxyTextBox.Text = NewOption.Proxy;
                        PortTextBox.Text = NewOption.Port.ToString();
                        DirectoryTextBox.Text = NewOption.Directory;
                    }
                }
            }
        }        

        private void button1_Click(object sender, EventArgs e)
        {            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DirectoryTextBox.Text = folderBrowserDialog1.SelectedPath;
            }

        }
    }
}
