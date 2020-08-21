using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using rest_api;
using static rest_api.Controllers.DataTransferObject;

namespace rest_api.Controllers
{
    public class Teacher_ExamController : ApiController
    {
        private ExamEntities db = new ExamEntities();

        // GET: api/Teacher_Exam
        public List<TeacherExamDTO> GetTeacher_Exam()
        {
            List<Teacher_Exam> result = db.Teacher_Exam.ToList();
            List<TeacherExamDTO> teacherExams = new List<TeacherExamDTO>();
            foreach (var item in result)
            {
                teacherExams.Add(new TeacherExamDTO(item));
            }
            return teacherExams;
        }        

        // GET: api/Teacher_Exam/5
        [ResponseType(typeof(Teacher_Exam))]
        public IHttpActionResult GetTeacher_Exam(int id)
        {
            //returns all exams set by teahcer with 'id'
            List<Teacher_Exam> teacher_Exams = db.Teacher_Exam.Where(examEntry => examEntry.TeacherID == id).ToList();
            
            if (teacher_Exams == null)
            {
                return NotFound();
            }

            List<ScheduledExam> result = new List<ScheduledExam>();
            foreach (var teacherExam in teacher_Exams)
            {
                ScheduledExam entry = new ScheduledExam(teacherExam);
                entry.ExamTitle = db.Exams.Find(teacherExam.ExamID).Title;
                entry.SubjectID = db.Exams.Find(teacherExam.ExamID).SubjectID;
                entry.Subject = db.Subjects.Find(entry.SubjectID).Name;

                result.Add(entry); 
            }

            return Ok(result);
        }

        // PUT: api/Teacher_Exam/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacher_Exam(int id, TeacherExamDTO teacherExamDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (teacherExamDTO.ID != null || id != teacherExamDTO.ID)
            {
                return BadRequest();
            }

            Teacher_Exam teacherExam = db.Teacher_Exam.Find(teacherExamDTO.ID);
            teacherExam.TeacherID = teacherExamDTO.TeacherID;
            teacherExam.ExamID = teacherExamDTO.ExamID;
            teacherExam.DatetimeStart = teacherExamDTO.DatetimeStart;
            teacherExam.DatetimeEnd = teacherExamDTO.DatetimeEnd;
            teacherExam.AvailableTime = teacherExamDTO.AvailableTime;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Teacher_ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Teacher_Exam
        [ResponseType(typeof(Teacher_Exam))]
        public IHttpActionResult PostTeacher_Exam(TeacherExamDTO teacherExamDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Teacher_Exam teacherExam = new Teacher_Exam();
            if (teacherExamDTO.ID != null)
            {
                teacherExam.ID = (int) teacherExamDTO.ID;
            }
            teacherExam.TeacherID = teacherExamDTO.TeacherID;
            teacherExam.ExamID = teacherExamDTO.ExamID;
            teacherExam.DatetimeStart = teacherExamDTO.DatetimeStart;
            teacherExam.DatetimeEnd = teacherExamDTO.DatetimeEnd;
            teacherExam.AvailableTime = teacherExamDTO.AvailableTime;

            db.Teacher_Exam.Add(teacherExam);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teacherExamDTO.ID }, teacherExamDTO);
        }

        // DELETE: api/Teacher_Exam/5
        [ResponseType(typeof(Teacher_Exam))]
        public IHttpActionResult DeleteTeacher_Exam(int id)
        {
            Teacher_Exam teacher_Exam = db.Teacher_Exam.Find(id);
            if (teacher_Exam == null)
            {
                return NotFound();
            }

            db.Teacher_Exam.Remove(teacher_Exam);
            db.SaveChanges();

            return Ok(teacher_Exam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Teacher_ExamExists(int id)
        {
            return db.Teacher_Exam.Count(e => e.ID == id) > 0;
        }
    }
}