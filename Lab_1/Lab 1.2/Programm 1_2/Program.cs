using System;

Console.Write("Введите год, для проверки на високосность: ");
int number = Convert.ToInt32(Console.ReadLine());

if ((number % 4 == 0 && number % 100 != 0) || (number % 400 == 0))
{
  Console.WriteLine("Да");
}
else
{
  Console.WriteLine("Нет");
}
Console.ReadLine();