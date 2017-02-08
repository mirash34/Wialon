namespace Wialion_Forms
{
    partial class AddForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        
        
      //  protected override void Dispose(bool disposing)
        //{
         //   if (disposing && (components != null))
          //  {
            //    components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddForm));
            this.label1 = new System.Windows.Forms.Label();
            this.AddOk = new System.Windows.Forms.Button();
            this.ComboGroup = new System.Windows.Forms.ComboBox();
            this.RemoveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите или выбирете имя группы";
            // 
            // AddOk
            // 
            this.AddOk.Location = new System.Drawing.Point(63, 93);
            this.AddOk.Name = "AddOk";
            this.AddOk.Size = new System.Drawing.Size(75, 23);
            this.AddOk.TabIndex = 2;
            this.AddOk.Text = "Создать";
            this.AddOk.UseVisualStyleBackColor = true;
            this.AddOk.Click += new System.EventHandler(this.AddOk_Click);
            // 
            // ComboGroup
            // 
            this.ComboGroup.FormattingEnabled = true;
            this.ComboGroup.Location = new System.Drawing.Point(63, 66);
            this.ComboGroup.Name = "ComboGroup";
            this.ComboGroup.Size = new System.Drawing.Size(303, 21);
            this.ComboGroup.TabIndex = 4;
            // 
            // RemoveBtn
            // 
            this.RemoveBtn.Location = new System.Drawing.Point(291, 93);
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoveBtn.TabIndex = 5;
            this.RemoveBtn.Text = "Удалить";
            this.RemoveBtn.UseVisualStyleBackColor = true;
            this.RemoveBtn.Click += new System.EventHandler(this.RemoveBtn_Click);
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 137);
            this.Controls.Add(this.RemoveBtn);
            this.Controls.Add(this.ComboGroup);
            this.Controls.Add(this.AddOk);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddForm";
            this.Text = "Редактировать группы";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddOk;
        private System.Windows.Forms.ComboBox ComboGroup;
        private System.Windows.Forms.Button RemoveBtn;
    }
}