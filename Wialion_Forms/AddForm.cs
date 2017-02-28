using System;
using System.Linq;
using System.Windows.Forms;


namespace Wialion_Forms
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.Add_FormClosing);
            using (DbOption db = new DbOption())
            {
                this.ComboGroup.DataSource = db.Groups.ToList();
                this.ComboGroup.DisplayMember = "Name";
                this.ComboGroup.ValueMember = "GroupId";
            }
        }

        private void AddOk_Click(object sender, EventArgs e)
        {

            using (DbOption db = new DbOption())
            {
                if (ComboGroup.Text != null)
                {
                    db.Groups.Add(new Groups { Name = ComboGroup.Text });
                    try { db.SaveChanges(); }
                    catch { System.Windows.Forms.MessageBox.Show("Введите другое название группы. Это не соответсвует формату данных"); }
                    this.ComboGroup.DataSource = db.Groups.ToList();
                    this.ComboGroup.DisplayMember = "Name";
                    this.ComboGroup.ValueMember = "GroupId";
                }
            }
        }

        private void Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {           
            using (DbOption db = new DbOption())
            {
                if (ComboGroup.Text != null)
                {
                    int a = Convert.ToInt32(ComboGroup.SelectedValue.ToString());
                    var group = db.Groups.OrderByDescending(u => u.Name == ComboGroup.Text & u.GroupId == a).First();
                    db.Groups.Remove(group);
                    var units = db.Units.Where(x => x.GroupId == a);
                    db.Units.RemoveRange(units);
                    db.SaveChanges();
                    this.ComboGroup.DataSource = db.Groups.ToList();
                    this.ComboGroup.DisplayMember = "Name";
                    this.ComboGroup.ValueMember = "GroupId";
                }
            }
        }

    }
}
