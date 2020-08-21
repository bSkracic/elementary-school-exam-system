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
    public class ExamsController : ApiController
    {
        private ExamEntities db = new ExamEntities();

        // GET: api/Exams
        public List<ExamDTO> GetExams()
        {
            List<Exam> exams = db.Exams.ToList<Exam>();
            List<ExamDTO> examDTOs = new List<ExamDTO>();
            foreach(var exam in exams)
            {
                ExamDTO examDTO = new ExamDTO(exam);
                examDTO.Subject = db.Subjects.Find(exam.SubjectID).Name;
                examDTOs.Add(examDTO);
            }
            return examDTOs;
        }

        // GET: api/Exams/5
        [ResponseType(typeof(Exam))]
        public IHttpActionResult GetExam(int id)
        {
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

        // PUT: api/Exams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExam(int id, Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exam.ID)
            {
                return BadRequest();
            }

            db.Entry(exam).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
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

        // POST: api/Exams
        [ResponseType(typeof(Exam))]
        public IHttpActionResult PostExam(Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exams.Add(exam);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exam.ID }, exam);
        }

        // DELETE: api/Exams/5
        [ResponseType(typeof(Exam))]
        public IHttpActionResult DeleteExam(int id)
        {
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return NotFound();
            }

            db.Exams.Remove(exam);
            db.SaveChanges();

            return Ok(exam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamExists(int id)
        {
            return db.Exams.Count(e => e.ID == id) > 0;
        }
    }
}