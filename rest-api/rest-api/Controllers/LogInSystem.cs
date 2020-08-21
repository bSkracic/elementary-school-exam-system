using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rest_api.Controllers
{
    public class LogInSystem
    {
        public static int NONEXSISTENT_USERNAME = -2;
        public static int WRONG_PASSWORD = -1;
        public static int VERIFIED = 0;

        public struct TeacherLogin
        {
            public string Mail;
            public string Password;
        }

        public struct InternalCodeResponse
        {
            public int Code { get; set; }
            public int? TeacherID { get; set; }

            public InternalCodeResponse(int _code, int? _teacherID)
            {
                this.Code = _code;
                this.TeacherID = _teacherID;
            }
        }
        
        //public class TeacherDTO, headmaster stuff
        //public class StudentDTO, headmaster stuff
    }
}