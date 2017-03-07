namespace Wialion_Forms
{
    partial class WialonForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
       
    
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

       

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WialonForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AddBtn = new System.Windows.Forms.Button();
            this.UnitsWialon = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CheckedUnits = new System.Windows.Forms.CheckedListBox();
            this.RemoveBtn = new System.Windows.Forms.Button();
            this.ReportList = new System.Windows.Forms.ComboBox();
            this.PickerFrom = new System.Windows.Forms.DateTimePicker();
            this.PickerTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ReportBtn = new System.Windows.Forms.Button();
            this.ComboBoxGroup = new System.Windows.Forms.ComboBox();
            this.AddGroup = new System.Windows.Forms.Button();
            this.ReportLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.zToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.опцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AddBtn);
            this.groupBox1.Controls.Add(this.UnitsWialon);
            this.groupBox1.Location = new System.Drawing.Point(13, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 326);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Техника";
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(110, 295);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(96, 21);
            this.AddBtn.TabIndex = 4;
            this.AddBtn.Text = "Добавить >>>";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // UnitsWialon
            // 
            this.UnitsWialon.FormattingEnabled = true;
            this.UnitsWialon.Location = new System.Drawing.Point(6, 16);
            this.UnitsWialon.Name = "UnitsWialon";
            this.UnitsWialon.Size = new System.Drawing.Size(335, 259);
            this.UnitsWialon.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CheckedUnits);
            this.groupBox2.Controls.Add(this.RemoveBtn);
            this.groupBox2.Location = new System.Drawing.Point(390, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 326);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Техника в группе";
            // 
            // CheckedUnits
            // 
            this.CheckedUnits.FormattingEnabled = true;
            this.CheckedUnits.Location = new System.Drawing.Point(6, 16);
            this.CheckedUnits.Name = "CheckedUnits";
            this.CheckedUnits.Size = new System.Drawing.Size(335, 259);
            this.CheckedUnits.TabIndex = 2;
            // 
            // RemoveBtn
            // 
            this.RemoveBtn.Location = new System.Drawing.Point(131, 295);
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new System.Drawing.Size(96, 21);
            this.RemoveBtn.TabIndex = 1;
            this.RemoveBtn.Text = "Удалить";
            this.RemoveBtn.UseVisualStyleBackColor = true;
            this.RemoveBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // ReportList
            // 
            this.ReportList.FormattingEnabled = true;
            this.ReportList.Location = new System.Drawing.Point(19, 87);
            this.ReportList.Name = "ReportList";
            this.ReportList.Size = new System.Drawing.Size(335, 21);
            this.ReportList.TabIndex = 2;
            // 
            // PickerFrom
            // 
            this.PickerFrom.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.PickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PickerFrom.Location = new System.Drawing.Point(39, 36);
            this.PickerFrom.Name = "PickerFrom";
            this.PickerFrom.Size = new System.Drawing.Size(200, 20);
            this.PickerFrom.TabIndex = 4;
            this.PickerFrom.Value = new System.DateTime(2016, 12, 27, 11, 24, 34, 0);
            // 
            // PickerTo
            // 
            this.PickerTo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.PickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PickerTo.Location = new System.Drawing.Point(417, 36);
            this.PickerTo.Name = "PickerTo";
            this.PickerTo.Size = new System.Drawing.Size(200, 20);
            this.PickerTo.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "С";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(393, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "По";
            // 
            // ReportBtn
            // 
            this.ReportBtn.Location = new System.Drawing.Point(16, 485);
            this.ReportBtn.Name = "ReportBtn";
            this.ReportBtn.Size = new System.Drawing.Size(720, 23);
            this.ReportBtn.TabIndex = 8;
            this.ReportBtn.Text = "Составить отчёт";
            this.ReportBtn.UseVisualStyleBackColor = true;
            this.ReportBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // ComboBoxGroup
            // 
            this.ComboBoxGroup.FormattingEnabled = true;
            this.ComboBoxGroup.Location = new System.Drawing.Point(396, 87);
            this.ComboBoxGroup.Name = "ComboBoxGroup";
            this.ComboBoxGroup.Size = new System.Drawing.Size(221, 21);
            this.ComboBoxGroup.TabIndex = 9;
            this.ComboBoxGroup.DropDown += new System.EventHandler(this.ComboBoxGroup_DropDown);
            
            // 
            // AddGroup
            // 
            this.AddGroup.Location = new System.Drawing.Point(637, 86);
            this.AddGroup.Name = "AddGroup";
            this.AddGroup.Size = new System.Drawing.Size(102, 23);
            this.AddGroup.TabIndex = 10;
            this.AddGroup.Text = "Редактировать";
            this.AddGroup.UseVisualStyleBackColor = true;
            this.AddGroup.Click += new System.EventHandler(this.AddGroup_Click);
            // 
            // ReportLabel
            // 
            this.ReportLabel.AutoSize = true;
            this.ReportLabel.Location = new System.Drawing.Point(19, 71);
            this.ReportLabel.Name = "ReportLabel";
            this.ReportLabel.Size = new System.Drawing.Size(36, 13);
            this.ReportLabel.TabIndex = 3;
            this.ReportLabel.Text = "Отчет";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Группа";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(751, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // zToolStripMenuItem
            // 
            this.zToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.опцииToolStripMenuItem});
            this.zToolStripMenuItem.Name = "zToolStripMenuItem";
            this.zToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.zToolStripMenuItem.Text = "Файл";
            // 
            // опцииToolStripMenuItem
            // 
            this.опцииToolStripMenuItem.Name = "опцииToolStripMenuItem";
            this.опцииToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.опцииToolStripMenuItem.Text = "Опции";
            this.опцииToolStripMenuItem.Click += new System.EventHandler(this.опцииToolStripMenuItem_Click);
            // 
            // WialonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 526);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AddGroup);
            this.Controls.Add(this.ComboBoxGroup);
            this.Controls.Add(this.ReportBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PickerTo);
            this.Controls.Add(this.PickerFrom);
            this.Controls.Add(this.ReportLabel);
            this.Controls.Add(this.ReportList);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "WialonForm";
            this.Text = "WialonAPI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox UnitsWialon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ReportList;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button RemoveBtn;
        private System.Windows.Forms.CheckedListBox CheckedUnits;
        private System.Windows.Forms.DateTimePicker PickerFrom;
        private System.Windows.Forms.DateTimePicker PickerTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ReportBtn;
        private System.Windows.Forms.ComboBox ComboBoxGroup;
        private System.Windows.Forms.Button AddGroup;
        private System.Windows.Forms.Label ReportLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem zToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem опцииToolStripMenuItem;
    }
}

