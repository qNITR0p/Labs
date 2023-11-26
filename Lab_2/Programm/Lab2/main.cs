using System;

namespace Lab2
{
    class Program
{
   static void Main(string[] args)
   {
       // Создаем экземпляр класса Person с помощью конструктора с тремя параметрами
       Person person1 = new Person("John", "Doe", new DateTime(1990, 1, 1));

       // Выводим информацию о персоне
       Console.WriteLine(person1.ToFullString());

       // Создаем еще одного экземпляра класса Person с помощью конструктора без параметров
       Person person2 = new Person();

       // Выводим информацию о втором человеке
       Console.WriteLine(person2.ToFullString());

       // Изменяем имя и фамилию первого человека и выводим их
       person1.Name = "Jane";
       person1.Surname = "Doe";
       Console.WriteLine(person1.ToShortString());

       // Изменяем год рождения первого человека и выводим его
       person1.BirthYear = 1992;
       Console.WriteLine(person1.ToFullString());
       
       // Создаем экземпляр класса Student с помощью конструктора без параметров
      Student student1 = new Student();

      // Выводим информацию о студенте
      Console.WriteLine(student1.ToShortString());

      // Создаем экземпляр класса Student с помощью конструктора с тремя параметрами
      Person person = new Person("John", "Doe", new DateTime(1990, 1, 1));
      Student student2 = new Student(person, Education.Bachelor, 1);

      // Выводим информацию о студенте
      Console.WriteLine(student2.ToFullString());

      // Добавляем экзамены в список экзаменов
      student2.AddExams(new Exam("Math", 5, new DateTime(2023, 1, 1)), new Exam("Physics", 4, new DateTime(2023, 2, 1)));

      // Выводим информацию о студенте после добавления экзаменов
      Console.WriteLine(student2.ToFullString());
   }
}
}