using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExamSystem_Headmaster
{
    struct LoginRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public LoginRequest(string _mail, string _password)
        {
            this.Mail = _mail;
            this.Password = _password;
        }
    }

    struct LoginResponse
    {
        public int? UserID { get; set; }
        public int Code { get; set; }
        public LoginResponse(int _userID, int _code)
        {
            this.UserID = _userID;
            this.Code = _code;
        }
    }

    public class Class
    {
        public int ID { get; set; }
        public string ClassYear { get; set; }
        public string ClassName { get; set; }
    }

    public class ExamDataDTO
    {
        public int SubjectID { get; set; }
        public int ClassID { get; set; }
        public int PointScorred { get; set; }
        public int MaxPoints { get; set; }

        public ExamDataDTO() { }

        public ExamDataDTO(int _subjectID, int _classID, int _pointsScorred, int _maxPoints)
        {
            this.SubjectID = _subjectID;
            this.ClassID = _classID;
            this.PointScorred = _pointsScorred;
            this.MaxPoints = _maxPoints;
        }
    }

    public class StudentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LoginMail { get; set; }
        public string LoginPassword { get; set; }
        public int ClassID { get; set; }
    }

    public class SubjectDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class TeacherStatistic
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ExamCount { get; set; }
    }

    public class TSResponse 
    {
        public List<TeacherStatistic> Statistics { get; set; }   
    }

    class ExamAPI
    {
        public const string SubjectURL = "https://hk-iot-team-02.azurewebsites.net/api/Subjects";
        public const string LoginURL = "https://hk-iot-team-02.azurewebsites.net/api/Headmasters/Login";
        public const string ClassesURL = "https://hk-iot-team-02.azurewebsites.net/api/Classes";
        public const string HeadMasterURL = "https://hk-iot-team-02.azurewebsites.net/api/Headmasters/";
        public const string TeacherStatisticsURL = "https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam";
        public const string StudentURL = "https://hk-iot-team-02.azurewebsites.net/api/Students/";
    }
    
    class HTTP
    {
        /// <summary>
        /// Send HTTP 'POST' request to designated URL. 
        /// Returns response string if specifed by server.
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_url"></param>
        /// <returns></returns>
        public static async Task<string> POST(object _data, string _url)
        {
            var httpPOSTrequest = (HttpWebRequest)WebRequest.Create(_url);
            httpPOSTrequest.ContentType = "application/json";
            httpPOSTrequest.Method = "POST";

            using (var sw = new StreamWriter(httpPOSTrequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(_data);
                sw.Write(json);
            }

            var response = (HttpWebResponse)httpPOSTrequest.GetResponse();
            string result;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Send HTTP 'GET' request to designated URL.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_url"></param>
        /// <returns></returns>
        public static async Task<string> GET(int _id, string _url)
        {
            if(_id != 0)
            {
                _url += _id.ToString();
            }

            var httpGETrequest = (HttpWebRequest)WebRequest.Create(_url);

            var response = (HttpWebResponse)httpGETrequest.GetResponse();
            string result;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Send HTTP 'PUT' request to designated URL.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_url"></param>
        /// <returns></returns>
        public static async Task<string> PUT(object _data, string _url)
        {
            var httpPOSTrequest = (HttpWebRequest)WebRequest.Create(_url);
            httpPOSTrequest.ContentType = "application/json";
            httpPOSTrequest.Method = "PUT";

            using (var sw = new StreamWriter(httpPOSTrequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(_data);
                sw.Write(json);
            }

            var response = (HttpWebResponse)httpPOSTrequest.GetResponse();
            string result;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }
    }
}
