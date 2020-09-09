namespace ExamSystem_Headmaster
{
    partial class StudentView
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
            this.sv_name = new System.Windows.Forms.TextBox();
            this.sv_surname = new System.Windows.Forms.TextBox();
            this.sv_mail = new System.Windows.Forms.TextBox();
            this.sv_password = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sv_class = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sv_students = new System.Windows.Forms.ListView();
            this.S_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.S_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.S_SURNAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.S_CLASS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // sv_name
            // 
            this.sv_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sv_name.Location = new System.Drawing.Point(12, 138);
            this.sv_name.Name = "sv_name";
            this.sv_name.Size = new System.Drawing.Size(147, 26);
            this.sv_name.TabIndex = 1;
            // 
            // sv_surname
            // 
            this.sv_surname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sv_surname.Location = new System.Drawing.Point(12, 203);
            this.sv_surname.Name = "sv_surname";
            this.sv_surname.Size = new System.Drawing.Size(146, 26);
            this.sv_surname.TabIndex = 2;
            // 
            // sv_mail
            // 
            this.sv_mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sv_mail.Location = new System.Drawing.Point(12, 264);
            this.sv_mail.Name = "sv_mail";
            this.sv_mail.Size = new System.Drawing.Size(147, 26);
            this.sv_mail.TabIndex = 3;
            // 
            // sv_password
            // 
            this.sv_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sv_password.Location = new System.Drawing.Point(12, 324);
            this.sv_password.Name = "sv_password";
            this.sv_password.Size = new System.Drawing.Size(146, 26);
            this.sv_password.TabIndex = 4;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(12, 115);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(55, 20);
            this.label.TabIndex = 6;
            this.label.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Surname:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Mail:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(23, 364);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Class:";
            // 
            // sv_class
            // 
            this.sv_class.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sv_class.FormattingEnabled = true;
            this.sv_class.Location = new System.Drawing.Point(23, 387);
            this.sv_class.Name = "sv_class";
            this.sv_class.Size = new System.Drawing.Size(108, 28);
            this.sv_class.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Green;
            this.button1.Location = new System.Drawing.Point(11, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 49);
            this.button1.TabIndex = 12;
            this.button1.Text = "Save Changes";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(176, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Students:";
            // 
            // sv_students
            // 
            this.sv_students.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.S_ID,
            this.S_NAME,
            this.S_SURNAME,
            this.S_CLASS});
            this.sv_students.FullRowSelect = true;
            this.sv_students.HideSelection = false;
            this.sv_students.Location = new System.Drawing.Point(180, 47);
            this.sv_students.Name = "sv_students";
            this.sv_students.Size = new System.Drawing.Size(355, 436);
            this.sv_students.TabIndex = 15;
            this.sv_students.UseCompatibleStateImageBehavior = false;
            this.sv_students.View = System.Windows.Forms.View.Details;
            this.sv_students.SelectedIndexChanged += new System.EventHandler(this.sv_students_SelectedIndexChanged);
            // 
            // S_ID
            // 
            this.S_ID.Text = "ID";
            // 
            // S_NAME
            // 
            this.S_NAME.Text = "Name";
            this.S_NAME.Width = 101;
            // 
            // S_SURNAME
            // 
            this.S_SURNAME.Text = "Surname";
            this.S_SURNAME.Width = 114;
            // 
            // S_CLASS
            // 
            this.S_CLASS.Text = "Class";
            this.S_CLASS.Width = 79;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ExamSystem_Headmaster.Properties.Resources.exam_system_icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(49, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(82, 75);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // StudentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(547, 516);
            this.Controls.Add(this.sv_students);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sv_class);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label);
            this.Controls.Add(this.sv_password);
            this.Controls.Add(this.sv_mail);
            this.Controls.Add(this.sv_surname);
            this.Controls.Add(this.sv_name);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "StudentView";
            this.Text = "Students Details";
            this.Load += new System.EventHandler(this.StudentView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox sv_name;
        private System.Windows.Forms.TextBox sv_surname;
        private System.Windows.Forms.TextBox sv_mail;
        private System.Windows.Forms.TextBox sv_password;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox sv_class;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView sv_students;
        private System.Windows.Forms.ColumnHeader S_ID;
        private System.Windows.Forms.ColumnHeader S_NAME;
        private System.Windows.Forms.ColumnHeader S_SURNAME;
        private System.Windows.Forms.ColumnHeader S_CLASS;
    }
}