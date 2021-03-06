﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WialonActiveXLib;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;


namespace Wialion_Forms
{
    public partial class WialonForm : Form
    {
        public WialonCollection Units;
        public WialonConnection Wialon;
        public WialonCollection Reports;
        AddForm AddForm1;
        Options Option;
        public WialonForm()
        {
            TimeZone localZone = TimeZone.CurrentTimeZone;            
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;
            DateTime currentUTC =localZone.ToUniversalTime(currentDate);
            TimeSpan currentOffset =localZone.GetUtcOffset(currentDate);          
            AddForm1 = new AddForm();
            Option = new Options();
            InitializeComponent();
          
            using (DbOption db = new DbOption())
            {
                ComboBoxGroup.DataSource = db.Groups.ToList();
                ComboBoxGroup.DisplayMember = "Name";
                ComboBoxGroup.ValueMember = "GroupId";
            }
            Wialon = new WialonConnection();
            Units = new WialonCollection();
            Reports = new WialonCollection();
            WialonUnit Unit = new WialonUnit();
            WialonReport Report = new WialonReport();
            long CountOfUnits;
            if (Wialon == null)
            {
                MessageBox.Show("No object");
                return;
            }           
            if (Option.ProxyTextBox.Text.Trim() != "" & Option.PortTextBox.Text.Trim() != "" & Option.LoginTextBox.Text.Trim() != "" & Option.PasswordTextBox.Text.Trim() != "" & Option.DirectoryTextBox.Text.Trim() != "")
            {
                Wialon.SetProxyMode(Option.ProxyTextBox.Text.Trim(), (ushort)Convert.ToInt32(Option.PortTextBox.Text.Trim()), Option.LoginTextBox.Text.Trim() + ":" + Option.PasswordTextBox.Text.Trim());
                Units = Wialon.Login(Option.ProxyTextBox.Text.Trim(), (ushort)Convert.ToInt32(Option.PortTextBox.Text.Trim()), Option.LoginTextBox.Text.Trim(), Option.PasswordTextBox.Text.Trim());
            }
            else
            {
             Wialon.SetProxyMode("activex-wln.sucden.ru", 80, "UserForApp:UserForApp");
             Units = Wialon.Login("activex-wln.sucden.ru", 80, "UserForApp", "UserForApp");               
            }
            Reports = Wialon.GetReportsList();
            WialonReport NewRep = new WialonReport();
            List<ReportL> RepList = new List<ReportL>();
            foreach (WialonReport a in Reports)
            {
                if (a == null)
                {
                    MessageBox.Show(Wialon.GetLastError().ToString());
                    return;
                }
                else
                    RepList.Add(new ReportL() { Name = a.Name, ReportID = a.ReportID, ResourceID = a.ResourceID });
            }
            RepList.Sort((a, b) => a.Name.CompareTo(b.Name));
            ReportList.DataSource = RepList;
            ReportList.DisplayMember = "Name";
            ReportList.ValueMember = "ReportID";
            CountOfUnits = Units.Count;
            List<Units> UnitsCl = new List<Units>();
            for (int i = 1; i < CountOfUnits; i++)
            {
                Unit = (WialonUnit)Units[i];
                if (Unit == null)
                {
                    MessageBox.Show("Not unit");
                    return;
                }
                else
                {
                    UnitsCl.Add(new Units() { Name = Unit.Name, UnitId = (int)Unit.ID });
                }
            }
            UnitsCl.Sort((a, b) => a.Name.CompareTo(b.Name));
            UnitsWialon.DataSource = UnitsCl;
            UnitsWialon.DisplayMember = "Name";
            UnitsWialon.ValueMember = "UnitId";
            //  string some = Wialon.GetReportByID(1482094800, 1482181199, UId, (int)GetTimeZoneForWialon(), "ru", ResId, RepId);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Unit);
            Unit = null;
            // StreamWriter SW = new StreamWriter(new FileStream(@"C:\Rapport\MyXml.txt", FileMode.Create, FileAccess.Write));
            // SW.Write(some);
            // SW.Close();
            //  StreamWriter SW2 = new StreamWriter(new FileStream(@"C:\Rapport\MyXml2.txt", FileMode.Create, FileAccess.Write));
            //  if (some != null)
            //  {
            //      SW2.Write(some);
            //     SW2.Close();
            // }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Units);
            Units = null;
            Wialon = null;
            ComboBoxGroup.SelectedIndexChanged += ComboBoxGroup_SelectedIndexChanged;
        }
        void ComboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {   int ComboGroupS=(int)ComboBoxGroup.SelectedValue;     
            
                  using (DbOption db = new DbOption())
                  {
                      if (ComboGroupS != 0)
                      {
                          CheckedUnits.DataSource = db.Units.ToList().Where(x => x.GroupId == ComboGroupS).ToList();
                          CheckedUnits.DisplayMember = "Name";
                          CheckedUnits.ValueMember = "UnitId";
                      }
                  }
              
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Units> NewUnits = new List<Units>();
            foreach (Units a in CheckedUnits.Items)
            { NewUnits.Add(a); }
            if (ComboBoxGroup.SelectedValue != null & ComboBoxGroup.SelectedValue.ToString() != "")
            {
                foreach (Units Check in UnitsWialon.CheckedItems)
                {
                    bool flag = false;
                    foreach (Units i in CheckedUnits.Items)
                    {
                        if (Check.Name == i.Name)
                        { flag = true; break; }
                    }
                    if (!flag)
                    {
                        NewUnits.Add(new Units() { Name = Check.Name, UnitId = Check.UnitId, GroupId = Convert.ToInt32(ComboBoxGroup.SelectedValue.ToString()) });
                    }
                }
                CheckedUnits.DataSource = NewUnits;
                CheckedUnits.DisplayMember = "Name";
                CheckedUnits.ValueMember = "UnitId";
                using (DbOption db = new DbOption())
                {
                    if (NewUnits.Count != 0)
                        foreach (Units a in NewUnits)
                        {
                            try
                            {
                                db.Units.Add(new Units { Name = a.Name, UnitId = a.UnitId, GroupId = (int)ComboBoxGroup.SelectedValue });
                                db.SaveChanges();
                            }
                            catch { MessageBox.Show("Не удалось добавить технику " + a.Name); }
                        }
                    else
                        MessageBox.Show("Вы не выбрали технику");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<Units> SaveUnit = new List<Units>();
            List<Units> RemoveUnit = new List<Units>();
            for (int i = CheckedUnits.Items.Count - 1; i >= 0; i--)
            {
                if (!CheckedUnits.GetItemChecked(i))
                    SaveUnit.Add((Units)CheckedUnits.Items[i]);
                else
                    RemoveUnit.Add((Units)CheckedUnits.Items[i]);
            }
            SaveUnit.Reverse();
            CheckedUnits.DataSource = SaveUnit;
            CheckedUnits.DisplayMember = "Name";
            CheckedUnits.ValueMember = "UnitId";
            using (DbOption db = new DbOption())
            {
                foreach (Units a in RemoveUnit)
                {
                    var unit = db.Units.Where(u => u.Name == a.Name & u.UnitId == a.UnitId & u.GroupId == a.GroupId).First();
                    db.Units.Remove(unit);
                    db.SaveChanges();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportBtn.Enabled =false;            
            DateTime From = PickerFrom.Value;
            DateTime To = PickerTo.Value; ;
            DateTime TimeFrom1970 = new DateTime(1970, 1, 1);
            TimeSpan DiffTo = To - TimeFrom1970;
            TimeSpan DiffFrom = From - TimeFrom1970;
            int FromTo = (To - From).Seconds;
            WialonConnection Wialon = new WialonConnection();
            WialonCollection Units = new WialonCollection();
            WialonCollection Reports = new WialonCollection();
            WialonUnit Unit = new WialonUnit();      
            Wialon.SetProxyMode(Option.ProxyTextBox.Text.Trim(), (ushort)Convert.ToInt32(Option.PortTextBox.Text.Trim()), Option.LoginTextBox.Text.Trim() + ":" + Option.PasswordTextBox.Text.Trim());
            Units = Wialon.Login(Option.ProxyTextBox.Text.Trim(), (ushort)Convert.ToInt32(Option.PortTextBox.Text.Trim()), Option.LoginTextBox.Text.Trim(), Option.PasswordTextBox.Text.Trim());
            Reports = Wialon.GetReportsList();
            long IdRep = 0;
            long IdRes = 0;
            foreach (WialonReport a in Reports)
            {
                if (a.Name == ReportList.Text & a.ReportID == long.Parse(ReportList.SelectedValue.ToString()))
                {
                    IdRep = a.ReportID;
                    IdRes = a.ResourceID;
                }
            }
            List<Merge> NewMerge = new List<Merge>();
            List<AZS> NewAZS = new List<AZS>();
            TimeZone localZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = localZone.GetUtcOffset(DateTime.Now);
            foreach (Units a in CheckedUnits.Items)
            {
                //string some2 = Wialon.GetReportByID((uint)DiffFrom.TotalSeconds, (uint)DiffTo.TotalSeconds, a.UnitId, 10800, "ru", IdRes, IdRep);
                //string some = Wialon.GetReportByID((uint)DiffFrom.TotalSeconds, (uint)DiffTo.TotalSeconds, a.UnitId, (int)GetTimeZoneForWialon(), "ru", IdRes, IdRep);
                string XmlReport = Wialon.GetReportByID((uint)DiffFrom.TotalSeconds, (uint)DiffTo.TotalSeconds, a.UnitId, (int)currentOffset.TotalSeconds, "ru", IdRes, IdRep);
                // StreamWriter SW = new StreamWriter(new FileStream(@"C:\Rapport\MyXml.txt", FileMode.Create, FileAccess.Write));
                //SW.Write(some);
                // SW.Close();
                XmlDocument xDoc = new XmlDocument();
                if (XmlReport != null & ReportList.Text == "График топлива и поездки CON2")
                {
                    xDoc.LoadXml(XmlReport);
                    XmlElement xRoot = xDoc.DocumentElement;
                    string XPh = "//table[@name='Сливы']/header/*";
                    string XPh2 = "//table[@name='Сливы']/total/*";
                    string XPh3 = "//table[@name='Сливы']/row/*";
                   // string XPh4 = "//table[@name='Поездки']/row/subrows/row/*";
                    // string SheremetXPh = "//table[@name='Заправки']/row/subrows/row/*";
                    XmlNodeList childnodes = xRoot.SelectNodes(XPh);
                    XmlNodeList childnodes2 = xRoot.SelectNodes(XPh2);
                    try
                    {
                        XmlNodeList childnodes3 = xRoot.SelectNodes(XPh3);
                        int j = 0;
                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ".";
                        Merge AddMerge = new Merge();
                       
                        foreach (XmlNode n in childnodes3)
                        {       // System.Windows.Forms.MessageBox.Show(n.Attributes.Item(0).Value.ToString());
                            if (j == 0)
                            {                                
                                AddMerge.Time = Int32.Parse(n.Attributes.Item(1).Value.ToString());
                                j++; continue;
                            }
                            if (j == 1)
                            {                               
                                AddMerge.Merged = Double.Parse(n.Attributes.Item(1).Value, provider);
                                j++; continue;
                            }
                            if (j == 2)
                            {                                
                                AddMerge.FirstLevel = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 3)
                            {                             
                                AddMerge.SecondLevel = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 4)
                            {                              
                                AddMerge.Driver = n.Attributes.Item(0).Value.ToString();
                                j++; continue;
                            }
                            if (j == 5)
                            {                         
                                AddMerge.FirstSpeed = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 6)
                            {                                
                                AddMerge.FinalSpeed = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 7)
                            {                               
                                AddMerge.Mileage = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j = 0;
                                NewMerge.Add(new Merge() { Driver = AddMerge.Driver, FirstLevel = AddMerge.FirstLevel, Merged = AddMerge.Merged, SecondLevel = AddMerge.SecondLevel, Time = AddMerge.Time, FinalSpeed = AddMerge.FinalSpeed, FirstSpeed = AddMerge.FirstSpeed, Mileage = AddMerge.Mileage, UnitName = a.Name });
                                continue;
                            }
                        }
                    }
                    catch { continue; }
                }
                else if (XmlReport != null & ReportList.Text == "Выдача топлива по АЗС")
                {
                    xDoc.LoadXml(XmlReport);
                    XmlElement xRoot = xDoc.DocumentElement;
                   // string XPh = "//table[@name='АЗС']/header/*";
                    //string XPh2 = "//table[@name='АЗС']/total/*";
                    string XPh3 = "//table[@name='АЗС']/row/*";
                    //string XPh4 = "//table[@name='АЗС']/row/subrows/row/*";
                    try
                    {
                        XmlNodeList childnodes3 = xRoot.SelectNodes(XPh3);
                        int j = 0;
                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ".";
                        AZS AddAZS = new AZS();
                        AddAZS.UnitName = a.Name;
                        foreach (XmlNode n in childnodes3)
                        {                            
                            if (j == 0)
                            {                             
                                AddAZS.Time = n.Attributes.Item(0).Value.ToString();

                                j++; continue;
                            }
                            if (j == 1)
                            {                               
                                AddAZS.Ignition = Double.Parse(n.Attributes.Item(1).Value, provider);
                                j++; continue;
                            }
                            if (j == 2)
                            {                               
                                AddAZS.Valve = Int32.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 3)
                            {                               
                                AddAZS.FuelCounter = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 4)
                            {                               
                                AddAZS.Reader = Double.Parse(n.Attributes.Item(0).Value.ToString(), provider);

                                // System.Windows.Forms.MessageBox.Show(AddMerge.Time + " " + AddMerge.Merged + " " + AddMerge.FirstLevel + " " + AddMerge.SecondLevel + " " + AddMerge.Driver);
                                j++; continue;
                            }
                            if (j == 5)
                            {                                
                                AddAZS.Alarm = n.Attributes.Item(1).Value.ToString();
                                j++; continue;
                            }
                            if (j == 6)
                            {                              
                                AddAZS.FuelLevel = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 7)
                            {                               
                                AddAZS.Driver = n.Attributes.Item(0).Value.ToString();
                                j++; continue;

                            }

                            if (j == 8)
                            {                                
                                AddAZS.Coordinates = n.Attributes.Item(0).Value.ToString();
                                j = 0;
                                NewAZS.Add(new AZS() { Driver = AddAZS.Driver, Alarm = AddAZS.Driver, Coordinates = AddAZS.Coordinates, FuelCounter = AddAZS.FuelCounter, FuelLevel = AddAZS.FuelLevel, Ignition = AddAZS.Ignition, Reader = AddAZS.Reader, Time = AddAZS.Time, UnitName = a.Name.TrimEnd(' '), Valve = AddAZS.Valve });
                                continue;
                            }
                        }
                    }
                    catch { continue; }
                }
            }

            if (ReportList.Text == "График топлива и поездки CON2")
            {
                List<Merge> GarantyMerge = new List<Merge>();
                foreach (Merge Merg in NewMerge)
                {
                    DateTime date = new DateTime(1970, 1, 1).AddSeconds(Merg.Time);
                    if (Merg.FinalSpeed == 0 & Merg.Mileage == 0)
                    {
                        GarantyMerge.Add(Merg);
                    }
                    
                }
                if (GarantyMerge.Count != 0)
                {
                    Excel.Application ObjExcel = new Excel.Application();                  
                    ObjExcel.Visible = true;
                    ObjExcel.Workbooks.Add();
                    Excel._Worksheet WorkSheet = ObjExcel.ActiveSheet;
                    WorkSheet.Name = "Сливы";
                    WorkSheet.Cells[1, 1] = "Водитель";
                    WorkSheet.Cells[1, 2] = "Время";
                    WorkSheet.Cells[1, 3] = "Первоначальный уровень";
                    WorkSheet.Cells[1, 4] = "Конечный уровень";
                    WorkSheet.Cells[1, 5] = "Слито";
                    WorkSheet.Cells[1, 6] = "Пробег";
                    int m = 2;
                    for (int i = 0; i < GarantyMerge.Count; i++)
                    {
                        if (i % 5 == 0)
                        { m++; }
                        WorkSheet.Cells[m, i % 5] = GarantyMerge[i];
                    }
                }
                else
                    MessageBox.Show("Сливов не обнаружено");
            }
            if (ReportList.Text == "Выдача топлива по АЗС" & NewAZS.Count != 0)
            {
                Excel.Application ObjExcel = new Excel.Application();                
                ObjExcel.Visible = false;
                System.Diagnostics.Process excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL").Last();
                Excel.Workbook Workbooks = ObjExcel.Workbooks.Add();
                Excel._Worksheet WorkSheet = ObjExcel.ActiveSheet;
                WorkSheet.Name = "АЗС";
                WorkSheet.Cells[1, 1] = "Время";
                WorkSheet.Cells[1, 2] = "Зажигание";
                WorkSheet.Cells[1, 3] = "Клапан";
                WorkSheet.Cells[1, 4] = "Счетчик топлива";
                WorkSheet.Cells[1, 5] = "Считыватель";
                WorkSheet.Cells[1, 6] = "Тревожная кнопка";
                WorkSheet.Cells[1, 7] = "Уровень топлива";
                WorkSheet.Cells[1, 8] = "Водитель";
                WorkSheet.Cells[1, 9] = "Координаты";
                WorkSheet.Cells[1, 10] = "АЗС";
                int m = 2;
                List<AZS> GoodAZS = new List<AZS>();
                for (int i = 0; i < NewAZS.Count; i++)
                {
                    if (i != 0)
                    {
                        if (NewAZS[i].Valve == 1 & NewAZS[i - 1].Valve == 0)
                        {
                            GoodAZS.Add(NewAZS[i - 1]);
                            while (NewAZS[i].Valve == 1 & i < NewAZS.Count-1)
                            {
                                i++;
                            }
                            GoodAZS.Add(NewAZS[i - 1]);
                        }
                    }
                    else
                        GoodAZS.Add(NewAZS[i]);
                }
                GoodAZS.Sort((a, b) => a.UnitName.CompareTo(b.UnitName));
                for (int j = 0; j < GoodAZS.Count; j++)
                {
                    for (int i = 1; i < 11; i++)
                        switch (i % 10)
                        {
                            case 1: WorkSheet.Cells[m, i % 10] = GoodAZS[j].Time; break;
                            case 2: WorkSheet.Cells[m, i % 10] = GoodAZS[j].Ignition; break;
                            case 3: WorkSheet.Cells[m, i % 10] = GoodAZS[j].Valve; break;
                            case 4: WorkSheet.Cells[m, i % 10] = GoodAZS[j].FuelCounter; break;
                            case 5: WorkSheet.Cells[m, i % 10] = GoodAZS[j].Reader; break;
                            case 6: WorkSheet.Cells[m, i % 10] = GoodAZS[j].Alarm; break;
                            case 7: WorkSheet.Cells[m, i % 10] = GoodAZS[j].FuelLevel; break;
                            case 8: WorkSheet.Cells[m, i % 10] = GoodAZS[j].Driver; break;
                            case 9: WorkSheet.Cells[m, i % 10] = GoodAZS[j].Coordinates; break;
                            case 0: WorkSheet.Cells[m, 10] = GoodAZS[j].UnitName; break;
                        }
                    m++;
                }
                ObjExcel.DisplayAlerts = false;
                ObjExcel.Visible = false;
                ObjExcel.UserControl = false;              
                Workbooks.SaveAs(@Option.DirectoryTextBox.Text.Trim() + "\\" + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".xlsx");
                ObjExcel.DisplayAlerts = false;
                ObjExcel.Quit();
                ObjExcel.Workbooks.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjExcel);
                ObjExcel = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(Workbooks);
                Workbooks = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(WorkSheet);
                WorkSheet = null;
                excelProc.Kill();
                GC.Collect();
            }
            ReportBtn.Enabled = true;
        }
        private void AddGroup_Click(object sender, EventArgs e)
        {
            AddForm1.Show();
        }
        private void ComboBoxGroup_DropDown(object sender, EventArgs e)
        {   
            using (DbOption db = new DbOption())
            {
                var Groups = db.Groups.Select(x => new { GroupId = x.GroupId, Name = x.Name });
                ComboBoxGroup.DataSource = Groups.ToList();
                ComboBoxGroup.DisplayMember = "Name";
                ComboBoxGroup.ValueMember = "GroupId";
            }
        }
        private void опцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Option.Show();
        }

        
    }
}
