namespace ExamSystem_Headmaster
{
    partial class Form2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.teacher_statistics = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.h_surname = new System.Windows.Forms.Label();
            this.h_name = new System.Windows.Forms.Label();
            this.m_student_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.new_teacher = new System.Windows.Forms.Button();
            this.new_student = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_classes = new System.Windows.Forms.ComboBox();
            this.m_students = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_subjects = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.T_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.T_SURNAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.T_EXAM_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_student_chart)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // teacher_statistics
            // 
            this.teacher_statistics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.T_NAME,
            this.T_SURNAME,
            this.T_EXAM_COUNT});
            this.teacher_statistics.FullRowSelect = true;
            this.teacher_statistics.HideSelection = false;
            this.teacher_statistics.Location = new System.Drawing.Point(17, 55);
            this.teacher_statistics.Name = "teacher_statistics";
            this.teacher_statistics.Size = new System.Drawing.Size(446, 436);
            this.teacher_statistics.TabIndex = 0;
            this.teacher_statistics.UseCompatibleStateImageBehavior = false;
            this.teacher_statistics.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.h_surname);
            this.groupBox1.Controls.Add(this.h_name);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(96, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 82);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // h_surname
            // 
            this.h_surname.AutoSize = true;
            this.h_surname.Location = new System.Drawing.Point(6, 37);
            this.h_surname.Name = "h_surname";
            this.h_surname.Size = new System.Drawing.Size(51, 20);
            this.h_surname.TabIndex = 1;
            this.h_surname.Text = "label1";
            // 
            // h_name
            // 
            this.h_name.AutoSize = true;
            this.h_name.Location = new System.Drawing.Point(6, 16);
            this.h_name.Name = "h_name";
            this.h_name.Size = new System.Drawing.Size(51, 20);
            this.h_name.TabIndex = 0;
            this.h_name.Text = "label1";
            // 
            // m_student_chart
            // 
            chartArea3.Name = "ChartArea1";
            this.m_student_chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.m_student_chart.Legends.Add(legend3);
            this.m_student_chart.Location = new System.Drawing.Point(371, 116);
            this.m_student_chart.Name = "m_student_chart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Students";
            this.m_student_chart.Series.Add(series3);
            this.m_student_chart.Size = new System.Drawing.Size(415, 388);
            this.m_student_chart.TabIndex = 4;
            this.m_student_chart.Text = "chart1";
            // 
            // new_teacher
            // 
            this.new_teacher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_teacher.Location = new System.Drawing.Point(451, 12);
            this.new_teacher.Name = "new_teacher";
            this.new_teacher.Size = new System.Drawing.Size(135, 73);
            this.new_teacher.TabIndex = 5;
            this.new_teacher.Text = "Create Teacher Account";
            this.new_teacher.UseVisualStyleBackColor = true;
            this.new_teacher.Click += new System.EventHandler(this.new_teacher_Click);
            // 
            // new_student
            // 
            this.new_student.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_student.Location = new System.Drawing.Point(307, 12);
            this.new_student.Name = "new_student";
            this.new_student.Size = new System.Drawing.Size(138, 73);
            this.new_student.TabIndex = 6;
            this.new_student.Text = "Create Student Account";
            this.new_student.UseVisualStyleBackColor = true;
            this.new_student.Click += new System.EventHandler(this.new_student_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(592, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(146, 73);
            this.button4.TabIndex = 7;
            this.button4.Text = "View And Edit Students";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.m_classes);
            this.groupBox2.Controls.Add(this.m_students);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.m_subjects);
            this.groupBox2.Controls.Add(this.m_student_chart);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(552, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(792, 528);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Students\' statistics";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Students:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Choose a class:";
            // 
            // m_classes
            // 
            this.m_classes.FormattingEnabled = true;
            this.m_classes.Location = new System.Drawing.Point(40, 65);
            this.m_classes.Name = "m_classes";
            this.m_classes.Size = new System.Drawing.Size(127, 24);
            this.m_classes.TabIndex = 8;
            // 
            // m_students
            // 
            this.m_students.FormattingEnabled = true;
            this.m_students.ItemHeight = 16;
            this.m_students.Location = new System.Drawing.Point(40, 116);
            this.m_students.Name = "m_students";
            this.m_students.Size = new System.Drawing.Size(325, 388);
            this.m_students.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Choose a subject:";
            // 
            // m_subjects
            // 
            this.m_subjects.FormattingEnabled = true;
            this.m_subjects.Location = new System.Drawing.Point(371, 65);
            this.m_subjects.Name = "m_subjects";
            this.m_subjects.Size = new System.Drawing.Size(235, 24);
            this.m_subjects.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.teacher_statistics);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(12, 113);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(534, 528);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Teachers\' statistics";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(744, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 73);
            this.button1.TabIndex = 10;
            this.button1.Text = "LOG OUT";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ExamSystem_Headmaster.Properties.Resources.exam_system_icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(78, 73);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // T_NAME
            // 
            this.T_NAME.Text = "Name";
            this.T_NAME.Width = 156;
            // 
            // T_SURNAME
            // 
            this.T_SURNAME.Text = "Surname";
            this.T_SURNAME.Width = 162;
            // 
            // T_EXAM_COUNT
            // 
            this.T_EXAM_COUNT.Text = "Exam Count";
            this.T_EXAM_COUNT.Width = 129;
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(702, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 31);
            this.button2.TabIndex = 11;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(1356, 651);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.new_student);
            this.Controls.Add(this.new_teacher);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form2";
            this.Text = "Exam System";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_student_chart)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView teacher_statistics;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label h_surname;
        private System.Windows.Forms.Label h_name;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_student_chart;
        private System.Windows.Forms.Button new_teacher;
        private System.Windows.Forms.Button new_student;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_subjects;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_classes;
        private System.Windows.Forms.ListBox m_students;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader T_NAME;
        private System.Windows.Forms.ColumnHeader T_SURNAME;
        private System.Windows.Forms.ColumnHeader T_EXAM_COUNT;
        private System.Windows.Forms.Button button2;
    }
}