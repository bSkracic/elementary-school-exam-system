namespace ExamSystem_Headmaster
{
    partial class Form1
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
            this.input_email = new System.Windows.Forms.TextBox();
            this.input_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.login_submit = new System.Windows.Forms.Button();
            this.exam_logo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.exam_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // input_email
            // 
            this.input_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input_email.Location = new System.Drawing.Point(124, 163);
            this.input_email.Name = "input_email";
            this.input_email.Size = new System.Drawing.Size(220, 26);
            this.input_email.TabIndex = 0;
            // 
            // input_password
            // 
            this.input_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input_password.Location = new System.Drawing.Point(124, 221);
            this.input_password.Name = "input_password";
            this.input_password.PasswordChar = '*';
            this.input_password.Size = new System.Drawing.Size(220, 26);
            this.input_password.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(121, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(121, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Email:";
            // 
            // login_submit
            // 
            this.login_submit.BackColor = System.Drawing.Color.White;
            this.login_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_submit.ForeColor = System.Drawing.Color.Green;
            this.login_submit.Location = new System.Drawing.Point(124, 274);
            this.login_submit.Name = "login_submit";
            this.login_submit.Size = new System.Drawing.Size(220, 38);
            this.login_submit.TabIndex = 5;
            this.login_submit.Text = "LOG IN";
            this.login_submit.UseVisualStyleBackColor = false;
            this.login_submit.Click += new System.EventHandler(this.login_submit_Click);
            // 
            // exam_logo
            // 
            this.exam_logo.BackgroundImage = global::ExamSystem_Headmaster.Properties.Resources.exam_system_icon;
            this.exam_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exam_logo.Location = new System.Drawing.Point(170, 12);
            this.exam_logo.Name = "exam_logo";
            this.exam_logo.Size = new System.Drawing.Size(124, 112);
            this.exam_logo.TabIndex = 6;
            this.exam_logo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(458, 353);
            this.Controls.Add(this.exam_logo);
            this.Controls.Add(this.login_submit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.input_password);
            this.Controls.Add(this.input_email);
            this.Name = "Form1";
            this.Text = "Exam System";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.exam_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input_email;
        private System.Windows.Forms.TextBox input_password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button login_submit;
        private System.Windows.Forms.PictureBox exam_logo;
    }
}

