using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
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
    public class Exam_QuestionController : ApiController
    {
        private ExamEntities db = new ExamEntities();

        // GET: api/Exam_Question
        public IQueryable<Exam_Question> GetExam_Question()
        {
            return db.Exam_Question;
        }

        // GET: api/Exam_Question/5
        [ResponseType(typeof(List<QuestionDTO>))]
        public IHttpActionResult GetExam_Question(int id)
        {
            //return all of the question ids for exam with 'id'
            List<Exam_Question> exam_Questions = db.Exam_Question.Where(entry => entry.ExamID == id).ToList();
            if (exam_Questions == null)
            {
                return NotFound();
            }

            List<QuestionDTO> questions = new List<QuestionDTO>();
            foreach (var item in exam_Questions)
            {
                if(item.QuestionID != null)
                {
                    Question question = db.Questions.Find(item.QuestionID);
                    questions.Add(new QuestionDTO(question));
                }
            }

            return Ok(questions);
        }

        // PUT: api/Exam_Question/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExam_Question(int id, ExamQuestionDTO examQuestionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != examQuestionDTO.ID)
            {
                return BadRequest();
            }

            Exam_Question exam_Question = db.Exam_Question.Find(id);
            exam_Question.QuestionID = examQuestionDTO.QuestionID;
            exam_Question.ExamID = examQuestionDTO.ExamID;
            exam_Question.QuestionNumber = 0;

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
        [ResponseType(typeof(ExamQuestionDTO))]
        public IHttpActionResult PostExam_Question(ExamQuestionDTO examQuestionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exam_Question exam_Question = new Exam_Question();
            exam_Question.ExamID = examQuestionDTO.ExamID;
            exam_Question.QuestionID = examQuestionDTO.QuestionID;
            exam_Question.QuestionNumber = examQuestionDTO.QuestionNumber;
            db.Exam_Question.Add(exam_Question);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exam_Question.ID }, exam_Question);
        }

        // DELETE: api/Exam_Question/5
        [ResponseType(typeof(ExamQuestionDTO))]
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