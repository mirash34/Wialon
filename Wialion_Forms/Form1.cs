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
using System.Globalization;


namespace Wialion_Forms
{
    public partial class Form1 : Form
    {
        DataContext db;
        static long UId;
        static long RepId;
        static long ResId;
        DataTable dt = new DataTable();
        public WialonCollection Units;
        public WialonConnection Wialon;
        public WialonCollection Reports;
        AddForm AddForm1;
        Options Option;
        public Form1()
        {
            AddForm1 = new AddForm();
            Option = new Options();
            InitializeComponent();
            // string ConnectionString = ConfigurationManager.ConnectionStrings["DBGroup"].ConnectionString;

            db = new DataContext(ConfigurationManager.ConnectionStrings["DBGroup"].ConnectionString);
            Table<GroupData> GroupD = db.GetTable<GroupData>();
            this.ComboBoxGroup.DataSource = GroupD.ToList<GroupData>();
            this.ComboBoxGroup.DisplayMember = "GroupName";
            this.ComboBoxGroup.ValueMember = "GroupId";
            Wialon = new WialonConnection();
            Units = new WialonCollection();
            Reports = new WialonCollection();
            WialonUnit Unit = new WialonUnit();
            WialonReport Report = new WialonReport();
            TimeZoneFunctionality.TimeZoneInformation timeZoneInformation = TimeZoneFunctionality.GetTimeZone();
            //Console.WriteLine(timeZoneInformation.bias + " " + timeZoneInformation.daylightBias + " " + timeZoneInformation.daylightDate + " " + timeZoneInformation.daylightName + " " + timeZoneInformation.standardBias + " " + timeZoneInformation.standardName);
            long s;
            s = GetTimeZoneForWialon();
            long CountOfUnits;
            if (Wialon == null)
            {
                System.Windows.Forms.MessageBox.Show("No object");
                return;
            }
            
            //Wialon.SetProxyMode("activex-wln.sucden.ru", 80, "А.Морозов:sucden");
            //Units = Wialon.Login("activex-wln.sucden.ru", 80, "А.Морозов", "sucden");
            Wialon.SetProxyMode(Option.ProxyTextBox.Text.Trim(), (ushort)Convert.ToInt32(Option.PortTextBox.Text.Trim()), Option.LoginTextBox.Text.Trim()+":"+ Option.PasswordTextBox.Text.Trim());
            Units = Wialon.Login(Option.ProxyTextBox.Text.Trim(), (ushort)Convert.ToInt32(Option.PortTextBox.Text.Trim()), Option.LoginTextBox.Text.Trim(), Option.PasswordTextBox.Text.Trim());
            Reports = Wialon.GetReportsList();
            WialonReport NewRep = new WialonReport();
            List<ReportL> RepList = new List<ReportL>();
            foreach (WialonReport a in Reports)
            {

                if (a == null)
                {

                    System.Windows.Forms.MessageBox.Show(Wialon.GetLastError().ToString());
                    return;
                }
                else
                    // ReportList.Items.Add(a.Name);
                    RepList.Add(new ReportL() { Name = a.Name, ReportID = a.ReportID, ResourceID = a.ResourceID });


                // if (a.Name == "График топлива и поездки CON")
                //  {

                //     NewRep = a;
                //   RepId = a.ReportID;
                //   ResId = a.ResourceID;
                //   break;
                // }

            }
            ReportList.DataSource = RepList;
            ReportList.DisplayMember = "Name";
            ReportList.ValueMember = "ReportID";

            CountOfUnits = Units.Count;
            // Console.WriteLine("Units= " + CountOfUnits.ToString());
            List<UnitsData> UnitsCl = new List<UnitsData>();
            for (int i = 1; i < CountOfUnits; i++)
            {
                Unit = (WialonUnit)Units[i];
                // if (Unit.Name == "КАМАЗ О006НР")
                //  {
                //      UId = Unit.ID;

                //  }

                if (Unit == null)
                {
                    System.Windows.Forms.MessageBox.Show("Not unit");

                    return;
                }
                else
                {
                    //if ((ComboBoxGroup.SelectedValue.ToString()) != null)
                    UnitsCl.Add(new UnitsData() { UnitName = Unit.Name, UnitId = (int)Unit.ID });//, GroupId = Convert.ToInt32(ComboBoxGroup.SelectedValue.ToString()) });
                                                                                                 // else
                                                                                                 //  System.Windows.Forms.MessageBox.Show("Выберете группу");

                }
                //UnitsWialon.Items.Add(Unit.Name);


            }

            UnitsWialon.DataSource = UnitsCl;
            UnitsWialon.DisplayMember = "UnitName";
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
        {
            Table<UnitsData> GroupsD = db.GetTable<UnitsData>();

            if ((int)ComboBoxGroup.SelectedValue != 0)
            {
                List<UnitsData> GroupUnits = new List<UnitsData>();


                this.CheckedUnits.DataSource = GroupsD.ToList<UnitsData>().Where(x => x.GroupId == (int)ComboBoxGroup.SelectedValue).ToList<UnitsData>();
                this.CheckedUnits.DisplayMember = "UnitName";
                this.CheckedUnits.ValueMember = "UnitId";

            }



        }
        public static long GetTimeZoneForWialon()
        {
            TimeZoneFunctionality.TimeZoneInformation TZI;
            TimeZoneFunctionality.TIME_ZONE DST;
            long SomeDst = TimeZoneFunctionality.GetTimeZoneInformation(out TZI);
            if (SomeDst == 1)
                return TZI.bias * (-60);
            else if (SomeDst == 2)
                return TZI.bias * (-60) + 3600;
            else
                return TZI.bias * (-60) - 3600;
            // return TZI.bias * (-60) + (int)(DST - 1) * 3600;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<UnitsData> NewUnits = new List<UnitsData>();
            foreach (UnitsData a in CheckedUnits.Items)
            { NewUnits.Add(a); }
            if (ComboBoxGroup.SelectedValue != null & ComboBoxGroup.SelectedValue != "")
            {
                foreach (UnitsData Check in UnitsWialon.CheckedItems)
                {
                    bool flag = false;
                    foreach (UnitsData i in CheckedUnits.Items)
                    {
                        if (Check.UnitName == i.UnitName)
                        { flag = true; break; }
                    }

                    if (!flag)
                    {
                        //CheckedUnits.Items.Add(Check);
                        NewUnits.Add(new UnitsData() { UnitName = Check.UnitName, UnitId = Check.UnitId, GroupId = Convert.ToInt32(ComboBoxGroup.SelectedValue.ToString()) });
                    }
                }
                CheckedUnits.DataSource = NewUnits;
                CheckedUnits.DisplayMember = "UnitName";
                CheckedUnits.ValueMember = "UnitId";

                Table<UnitsData> UnitsD = db.GetTable<UnitsData>();
                if (NewUnits.Count != 0)

                    foreach (UnitsData a in NewUnits)
                    {
                        try
                        {
                            UnitsD.InsertOnSubmit(new UnitsData { UnitName = a.UnitName, UnitId = a.UnitId, GroupId = (int)ComboBoxGroup.SelectedValue });
                            db.SubmitChanges();
                        }

                        catch { System.Windows.Forms.MessageBox.Show("Не удалось добавить технику " + a.UnitName); }
                    }

                else
                    System.Windows.Forms.MessageBox.Show("Вы не выбрали технику");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<UnitsData> SaveUnit = new List<UnitsData>();
            List<UnitsData> RemoveUnit = new List<UnitsData>();


            for (int i = CheckedUnits.Items.Count - 1; i >= 0; i--)
            {
                if (!CheckedUnits.GetItemChecked(i))
                    // CheckedUnits.Items.RemoveAt(i);

                    SaveUnit.Add((UnitsData)CheckedUnits.Items[i]);
                else
                    RemoveUnit.Add((UnitsData)CheckedUnits.Items[i]);
            }
            SaveUnit.Reverse();
            CheckedUnits.DataSource = SaveUnit;
            CheckedUnits.DisplayMember = "UnitName";
            CheckedUnits.ValueMember = "UnitId";

            Table<UnitsData> UnitsD = db.GetTable<UnitsData>();

            foreach (UnitsData a in RemoveUnit)
            {
                var unit = UnitsD.Where(u => u.UnitName == a.UnitName & u.UnitId == a.UnitId & u.GroupId == a.GroupId).First();
                UnitsD.DeleteOnSubmit(unit);
                db.SubmitChanges();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            DateTime From;
            DateTime To;
            DateTime TimeFrom1970 = new DateTime(1970, 1, 1);
            From = PickerFrom.Value;
            To = PickerTo.Value;
            TimeSpan DiffTo = To - TimeFrom1970;
            TimeSpan DiffFrom = From - TimeFrom1970;
            int FromTo = (To - From).Seconds;
            //List<long> NameIdUnit = new List<long>();
            WialonConnection Wialon = new WialonConnection();
            WialonCollection Units = new WialonCollection();
            WialonCollection Reports = new WialonCollection();
            WialonUnit Unit = new WialonUnit();
            // Wialon.SetProxyMode("activex-wln.sucden.ru", 80, "А.Морозов:sucden");
            // Units = Wialon.Login("activex-wln.sucden.ru", 80, "А.Морозов", "sucden");
            Wialon.SetProxyMode(Option.ProxyTextBox.Text.Trim(), (ushort)Convert.ToInt32(Option.PortTextBox.Text.Trim()), Option.LoginTextBox.Text.Trim() + ":" + Option.PasswordTextBox.Text.Trim());
            Units = Wialon.Login(Option.ProxyTextBox.Text.Trim(), (ushort)Convert.ToInt32(Option.PortTextBox.Text.Trim()), Option.LoginTextBox.Text.Trim(), Option.PasswordTextBox.Text.Trim());

            Reports = Wialon.GetReportsList();

            // for (int i = 0; i < CheckedUnits.Items.Count; i++)
            //{
            //  foreach (WialonUnit a in Units)

            //     if (a.Name == CheckedUnits.Items[i].ToString())
            //    { NameIdUnit.Add(a.ID); }
            // }
            //}
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

            // for (int i = 0; i < NameIdUnit.Count; i++)
            string AllMerges;
            List<Merge> NewMerge = new List<Merge>();
            foreach (UnitsData a in CheckedUnits.Items)
            {
                int someinf = (int)GetTimeZoneForWialon();
                string some2 = Wialon.GetReportByID((uint)DiffFrom.TotalSeconds, (uint)DiffTo.TotalSeconds, a.UnitId, 10800, "ru", IdRes, IdRep);
                string some = Wialon.GetReportByID((uint)DiffFrom.TotalSeconds, (uint)DiffTo.TotalSeconds, a.UnitId, (int)GetTimeZoneForWialon(), "ru", IdRes, IdRep);
                StreamWriter SW = new StreamWriter(new FileStream(@"C:\Rapport\MyXml.txt", FileMode.Create, FileAccess.Write));
                SW.Write(some);
                SW.Close();
                XmlDocument xDoc = new XmlDocument();
                if (some != null)
                {

                    xDoc.LoadXml(some);
                    XmlElement xRoot = xDoc.DocumentElement;
                    string XPh = "//table[@name='Сливы']/header/*";
                    string XPh2 = "//table[@name='Сливы']/total/*";
                    string XPh3 = "//table[@name='Сливы']/row/*";
                    string XPh4 = "//table[@name='Поездки']/row/subrows/row/*";
                    XmlNodeList childnodes = xRoot.SelectNodes(XPh);
                    XmlNodeList childnodes2 = xRoot.SelectNodes(XPh2);
                    try
                    {
                        XmlNodeList childnodes3 = xRoot.SelectNodes(XPh3);


                        // XmlNodeList childnodes4 = xRoot.SelectNodes(XPh4);



                        //   foreach (XmlNode n in childnodes)
                        // {
                        //     System.Windows.Forms.MessageBox.Show(n.Attributes.Item(0).Value.ToString());

                        //  }
                        //  foreach (XmlNode n in childnodes2)
                        //  {
                        //     System.Windows.Forms.MessageBox.Show(n.Attributes.Item(0).Value.ToString());

                        //  }
                        int j = 0;

                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ".";
                        Merge AddMerge = new Merge();
                        AddMerge.UnitName = a.UnitName;
                        foreach (XmlNode n in childnodes3)
                        {
                            // System.Windows.Forms.MessageBox.Show(n.Attributes.Item(0).Value.ToString());
                            if (j == 0)
                            {
                                //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(1).Value.ToString());
                                AddMerge.Time = Int32.Parse(n.Attributes.Item(1).Value.ToString());

                                j++; continue;
                            }
                            if (j == 1)
                            {
                                //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(1).Value);
                                AddMerge.Merged = Double.Parse(n.Attributes.Item(1).Value, provider);
                                j++; continue;
                            }
                            if (j == 2)
                            {
                                //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(1).Value.ToString());
                                AddMerge.FirstLevel = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 3)
                            {
                                //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(1).Value.ToString());
                                AddMerge.SecondLevel = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 4)
                            {
                                //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(0).Value.ToString());
                                AddMerge.Driver = n.Attributes.Item(0).Value.ToString();

                                // System.Windows.Forms.MessageBox.Show(AddMerge.Time + " " + AddMerge.Merged + " " + AddMerge.FirstLevel + " " + AddMerge.SecondLevel + " " + AddMerge.Driver);
                                j++; continue;
                            }
                            if (j == 5)
                            {
                                //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(1).Value.ToString());
                                AddMerge.FirstSpeed = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 6)
                            {
                                //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(1).Value.ToString());
                                AddMerge.FinalSpeed = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j++; continue;
                            }
                            if (j == 7)
                            {
                                //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(1).Value.ToString());
                                AddMerge.Mileage = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                                j = 0;
                                NewMerge.Add(new Merge() { Driver = AddMerge.Driver, FirstLevel = AddMerge.FirstLevel, Merged = AddMerge.Merged, SecondLevel = AddMerge.SecondLevel, Time = AddMerge.Time, FinalSpeed = AddMerge.FinalSpeed, FirstSpeed = AddMerge.FirstSpeed, Mileage = AddMerge.Mileage, UnitName = AddMerge.UnitName });
                                continue;
                            }

                        }
                    }
                    catch { continue; }



                    /*
                    int i = 0;
                    List<Trip> Trips = new List<Trip>();


                    Trip AddTrip = new Trip();
                    foreach (XmlNode n in childnodes4)
                    {
                        //if (n.Name=="subrows")
                        //{continue;}
                        if (i == 0)
                        {
                            //System.Windows.Forms.MessageBox.Show(n.Attributes.Item(1).Value.ToString());
                           AddTrip.Groups = Int32.Parse(n.Attributes.Item(1).Value.ToString());
                            i++; continue;
                        }
                        if (i == 1)
                        {                    
                            AddTrip.Start = Int32.Parse(n.Attributes.Item(1).Value.ToString());
                            i++; continue;
                        }
                        if (i == 2)
                        {
                            AddTrip.InitialPosition = (n.Attributes.Item(0).Value.ToString());
                            i++; continue;
                        }
                        if (i == 3)
                        {
                            AddTrip.End = Int32.Parse(n.Attributes.Item(1).Value.ToString());
                            i++; continue;
                        }
                        if (i == 4)
                        {
                            AddTrip.EndPosition = (n.Attributes.Item(0).Value.ToString());
                            i++; continue;
                        }
                        if (i == 5)
                        {
                            AddTrip.TotalTime = Int32.Parse(n.Attributes.Item(1).Value.ToString());
                            i++; continue;
                        }
                        if (i == 6)
                        {
                            AddTrip.AverageSpeed = Double.Parse(n.Attributes.Item(1).Value.ToString(),provider);
                            i++; continue;
                        }
                        if (i == 7)
                        {
                            AddTrip.FirstLevel = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                            i++; continue;
                        }
                        if (i == 8)
                        {
                            AddTrip.FinalLevel = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                            i++; continue;
                        }
                        if (i == 9)
                        {
                            AddTrip.Spent = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                            i++; continue;
                        }
                        if (i == 10)
                        {
                            AddTrip.Mileage = Double.Parse(n.Attributes.Item(1).Value.ToString(), provider);
                            i = 0;
                            Trips.Add(new Trip(){AverageSpeed=AddTrip.AverageSpeed, End=AddTrip.End, EndPosition=AddTrip.EndPosition, FinalLevel=AddTrip.FinalLevel, FirstLevel=AddTrip.FirstLevel, Groups=AddTrip.Groups, InitialPosition=AddTrip.InitialPosition, Mileage=AddTrip.Mileage, Spent=AddTrip.Mileage, Start=AddTrip.Start, TotalTime=AddTrip.TotalTime}); continue;
                        }

                    }
                    foreach(Trip b in Trips)
                    { System.Windows.Forms.MessageBox.Show(b.Groups+" "+b.Start+" "+b.InitialPosition+" "+b.End+" "+b.EndPosition+" "+b.TotalTime+" "+b.AverageSpeed+" "+b.FirstLevel+" "+b.FinalLevel+" "+b.Spent+" "+b.Mileage); }

                    */



                }


            }

            AllMerges = "Предполагаемые сливы: \n";
            List<Merge> GarantyMerge = new List<Merge>();
            foreach (Merge Merg in NewMerge)
            {
                DateTime date = new DateTime(1970, 1, 1).AddSeconds(Merg.Time);
                if (Merg.FinalSpeed==0 & Merg.Mileage==0)
                {
                    GarantyMerge.Add(Merg);

                }
                //AllMerges += Merg.Time + " " + Merg.Merged + " " + Merg.FirstLevel + " " + Merg.SecondLevel + " " + Merg.Driver + " " + Merg.FirstSpeed + " " + Merg.FinalSpeed + " " + Merg.Mileage+"\n";
               // AllMerges += date + " " + Merg.UnitName + " " + Merg.Merged + " " + Merg.FirstLevel + " " + Merg.SecondLevel + " " + Merg.Driver + " " + Merg.FirstSpeed + " " + Merg.FinalSpeed + " " + Merg.Mileage + "\n";

            }
            if (GarantyMerge.Count != 0)
            { }


            //System.Windows.Forms.MessageBox.Show(AllMerges);
            button3.Enabled = true;

        }

        private void AddGroup_Click(object sender, EventArgs e)
        {
            AddForm1.Show();
        }

        private void ComboBoxGroup_DropDown(object sender, EventArgs e)
        {
            Table<GroupData> GroupsD = db.GetTable<GroupData>();
            // List<GroupData> GroupD = new List<GroupData>();
            var Groups = GroupsD.Select(x => new { GroupId = x.GroupId, GroupName = x.GroupName });

            ComboBoxGroup.DataSource = Groups.ToList();
            ComboBoxGroup.DisplayMember = "GroupName";
            ComboBoxGroup.ValueMember = "GroupId";
        }

        private void опцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Option.Show();
        }


    }
    public class Trip
    {
        public int Groups { get; set; }
        public int Start { get; set; }
        public string InitialPosition { get; set; }
        public int End { get; set; }
        public string EndPosition { get; set; }
        public int TotalTime { get; set; }
        public double AverageSpeed { get; set; }
        public double FirstLevel { get; set; }
        public double FinalLevel { get; set; }
        public double Spent { get; set; }
        public double Mileage { get; set; }
    }
    public class Merge
    {
        public int Time { get; set; }
        public double Merged { get; set; }
        public double FirstLevel { get; set; }
        public double SecondLevel { get; set; }
        public string Driver { get; set; }
        public double FirstSpeed { get; set; }
        public double FinalSpeed { get; set; }
        public double Mileage { get; set; }
        public string UnitName { get; set; }


    }
    public class ReportL
    {
        public string Name { get; set; }
        public long ReportID { get; set; }
        public long ResourceID { get; set; }

    }
    public class TimeZoneFunctionality
    {
        /// <summary>
        /// [Win32 API call]
        /// The GetTimeZoneInformation function retrieves the current time-zone parameters.
        /// These parameters control the translations between Coordinated Universal Time (UTC)
        /// and local time.
        /// </summary>
        /// <param name="lpTimeZoneInformation">[out] Pointer to a TIME_ZONE_INFORMATION structure to receive the current time-zone parameters.</param>
        /// <returns>
        /// If the function succeeds, the return value is one of the following values.
        /// <list type="table">
        /// <listheader>
        /// <term>Return code/value</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>TIME_ZONE_ID_UNKNOWN == 0</term>
        /// <description>
        /// The system cannot determine the current time zone. This error is also returned if you call the SetTimeZoneInformation function and supply the bias values but no transition dates.
        /// This value is returned if daylight saving time is not used in the current time zone, because there are no transition dates.
        /// </description>
        /// </item>
        /// <item>
        /// <term>TIME_ZONE_ID_STANDARD == 1</term>
        /// <description>
        /// The system is operating in the range covered by the StandardDate member of the TIME_ZONE_INFORMATION structure.
        /// </description>
        /// </item>
        /// <item>
        /// <term>TIME_ZONE_ID_DAYLIGHT == 2</term>
        /// <description>
        /// The system is operating in the range covered by the DaylightDate member of the TIME_ZONE_INFORMATION structure.
        /// </description>
        /// </item>
        /// </list>
        /// If the function fails, the return value is TIME_ZONE_ID_INVALID. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetTimeZoneInformation(out TimeZoneInformation lpTimeZoneInformation);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void GetSystemTime(out SystemTime lpSystemTipe);
        /// <summary>
        /// [Win32 API call]
        /// The SetTimeZoneInformation function sets the current time-zone parameters.
        /// These parameters control translations from Coordinated Universal Time (UTC)
        /// to local time.
        /// </summary>
        /// <param name="lpTimeZoneInformation">[in] Pointer to a TIME_ZONE_INFORMATION structure that contains the time-zone parameters to set.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetTimeZoneInformation([In] ref TimeZoneInformation lpTimeZoneInformation);

        /// <summary>
        /// The SystemTime structure represents a date and time using individual members
        /// for the month, day, year, weekday, hour, minute, second, and millisecond.
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public int year;
            public int month;
            public int dayOfWeek;
            public int day;
            public int hour;
            public int minute;
            public int second;
            public int milliseconds;
        }

        /// <summary>
        /// The TimeZoneInformation structure specifies information specific to the time zone.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TimeZoneInformation
        {
            /// <summary>
            /// Current bias for local time translation on this computer, in minutes. The bias is the difference, in minutes, between Coordinated Universal Time (UTC) and local time. All translations between UTC and local time are based on the following formula:
            /// <para>UTC = local time + bias</para>
            /// <para>This member is required.</para>
            /// </summary>
            public int bias;
            /// <summary>
            /// Pointer to a null-terminated string associated with standard time. For example, "EST" could indicate Eastern Standard Time. The string will be returned unchanged by the GetTimeZoneInformation function. This string can be empty.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string standardName;
            /// <summary>
            /// A SystemTime structure that contains a date and local time when the transition from daylight saving time to standard time occurs on this operating system. If the time zone does not support daylight saving time or if the caller needs to disable daylight saving time, the wMonth member in the SystemTime structure must be zero. If this date is specified, the DaylightDate value in the TimeZoneInformation structure must also be specified. Otherwise, the system assumes the time zone data is invalid and no changes will be applied.
            /// <para>To select the correct day in the month, set the wYear member to zero, the wHour and wMinute members to the transition time, the wDayOfWeek member to the appropriate weekday, and the wDay member to indicate the occurence of the day of the week within the month (first through fifth).</para>
            /// <para>Using this notation, specify the 2:00a.m. on the first Sunday in April as follows: wHour = 2, wMonth = 4, wDayOfWeek = 0, wDay = 1. Specify 2:00a.m. on the last Thursday in October as follows: wHour = 2, wMonth = 10, wDayOfWeek = 4, wDay = 5.</para>
            /// </summary>
            public SystemTime standardDate;
            /// <summary>
            /// Bias value to be used during local time translations that occur during standard time. This member is ignored if a value for the StandardDate member is not supplied.
            /// <para>This value is added to the value of the Bias member to form the bias used during standard time. In most time zones, the value of this member is zero.</para>
            /// </summary>
            public int standardBias;
            /// <summary>
            /// Pointer to a null-terminated string associated with daylight saving time. For example, "PDT" could indicate Pacific Daylight Time. The string will be returned unchanged by the GetTimeZoneInformation function. This string can be empty.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string daylightName;
            /// <summary>
            /// A SystemTime structure that contains a date and local time when the transition from standard time to daylight saving time occurs on this operating system. If the time zone does not support daylight saving time or if the caller needs to disable daylight saving time, the wMonth member in the SystemTime structure must be zero. If this date is specified, the StandardDate value in the TimeZoneInformation structure must also be specified. Otherwise, the system assumes the time zone data is invalid and no changes will be applied.
            /// <para>To select the correct day in the month, set the wYear member to zero, the wHour and wMinute members to the transition time, the wDayOfWeek member to the appropriate weekday, and the wDay member to indicate the occurence of the day of the week within the month (first through fifth).</para>
            /// </summary>
            public SystemTime daylightDate;
            /// <summary>
            /// Bias value to be used during local time translations that occur during daylight saving time. This member is ignored if a value for the DaylightDate member is not supplied.
            /// <para>This value is added to the value of the Bias member to form the bias used during daylight saving time. In most time zones, the value of this member is –60.</para>
            /// </summary>
            public int daylightBias;
        }

        /// <summary>
        /// Sets new time-zone information for the local system.
        /// </summary>
        /// <param name="tzi">Struct containing the time-zone parameters to set.</param>
        public static void SetTimeZone(TimeZoneInformation tzi)
        {
            // set local system timezone
            SetTimeZoneInformation(ref tzi);
        }

        /// <summary>
        /// Gets current timezone information for the local system.
        /// </summary>
        /// <returns>Struct containing the current time-zone parameters.</returns>
        public static TimeZoneInformation GetTimeZone()
        {
            // create struct instance
            TimeZoneInformation tzi;

            // retrieve timezone info
            long currentTimeZone = GetTimeZoneInformation(out tzi);

            return tzi;
        }

        /// <summary>
        /// Oversimplyfied method to test  the GetTimeZone functionality
        /// </summary>
        /// <param name="args">the usual stuff</param>
        /// 
        public enum TIME_ZONE
        {
            TIME_ZONE_ID_INVALID = 0,
            TIME_ZONE_STANDARD = 1,
            TIME_ZONE_DAYLIGHT = 2
        }


    }
    public class XmlData
    {
        public string txt { get; set; }
        public string val { get; set; }
        public string vt { get; set; }
        public string Name { get; set; }
        public int NumCol { get; set; }
    }
    [Table(Name = "Units")]
    public class UnitsData
    {
        // [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "UnitId")]
        [Column(Name = "UnitId")]
        public int UnitId { get; set; }
        [Column(Name = "Name")]
        public string UnitName { get; set; }
        [Column(Name = "GroupId")]
        public int GroupId { get; set; }
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Id")]
        public int Id { get; set; }
    }

    [Table(Name = "Groups")]
    public class GroupData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "GroupId")]
        public int GroupId { get; set; }
        [Column(Name = "Name")]
        public string GroupName { get; set; }

    }
}
