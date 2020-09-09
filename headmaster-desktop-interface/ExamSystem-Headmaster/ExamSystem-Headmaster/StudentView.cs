using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ExamSystem_Headmaster
{
    public partial class StudentView : Form
    {
        public Dictionary<string, int> ClassDict = new Dictionary<string, int>();
        public Dictionary<int, StudentDTO> StudentDict = new Dictionary<int, StudentDTO>();

        public StudentView()
        {
            InitializeComponent();
        }

        private void StudentView_Load(object sender, EventArgs e)
        {
            PopulateClasses();
            PopulateStudents();
        }

        private async Task PopulateStudents()
        {
            HttpClient client = new HttpClient();
            string response = "";
            try
            {
                response = await client.GetStringAsync(ExamAPI.StudentURL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Network error!", "Error", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                List<StudentDTO> _students = new JavaScriptSerializer().Deserialize<List<StudentDTO>>(response);

                StudentDict.Clear();
                sv_students.Items.Clear();
                foreach (var student in _students)
                {
                    string className = "";
                    foreach(var _class in ClassDict)
                    {
                        if(student.ClassID == _class.Value)
                        {
                            className = _class.Key;
                            break;
                        }
                    }

                    sv_students.Items.Add(new ListViewItem(new string[] { student.ID.ToString(), student.Name, student.Surname, className }));
                    StudentDict.Add(student.ID, student);
                }
            }
        }

        private async Task PopulateClasses()
        {
            HttpClient client = new HttpClient();
            string response = "";
            try
            {
                response = await client.GetStringAsync(ExamAPI.ClassesURL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Network error!", "Error", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                List<Class> _classes = new JavaScriptSerializer().Deserialize<List<Class>>(response);

                sv_class.Items.Clear();
                foreach (var _class in _classes)
                {
                    string className = _class.ClassYear + "-" + _class.ClassName;
                    ClassDict.Add(className, _class.ID);
                    sv_class.Items.Add(className);
                }
            }
        }

        private void sv_students_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sv_students.SelectedItems.Count == 0)
            {
                return;
            }
            int id = int.Parse(sv_students.SelectedItems[0].Text);
            var student = StudentDict[id];
            sv_name.Text = student.Name;
            sv_surname.Text = student.Surname;
            sv_mail.Text = student.LoginMail;
            sv_password.Text = student.LoginPassword;
            string className = "";
            foreach (var _class in ClassDict)
            {
                if (_class.Value == student.ClassID)
                {
                    className = _class.Key;
                    break;
                }
            }
            sv_class.SelectedItem = className;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(sv_name.Text == "" || sv_surname.Text == "" || sv_mail.Text == "" || sv_password.Text == "" || sv_class.SelectedItem == null)
            {
                MessageBox.Show("Neither field can be empty!", "Invalid Input", MessageBoxButtons.OK);
                return;
            }

            int id = int.Parse(sv_students.SelectedItems[0].Text);

            StudentDTO body = new StudentDTO();
            body.ID = id;
            body.Name = sv_name.Text;
            body.Surname = sv_surname.Text;
            body.LoginMail = sv_mail.Text;
            body.LoginPassword = sv_password.Text;
            body.ClassID = ClassDict[sv_class.SelectedItem.ToString()];

            Task<string> response;
            try
            {
                response = HTTP.PUT(body, ExamAPI.StudentURL + id);
            }catch(Exception ex)
            {
                MessageBox.Show("Network error!", "Error", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                MessageBox.Show("Changes Saved!", "Success", MessageBoxButtons.OK);
                PopulateStudents();
            }

        }
    }
}
