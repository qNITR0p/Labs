using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines(@"Lab5Text.txt");

        string pattern1 = "^(a|aaaaaa|a aa a)$";
        string pattern2 = "^[a-zA-Z0-9]{5,}$";
        string pattern3 = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        string pattern4 = @"^ул\. (.*?) д\. (\d+/\d+)$";


        for (int i = 0; i < input.Length; ++i)
        {
            Console.WriteLine($"Проверяю строку: {input[i]}");

            if (Regex.IsMatch(input[i], pattern1))
            {
                Console.WriteLine("Строка соответствует шаблону 1");
            }
            else if (Regex.IsMatch(input[i], pattern2))
            {
                Console.WriteLine("Строка соответствует шаблону 2");
            }
            else if (Regex.IsMatch(input[i], pattern3))
            {
                Console.WriteLine("Строка соответствует шаблону 3");
            }
            else if (Regex.IsMatch(input[i], pattern4))
            {
                Match match = Regex.Match(input[i], pattern4);
                Console.WriteLine($"Улица: {match.Groups[1].Value}");
                Console.WriteLine($"Номер дома: {match.Groups[2].Value}");
            }
            else
            {
                Console.WriteLine("Строка не соответствует ни одному из шаблонов");
            }
        }
    }
}
