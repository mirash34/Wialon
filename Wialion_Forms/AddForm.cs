using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WialonActiveXLib;
using System.Xml;
using System.IO;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Wialion_Forms
{
    public partial class AddForm : Form
    {
        DataContext db;
       // string ConnectionString;
        public AddForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.Add_FormClosing);
            db = new DataContext(ConfigurationManager.ConnectionStrings["DBGroup"].ConnectionString);
            Table<GroupData> GroupD = db.GetTable<GroupData>();      
            this.ComboGroup.DataSource = GroupD.ToList<GroupData>();          
            this.ComboGroup.DisplayMember = "GroupName";
            this.ComboGroup.ValueMember = "GroupId";
            //List<GroupData> GroupList = new List<GroupData>();
           // GroupList = GroupD.Select(x => new List<GroupData> { x.GroupName, x.GroupId });
           // var query = GroupD.OrderBy(u => u.GroupName);
           // List<GroupData> SomeGroup = new List<GroupData>();
          //  SomeGroup = (List<GroupData>)GroupD.OrderBy(u => u.GroupName);
           // ComboGroup.DataSource = query.ToList<GroupData>();            
          //  foreach (var a in query)
           // { System.Windows.Forms.MessageBox.Show(a.GroupName); }
           // this.ComboGroup.DisplayMember = "Name";
            //this.ComboGroup.ValueMember = "GroupId";
        
        }

        private void AddOk_Click(object sender, EventArgs e)
        {
            

            //Table<UnitsData> UnitsD = db.GetTable<UnitsData>();
            Table<GroupData> GroupD = db.GetTable<GroupData>();
            if (ComboGroup.Text != null)
            { GroupD.InsertOnSubmit(new GroupData { GroupName = ComboGroup.Text });
            try { db.SubmitChanges(); }
            catch { System.Windows.Forms.MessageBox.Show("Введите другое название группы. Это не соответсвует формату данных"); }
            this.ComboGroup.DataSource = GroupD.ToList<GroupData>();
            this.ComboGroup.DisplayMember = "GroupName";
            this.ComboGroup.ValueMember = "GroupId";


            }
            

        }

       
        private void Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            Table<GroupData> GroupD = db.GetTable<GroupData>();
            Table<UnitsData> UnitD = db.GetTable<UnitsData>();
            if (ComboGroup.Text != null)
            {
                var group = GroupD.OrderByDescending(u => u.GroupName == ComboGroup.Text & u.GroupId == Convert.ToInt32(ComboGroup.SelectedValue.ToString())).First();
                GroupD.DeleteOnSubmit(group);
                var units = UnitD.Where(x => x.GroupId == Convert.ToInt32(ComboGroup.SelectedValue.ToString()));
                UnitD.DeleteAllOnSubmit(units);             
                db.SubmitChanges();

                this.ComboGroup.DataSource = GroupD.ToList<GroupData>();
                this.ComboGroup.DisplayMember = "GroupName";
                this.ComboGroup.ValueMember = "GroupId";
            }
        }     

        //private void ComboGroup_SelectedIndexChanged(object sender, EventArgs e)
       //{
           
            //ComboGroup.Items = GroupD.OrderBy(u => u.GroupName);
         //  GroupData Dat = (GroupData)ComboGroup.SelectedItem;
           //listBox1.Items.Add(phone);

        //}
    }
}
