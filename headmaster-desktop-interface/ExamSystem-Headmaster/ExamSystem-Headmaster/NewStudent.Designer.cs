namespace ExamSystem_Headmaster
{
    partial class NewStudent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label5;
            this.s_name = new System.Windows.Forms.TextBox();
            this.s_surname = new System.Windows.Forms.TextBox();
            this.s_email = new System.Windows.Forms.TextBox();
            this.s_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.s_classes = new System.Windows.Forms.ComboBox();
            this.s_submit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.ForeColor = System.Drawing.Color.White;
            label5.Location = new System.Drawing.Point(73, 327);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(46, 17);
            label5.TabIndex = 9;
            label5.Text = "Class:";
            // 
            // s_name
            // 
            this.s_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s_name.Location = new System.Drawing.Point(26, 144);
            this.s_name.Name = "s_name";
            this.s_name.Size = new System.Drawing.Size(223, 26);
            this.s_name.TabIndex = 0;
            // 
            // s_surname
            // 
            this.s_surname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s_surname.Location = new System.Drawing.Point(26, 193);
            this.s_surname.Name = "s_surname";
            this.s_surname.Size = new System.Drawing.Size(223, 26);
            this.s_surname.TabIndex = 1;
            // 
            // s_email
            // 
            this.s_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s_email.Location = new System.Drawing.Point(26, 242);
            this.s_email.Name = "s_email";
            this.s_email.Size = new System.Drawing.Size(223, 26);
            this.s_email.TabIndex = 2;
            // 
            // s_password
            // 
            this.s_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s_password.Location = new System.Drawing.Point(26, 291);
            this.s_password.Name = "s_password";
            this.s_password.Size = new System.Drawing.Size(223, 26);
            this.s_password.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Surname:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(23, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Email:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(23, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Password:";
            // 
            // s_classes
            // 
            this.s_classes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s_classes.FormattingEnabled = true;
            this.s_classes.Items.AddRange(new object[] {
            "1-A",
            "1-B"});
            this.s_classes.Location = new System.Drawing.Point(76, 347);
            this.s_classes.Name = "s_classes";
            this.s_classes.Size = new System.Drawing.Size(119, 28);
            this.s_classes.TabIndex = 8;
            // 
            // s_submit
            // 
            this.s_submit.BackColor = System.Drawing.Color.White;
            this.s_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.s_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s_submit.ForeColor = System.Drawing.Color.Green;
            this.s_submit.Location = new System.Drawing.Point(26, 416);
            this.s_submit.Name = "s_submit";
            this.s_submit.Size = new System.Drawing.Size(223, 41);
            this.s_submit.TabIndex = 10;
            this.s_submit.Text = "Submit";
            this.s_submit.UseVisualStyleBackColor = false;
            this.s_submit.Click += new System.EventHandler(this.s_submit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ExamSystem_Headmaster.Properties.Resources.exam_system_icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(96, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 96);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // NewStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(264, 466);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.s_submit);
            this.Controls.Add(label5);
            this.Controls.Add(this.s_classes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.s_password);
            this.Controls.Add(this.s_email);
            this.Controls.Add(this.s_surname);
            this.Controls.Add(this.s_name);
            this.Name = "NewStudent";
            this.Text = "Create Student Account";
            this.Load += new System.EventHandler(this.NewStudent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox s_name;
        private System.Windows.Forms.TextBox s_surname;
        private System.Windows.Forms.TextBox s_email;
        private System.Windows.Forms.TextBox s_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox s_classes;
        private System.Windows.Forms.Button s_submit;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}