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

namespace rest_api.Controllers
{
    public class Exam_QuestionController : ApiController
    {
        private ExamEntities db = new ExamEntities();

        // GET: api/Exam_Question
        public IQueryable<Exam_Question> GetExam_Question()
        {
            return db.Exam_Question;
        }

        // GET: api/Exam_Question/5
        [ResponseType(typeof(Exam_Question))]
        public IHttpActionResult GetExam_Question(int id)
        {
            //return all of the question ids for exam with 'id'
            List<Exam_Question> exam_Questions = db.Exam_Question.Where(entry => entry.ExamID == id).ToList<Exam_Question>();
            if (exam_Questions == null)
            {
                return NotFound();
            }

            List<int> IDs = new List<int>();
            foreach (var item in exam_Questions)
            {
                if(item.QuestionID != null)
                {
                    IDs.Add((int)item.QuestionID);
                }
            }

            return Ok(IDs);
        }

        // PUT: api/Exam_Question/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExam_Question(int id, Exam_Question exam_Question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exam_Question.ID)
            {
                return BadRequest();
            }

            db.Entry(exam_Question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exam_QuestionExists(id))
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

        // POST: api/Exam_Question
        [ResponseType(typeof(Exam_Question))]
        public IHttpActionResult PostExam_Question(Exam_Question exam_Question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exam_Question.Add(exam_Question);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exam_Question.ID }, exam_Question);
        }

        // DELETE: api/Exam_Question/5
        [ResponseType(typeof(Exam_Question))]
        public IHttpActionResult DeleteExam_Question(int id)
        {
            Exam_Question exam_Question = db.Exam_Question.Find(id);
            if (exam_Question == null)
            {
                return NotFound();
            }

            db.Exam_Question.Remove(exam_Question);
            db.SaveChanges();

            return Ok(exam_Question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Exam_QuestionExists(int id)
        {
            return db.Exam_Question.Count(e => e.ID == id) > 0;
        }
    }
}