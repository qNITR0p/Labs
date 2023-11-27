using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines(@"Лабораторная работа 5 - testData.xml");

        string pattern1 = @"^\s*\d+\.\s*";
        string pattern2 = @"^\s*<";
        string pattern3 = @"</[^>]+>";
        string pattern4 = @"<client_address>(.*?)</client_address>|ул\. (.*?) д\. (\d+/\d+)$";

        for (int i = 0; i < input.Length; ++i)
        {
            if (Regex.IsMatch(input[i], pattern1))
            {
                input[i] = Regex.Replace(input[i], pattern1, "");
            }
            else if (Regex.IsMatch(input[i], pattern2))
            {
                input[i] = " " + input[i];
            }
            else if (Regex.IsMatch(input[i], pattern3))
            {
                Match match = Regex.Match(input[i], pattern3);
                string tag = match.Value.Substring(2, match.Value.Length - 3);
                input[i] = input[i].Replace(match.Value, $"</{tag}>");
            }
            else if (Regex.IsMatch(input[i], pattern4))
            {
                Match match = Regex.Match(input[i], pattern4);
                if (match.Groups[1].Value != "")
                {
                    input[i] = match.Groups[1].Value;
                }
                else
                {
                    input[i] = $"Улица: {match.Groups[2].Value}\nНомер дома: {match.Groups[3].Value}";
                }
            }
            else
            {
                input[i] = "Не найдено";
            }
        }

        File.WriteAllLines(@"Lab5Text.xml", input);
    }
}
