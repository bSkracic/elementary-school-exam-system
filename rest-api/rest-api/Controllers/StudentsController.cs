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
using System.Web.UI;
using rest_api;
using static rest_api.Controllers.DataTransferObject;
using static rest_api.Controllers.LogInSystem;

namespace rest_api.Controllers
{
    public class StudentsController : ApiController
    {
        private ExamEntities db = new ExamEntities();

        public struct StudentLoginResponse
        {
            public InternalCodeResponse Response { get; set; }
            public int? TeacherID { get; set; }

            public StudentLoginResponse(InternalCodeResponse _response, int? _teacherID)
            {
                this.Response = _response;
                this.TeacherID = _teacherID;
            }
        }

        //POST: api/Students/Login
        public HttpResponseMessage StudentLoginAttempt([FromBody]LoginRequest data, [FromUri]int? id)
        {
            Student student = db.Students.Where(x => x.LoginMail == data.Mail).FirstOrDefault();
            if (student != null)
            {
                if (student.LoginPassword == data.Password)
                {
                    InternalCodeResponse response = new InternalCodeResponse(VERIFIED, student.ID);
                    int teacherID = db.Teachers.Where(entry => entry.ClassID == student.ClassID).FirstOrDefault().ID;
                    return Request.CreateResponse(HttpStatusCode.OK, new StudentLoginResponse(response, teacherID));
                }                                                                                 
                else
                {
                    InternalCodeResponse response = new InternalCodeResponse(WRONG_PASSWORD, null);
                    return Request.CreateResponse(HttpStatusCode.OK, new StudentLoginResponse(response, null));
                }
            }
            else
            {
                InternalCodeResponse response = new InternalCodeResponse(NONEXSISTENT_USERNAME, null);
                return Request.CreateResponse(HttpStatusCode.OK, new StudentLoginResponse(response, null));
            }
        }

        // GET: api/Students
        public IQueryable<Student> GetStudents()
        {
            return db.Students;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return Ok();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.ID)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student student = new Student();
            student.Name = studentDTO.Name;
            student.Surname = studentDTO.Surname;
            student.LoginMail = studentDTO.LoginMail;
            student.LoginPassword = studentDTO.LoginPassword;
            student.ClassID = studentDTO.ClassID;

            db.Students.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.ID }, student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.ID == id) > 0;
        }
    }
}