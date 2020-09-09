using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ExamSystem_Headmaster
{
    public partial class Form2 : Form
    {

        public Dictionary<string, int> ClassDict = new Dictionary<string, int>();
        public Dictionary<string, int> SubjectDict = new Dictionary<string, int>();
        public List<ExamDataDTO> ExamData = new List<ExamDataDTO>();
        public List<StudentDTO> students = new List<StudentDTO>();

        public class Headmaster
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string LoginMail { get; set; }
            public string LoginPassword { get; set; }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RetrieveExamData();
            PopulateDetails();
            PopulateStudents();
            PopulateTeacherStatistics();
            PopulateClasses();
            PopulateSubjects();
            // Set listeners
            m_classes.SelectionChangeCommitted += UpdateStudentStatistics;
            m_subjects.SelectionChangeCommitted += UpdateStudentStatistics;
        }

        // Doesnt work
        private async Task PopulateTeacherStatistics()
        {
            string response = "";
            try
            {
                var client = new HttpClient();
                response = await client.GetStringAsync(ExamAPI.TeacherStatisticsURL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Network Error!", "Error", MessageBoxButtons.OK);

            }
            finally
            {
                var statistics = new JavaScriptSerializer().Deserialize<List<TeacherStatistic>>(response);

                teacher_statistics.Items.Clear();
                foreach (var teacher in statistics)
                {
                    var name = teacher.Name;
                    var surname = teacher.Surname;
                    var examCount = teacher.ExamCount.ToString();
                    teacher_statistics.Items.Add(new ListViewItem(new string[] { name, surname, examCount }));
                }

            }

        }

        private async Task PopulateDetails()
        {
            string response = "";
            try
            {
                var client = new HttpClient();
                response = await HTTP.GET(1, ExamAPI.HeadMasterURL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Network Error!", "Error", MessageBoxButtons.OK);
            }
            finally
            {
                var headmaster = new JavaScriptSerializer().Deserialize<Headmaster>(response);
                h_name.Text = "Name: " + headmaster.Name;
                h_surname.Text = "Surname: " + headmaster.Surname;
            }

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

                students.Clear();
                m_students.Items.Clear();
                foreach (var student in _students)
                {
                    m_students.Items.Add(student.Name + " " + student.Surname);
                    students.Add(student);
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

                m_classes.Items.Clear();
                foreach (var _class in _classes)
                {
                    string className = _class.ClassYear + "-" + _class.ClassName;
                    ClassDict.Add(className, _class.ID);
                    m_classes.Items.Add(className);
                }
                m_classes.SelectedItem = ClassDict.Keys.ElementAt(0);
            }
        }

        private async Task PopulateSubjects()
        {
            HttpClient client = new HttpClient();
            string response = "";
            try
            {
                response = await client.GetStringAsync(ExamAPI.SubjectURL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Network error!", "Error", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                m_subjects.Items.Clear();
                List<SubjectDTO> subjects = new JavaScriptSerializer().Deserialize<List<SubjectDTO>>(response);
                foreach (var subject in subjects)
                {
                    SubjectDict.Add(subject.Name, subject.ID);
                    m_subjects.Items.Add(subject.Name);
                }
                m_subjects.SelectedItem = SubjectDict.Keys.ElementAt(0);
            }
        }

        private async Task RetrieveExamData()
        {
            HttpClient client = new HttpClient();
            string response = "";
            try
            {
                response = await client.GetStringAsync(ExamAPI.ClassesURL + "/0");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Network error!", "Error", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                m_subjects.Items.Clear();
                ExamData = new JavaScriptSerializer().Deserialize<List<ExamDataDTO>>(response);
            }
        }

        private void new_student_Click(object sender, EventArgs e)
        {
            NewStudent newStudent = new NewStudent();
            newStudent.ShowDialog();
        }

        private void new_teacher_Click(object sender, EventArgs e)
        {

        }

        private void UpdateStudentStatistics(object sender, EventArgs e)
        {
            ExamData.Add(new ExamDataDTO(1, 1, 4, 4));
            ExamData.Add(new ExamDataDTO(1, 1, 2, 4));
            ExamData.Add(new ExamDataDTO(1, 1, 1, 4));
            ExamData.Add(new ExamDataDTO(1, 1, 3, 4));
            ExamData.Add(new ExamDataDTO(1, 1, 4, 4));
            ExamData.Add(new ExamDataDTO(1, 1, 4, 4));

            var selectedSubject = m_subjects.SelectedItem;
            var selectedClass = m_classes.SelectedItem;

            int subjectID = 0;
            int classID = 0;
            if (selectedSubject != null)
            {
                subjectID = SubjectDict[m_subjects.SelectedItem.ToString()];
            }
            if (selectedClass != null)
            {
                classID = ClassDict[m_classes.SelectedItem.ToString()];
            }


            m_students.Items.Clear();
            foreach (var student in students)
            {
                if (student.ClassID == classID)
                {
                    m_students.Items.Add(student.Name + " " + student.Surname);
                }
            }
            
            List<ExamDataDTO> filtered = new List<ExamDataDTO>();
            foreach (var entry in ExamData)
            {
                if (entry.SubjectID == subjectID && entry.ClassID == classID)
                {
                    filtered.Add(entry);
                }
            }
            
            

            Dictionary<string, int> performance = new Dictionary<string, int>();
            performance.Add("<50%", 0);
            performance.Add("50%-70%", 0);
            performance.Add("70%-90%", 0);
            performance.Add("90%-100%", 0);

            foreach (var entry in filtered)
            {
                if (entry.MaxPoints == 0) {
                    continue;
                }
                var achievedPercent = entry.PointScorred / entry.MaxPoints;
                if (achievedPercent < 0.5)
                {
                    performance["<50%"] += 1;
                }
                else if (achievedPercent >= 0.5 && achievedPercent < 0.7)
                {
                    performance["50%-70%"] += 1;
                }
                else if (achievedPercent >= 0.7 && achievedPercent < 0.9)
                {
                    performance["70%-90%"] += 1;
                }
                else
                {
                    performance["90%-100%"] += 1;
                }
            }

            m_student_chart.Series["Students"].Points.Clear();
            foreach (var entry in performance)
            {
                m_student_chart.Series["Students"].Points.AddXY(entry.Key, entry.Value);
                Console.WriteLine(entry.Key + ":" + entry.Value);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StudentView sw = new StudentView();
            sw.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PopulateStudents();
            PopulateTeacherStatistics();
        }
    }    
}
