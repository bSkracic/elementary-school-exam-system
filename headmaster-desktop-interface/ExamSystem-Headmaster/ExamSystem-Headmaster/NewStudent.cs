using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ExamSystem_Headmaster
{
    public partial class NewStudent : Form
    {

        public Dictionary<string, int> ClassDict = new Dictionary<string, int>();

        public NewStudent()
        {
            InitializeComponent();
        }

        private void NewStudent_Load(object sender, EventArgs e)
        {
            PopulateClasses();
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
            }
            List<Class> _classes = new JavaScriptSerializer().Deserialize<List<Class>>(response);

            s_classes.Items.Clear();
            foreach (var _class in _classes)
            {
                string className = _class.ClassYear + "-" + _class.ClassName;
                ClassDict.Add(className, _class.ID);
                s_classes.Items.Add(className);
            }
        }

        private void s_submit_Click(object sender, EventArgs e)
        {
            if(s_name.Text == "" || s_surname.Text == "" || s_email.Text == "" || s_password.Text == "")
            {
                MessageBox.Show("Neither field can be empty!", "Invalid Input", MessageBoxButtons.OK);
                return;
            }

            string selectedClass = "";
            try
            {
                selectedClass = s_classes.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Class must be selected!", "Invalid Input", MessageBoxButtons.OK);
                return;
            }

            StudentDTO body = new StudentDTO();
            body.Name = s_name.Text;
            body.Surname = s_surname.Text;
            body.LoginMail = s_email.Text;
            body.LoginPassword = s_password.Text;
            body.ClassID = ClassDict[selectedClass];

            int code = 0;
            try
            {
                var httpPOSTrequest = (HttpWebRequest)WebRequest.Create(ExamAPI.StudentURL);
                httpPOSTrequest.ContentType = "application/json";
                httpPOSTrequest.Method = "POST";

                using (var sw = new StreamWriter(httpPOSTrequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(body);
                    sw.Write(json);
                }

                var response = (HttpWebResponse)httpPOSTrequest.GetResponse();
                string result;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }

                code = (int)response.StatusCode;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Network error!", "Error", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                if(code == 201)
                {
                    MessageBox.Show("Account created successufly!", "Changes Saved", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Login Email must be unique!", "Error", MessageBoxButtons.OK);
                }
                
            }
        }
    }
}
