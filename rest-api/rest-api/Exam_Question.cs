//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rest_api
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exam_Question
    {
        public int ID { get; set; }
        public Nullable<int> ExamID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public Nullable<int> QuestionNumber { get; set; }
    
        public virtual Exam Exam { get; set; }
        public virtual Question Question { get; set; }
    }
}
