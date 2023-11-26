using System;

namespace Lab2
{
    public class Person
{
   private string name;
   private string surname;
   private DateTime birthDate;

   // Конструктор с тремя параметрами
   public Person(string name, string surname, DateTime birthDate)
   {
       this.name = name;
       this.surname = surname;
       this.birthDate = birthDate;
   }

   // Конструктор без параметров
   public Person()
   {
       this.name = "Default";
       this.surname = "Default";
       this.birthDate = DateTime.Now;
   }

   // Свойства с методами get и set
   public string Name
   {
       get { return name; }
       set { name = value; }
   }

   public string Surname
   {
       get { return surname; }
       set { surname = value; }
   }

   public DateTime BirthDate
   {
       get { return birthDate; }
       set { birthDate = value; }
   }

   public int BirthYear
   {
       get { return birthDate.Year; }
       set { birthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
   }

   // Методы
   public string ToFullString()
   {
       return $"Name: {name}, Surname: {surname}, BirthDate: {birthDate}";
   }

   public string ToShortString()
   {
       return $"Name: {name}, Surname: {surname}";
   }
}

}
