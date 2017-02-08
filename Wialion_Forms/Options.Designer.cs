namespace Wialion_Forms
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        //protected override void Dispose(bool disposing)
       // {
        //    if (disposing && (components != null))
          //  {
           //     components.Dispose();
           // }
           // base.Dispose(disposing);
       // }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UserDeleteBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.ProxyTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UserComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UserDeleteBtn);
            this.groupBox1.Controls.Add(this.SaveBtn);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.PortTextBox);
            this.groupBox1.Controls.Add(this.ProxyTextBox);
            this.groupBox1.Controls.Add(this.PasswordTextBox);
            this.groupBox1.Controls.Add(this.LoginTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.UserComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 250);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // UserDeleteBtn
            // 
            this.UserDeleteBtn.Location = new System.Drawing.Point(9, 191);
            this.UserDeleteBtn.Name = "UserDeleteBtn";
            this.UserDeleteBtn.Size = new System.Drawing.Size(439, 23);
            this.UserDeleteBtn.TabIndex = 14;
            this.UserDeleteBtn.Text = "Удалить пользователя";
            this.UserDeleteBtn.UseVisualStyleBackColor = true;
            this.UserDeleteBtn.Click += new System.EventHandler(this.UserDeleteBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(315, 151);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(133, 23);
            this.SaveBtn.TabIndex = 13;
            this.SaveBtn.Text = "Сохранить изменения";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Пользователь";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(161, 151);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(100, 20);
            this.PortTextBox.TabIndex = 11;
            // 
            // ProxyTextBox
            // 
            this.ProxyTextBox.Location = new System.Drawing.Point(6, 151);
            this.ProxyTextBox.Name = "ProxyTextBox";
            this.ProxyTextBox.Size = new System.Drawing.Size(100, 20);
            this.ProxyTextBox.TabIndex = 10;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(161, 77);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(100, 20);
            this.PasswordTextBox.TabIndex = 9;
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(9, 77);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(100, 20);
            this.LoginTextBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Порт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Прокси-сервер";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Логин";
            // 
            // UserComboBox
            // 
            this.UserComboBox.FormattingEnabled = true;
            this.UserComboBox.Location = new System.Drawing.Point(312, 77);
            this.UserComboBox.Name = "UserComboBox";
            this.UserComboBox.Size = new System.Drawing.Size(136, 21);
            this.UserComboBox.TabIndex = 0;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 287);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox UserComboBox;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox PortTextBox;
        public System.Windows.Forms.TextBox ProxyTextBox;
        public System.Windows.Forms.TextBox PasswordTextBox;
        public System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.Button UserDeleteBtn;
    }
}