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
using Microsoft.Ajax.Utilities;
using rest_api;
using static rest_api.Controllers.DataTransferObject;

namespace rest_api.Controllers
{
    public class QuestionsController : ApiController
    {
        private ExamEntities db = new ExamEntities();

        // GET: api/Questions
        public List<QuestionDTO> GetQuestions()
        {
            List<Question> result = db.Questions.ToList();
            List<QuestionDTO> questionList = new List<QuestionDTO>();
            foreach (var question in result)
            {
                questionList.Add(new QuestionDTO(question));
            }
            return questionList;
        }

        public struct QuestionRequest
        {
            public List<int> values { get; set; }
        }

        // POST: api/Questions/request-questions-for-exam
        public HttpResponseMessage RetrieveQuestionsForExam([FromBody]QuestionRequest request, int? id)
        {
            List<int> query = request.values;
            List<Question> result = db.Questions.Where(question => query.Contains(question.ID)).ToList();
            List<QuestionDTO> questionList = new List<QuestionDTO>();
            foreach (var question in result)
            {
                questionList.Add(new QuestionDTO(question));
            }
            return Request.CreateResponse(HttpStatusCode.OK, questionList);
        }

        // GET: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(new QuestionDTO(question));
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(int id, QuestionDTO questionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (questionDTO.ID == null || id != questionDTO.ID)
            {
                return BadRequest();
            }

            Question question = db.Questions.Find(questionDTO.ID);
            question.ID = (int) questionDTO.ID;
            question.Text = questionDTO.Text;
            question.FreeAnswerType = questionDTO.FreeAnswerType;
            question.A = questionDTO.A;
            question.B = questionDTO.B;
            question.C = questionDTO.C;
            question.D = questionDTO.D;
            question.CorrectAnswer = questionDTO.CorrectAnswer;
            question.MaxPoints = questionDTO.MaxPoints;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/Questions
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(QuestionDTO questionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Question question = new Question();
            if (questionDTO.ID != null)
            {
                question.ID = (int)questionDTO.ID;
            }
            question.Text = questionDTO.Text;
            question.FreeAnswerType = questionDTO.FreeAnswerType;
            question.A = questionDTO.A;
            question.B = questionDTO.B;
            question.C = questionDTO.C;
            question.D = questionDTO.D;
            question.CorrectAnswer = questionDTO.CorrectAnswer;
            question.MaxPoints = questionDTO.MaxPoints;
            db.Questions.Add(question);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = question.ID }, question);
        }



        // DELETE: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            List<Exam_Question> relations = db.Exam_Question.Where(entry => entry.QuestionID == id).ToList();

            foreach (var relation in relations)
            {
                db.Exam_Question.Remove(relation);
            }

            db.Questions.Remove(question);
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(int id)
        {
            return db.Questions.Count(e => e.ID == id) > 0;
        }
    }
}