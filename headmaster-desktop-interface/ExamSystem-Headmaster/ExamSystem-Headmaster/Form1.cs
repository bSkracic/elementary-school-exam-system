using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExamSystem_Headmaster
{
    public partial class Form1 : Form 
    {
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void login_submit_Click(object sender, EventArgs e)
        {
            sendLoginRequestAsync();
        }

        /// <summary>
        ///  Send login data to server and receive status code.
        /// </summary> 
        private async Task sendLoginRequestAsync()
        {
            LoginRequest loginRequest = new LoginRequest(this.input_email.Text, this.input_password.Text);
            var response = await HTTP.POST(loginRequest, ExamAPI.LoginURL);
            LoginResponse loginResposne = new JavaScriptSerializer().Deserialize<LoginResponse>(response);
            handleResponse(loginResposne);
        }

        private void handleResponse(LoginResponse _loginResponse)
        {
            switch (_loginResponse.Code)
            {
                case -2:
                    MessageBox.Show("Username does not exist!","Login Failed.", MessageBoxButtons.OK);
                    break;
                case -1:
                    MessageBox.Show("Wrong password!", "Login Failed.", MessageBoxButtons.OK);
                    break;
                case 0:
                    Form2 form2 = new Form2();
                    form2.Show();
                    form2.Closed += (s, args) => this.Close();
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("Network error!", "Login Failed.", MessageBoxButtons.OK);
                    break;
            }
        }

    }
}
