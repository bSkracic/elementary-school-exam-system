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

        public struct LoginRequest
        {
            public string Mail;
            public string Password;
        }

        public struct InternalCodeResponse
        {
            public int Code { get; set; }
            public int? UserID { get; set; }

            public InternalCodeResponse(int _code, int? _userID)
            {
                this.Code = _code;
                this.UserID = _userID;
            }
        }


    }
}