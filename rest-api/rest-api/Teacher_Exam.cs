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
    
    public partial class Teacher_Exam
    {
        public int ID { get; set; }
        public Nullable<int> TeacherID { get; set; }
        public Nullable<int> ExamID { get; set; }
        public System.DateTime DatetimeStart { get; set; }
        public System.DateTime DatetimeEnd { get; set; }
        public double AvailableTime { get; set; }
    
        public virtual Exam Exam { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}