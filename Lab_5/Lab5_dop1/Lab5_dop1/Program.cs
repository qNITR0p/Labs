using System;
using System.Globalization;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string text = "Добро пожаловать в наш магазин, вот наши цены: 1 кг. яблоки - 90 руб., 2 кг. апельсины - 130 руб. Также в ассортименте орехи в следующей фасовке: 0.5 кг. миндаль - 500 руб.";

        // Паттерн для поиска выражений с ценой за кг.
        string pattern = @"(\d+(\.\d+)?)\s*кг\.\s*([\w\s]+)\s*-\s*(\d+)\s*руб\.";

        Regex regex = new Regex(pattern);

        MatchCollection matches = regex.Matches(text);

        foreach (Match match in matches)
        {
            if (float.TryParse(match.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float weight) &&
                int.TryParse(match.Groups[4].Value, out int price))
            {
                string product = match.Groups[3].Value;
                float pricePerKg = price / weight;

                Console.WriteLine($"{product} - {pricePerKg:0} руб/кг");
            }
            else
            {
                Console.WriteLine($"Ошибка формата данных для строки: {match.Value}");
            }
        }
    }
}
