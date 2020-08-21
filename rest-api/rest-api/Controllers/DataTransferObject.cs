using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rest_api.Controllers
{
    public class DataTransferObject
    {
        public class QuestionDTO
        {
            //members
            public int? ID { get; set; }
            public string Text { get; set; }
            public bool FreeAnswerType { get; set; }
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string CorrectAnswer { get; set; }
            public double MaxPoints { get; set; }
            //constructor
            public QuestionDTO(Question question)
            {
                if (question != null)
                {
                    this.ID = question.ID;
                    this.Text = question.Text;
                    this.FreeAnswerType = question.FreeAnswerType;
                    this.A = question.A;
                    this.B = question.B;
                    this.C = question.C;
                    this.D = question.D;
                    this.CorrectAnswer = question.CorrectAnswer;
                    this.MaxPoints = question.MaxPoints;
                }
            }
        }

        public class TeacherDTO
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public Nullable<int> ClassID { get; set; }
            public string Class { get; set; }
        }

        public class ExamDTO
        {
            //members
            public int ID { get; set; }
            public string Title { get; set; }
            public Nullable<int> SubjectID { get; set; }
            public string Subject { get; set; }
            //constructor
            public ExamDTO(Exam exam)
            {
                this.ID = exam.ID;
                this.Title = exam.Title;
                this.SubjectID = exam.SubjectID;
            }
        }

        public class TeacherExamDTO
        {
            //members
            public int? ID { get; set; }
            public int TeacherID { get; set; }
            public int ExamID { get; set; }
            public System.DateTime DatetimeStart { get; set; }
            public System.DateTime DatetimeEnd { get; set; }
            public double AvailableTime { get; set; }
            //constructor
            public TeacherExamDTO(Teacher_Exam teacherExam)
            {
                if (teacherExam != null)
                {
                    this.ID = teacherExam.ID;
                    this.TeacherID = (int)teacherExam.TeacherID;
                    this.ExamID = (int)teacherExam.ExamID;
                    this.DatetimeStart = teacherExam.DatetimeStart;
                    this.DatetimeEnd = teacherExam.DatetimeEnd;
                    this.AvailableTime = teacherExam.AvailableTime;
                }
            }
        }

        public class ScheduledExam
        {
            public int? ID { get; set; }
            public System.DateTime DatetimeStart { get; set; }
            public System.DateTime DatetimeEnd { get; set; }
            public double AvailableTime { get; set; }
            public string ExamTitle { get; set; }
            public int? SubjectID { get; set; }
            public string Subject { get; set; }

            public ScheduledExam(Teacher_Exam teacherExam)
            {
                if (teacherExam != null)
                {
                    this.ID = teacherExam.ID;
                    this.DatetimeStart = teacherExam.DatetimeStart;
                    this.DatetimeEnd = teacherExam.DatetimeEnd;
                    this.AvailableTime = teacherExam.AvailableTime;
                }
            }
        }
    }
}