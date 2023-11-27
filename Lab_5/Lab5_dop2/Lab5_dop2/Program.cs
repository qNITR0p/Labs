using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        var regex = new Regex(@"((http|https|ftp)?://)?(www\.)?([a-z0-9]+(-[a-z0-9]+)*\.){1,5}[a-z]{2,6}");
        var content = File.ReadAllText("input.txt");

        var lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            for (int i = 0; i < lines.Length; i++)
            {
                var matches = regex.Matches(lines[i]);
                foreach (Match match in matches)
                {
                    writer.WriteLine($"URL: {match.Value}, Line: {i + 1}");
                }
            }
        }
    }
}
