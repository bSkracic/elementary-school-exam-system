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
using static rest_api.Controllers.LogInSystem;

//TODO make TeacherDTO

namespace rest_api.Controllers
{
    public class TeachersController : ApiController
    {
        private ExamEntities db = new ExamEntities();

        // GET: api/Teachers
        public IQueryable<Teacher> GetTeachers()
        {
            return db.Teachers;
        }

        //POST: api/Teachers/Login
        public HttpResponseMessage TeacherLoginAttempt([FromBody]TeacherLogin data, [FromUri]int? id)
        {
            Teacher teacher = db.Teachers.Where(x => x.LoginMail == data.Mail).FirstOrDefault();
            if (teacher != null)
            {
                if (teacher.LoginPassword == data.Password)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new InternalCodeResponse(VERIFIED, teacher.ID));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new InternalCodeResponse(WRONG_PASSWORD, null));
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new InternalCodeResponse(NONEXSISTENT_USERNAME, null));
            }
        }

        // GET: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult GetTeacher(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            TeacherDTO teacherDTO = new TeacherDTO();
            teacherDTO.ID = teacher.ID;
            teacherDTO.ClassID = teacher.ClassID;
            teacherDTO.Class = db.Classes.Find(teacher.ClassID).ClassYear + " " + db.Classes.Find(teacher.ClassID).ClassName;
            teacherDTO.Name = teacher.Name;
            teacherDTO.Surname = teacher.Surname;
            return Ok(teacherDTO);
        }

        // PUT: api/Teachers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacher(int id, Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.ID)
            {
                return BadRequest();
            }

            db.Entry(teacher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult PostTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teachers.Add(teacher);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teacher.ID }, teacher);
        }

        // DELETE: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult DeleteTeacher(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            db.Teachers.Remove(teacher);
            db.SaveChanges();

            return Ok(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherExists(int id)
        {
            return db.Teachers.Count(e => e.ID == id) > 0;
        }
    }
}