using System;
using System.Linq;

namespace Lab2
{
public enum Education
{
   Specialist,
   Bachelor,
   SecondEducation
}

public class Exam
{
   public string Subject { get; set; }
   public int Score { get; set; }
   public DateTime ExamDate { get; set; }

   public Exam(string subject, int score, DateTime examDate)
   {
       Subject = subject;
       Score = score;
       ExamDate = examDate;
   }

   public Exam()
   {
       Subject = "Default";
       Score = 0;
       ExamDate = DateTime.Now;
   }

   public string ToFullString()
   {
       return $"Subject: {Subject}, Score: {Score}, ExamDate: {ExamDate}";
   }
}

public class Student
{
   private Person person;
   private Education education;
   private int groupNumber;
   private Exam[] exams;

   public Student(Person person, Education education, int groupNumber)
   {
       this.person = person;
       this.education = education;
       this.groupNumber = groupNumber;
       this.exams = new Exam[0];
   }

   public Student()
   {
       this.person = new Person();
       this.education = Education.Bachelor;
       this.groupNumber = 0;
       this.exams = new Exam[0];
   }

   public Person Person
   {
       get { return person; }
       set { person = value; }
   }

   public Education Education
   {
       get { return education; }
       set { education = value; }
   }

   public int GroupNumber
   {
       get { return groupNumber; }
       set { groupNumber = value; }
   }

   public Exam[] Exams
   {
       get { return exams; }
       set { exams = value; }
   }

   public double AverageScore
   {
       get
       {
           if (exams.Length == 0)
               return 0;

           return exams.Average(exam => exam.Score);
       }
   }

   public void AddExams(params Exam[] examsToAdd)
   {
       var newExams = new Exam[exams.Length + examsToAdd.Length];
       exams.CopyTo(newExams, 0);
       examsToAdd.CopyTo(newExams, exams.Length);
       exams = newExams;
   }

   public string ToFullString()
   {
       var examsString = string.Join(", ", exams.Select(exam => exam.ToFullString()));
       return $"Person: {person.ToFullString()}, Education: {education}, GroupNumber: {groupNumber}, Exams: {examsString}";
   }

   public string ToShortString()
   {
       return $"Person: {person.ToShortString()}, Education: {education}, GroupNumber: {groupNumber}, AverageScore: {AverageScore}";
   }
}

}