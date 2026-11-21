using System.Collections.Generic;
using System.IO;
using System.Text;

//8. Основная часть — «Файловый журнал результатов»
string dataDirectory = "data";
string inputPath = Path.Combine(dataDirectory, "scores.txt");
string reportPath = Path.Combine(dataDirectory, "report.txt");
string historyPath = Path.Combine(dataDirectory, "history.txt");
string validPath = Path.Combine(dataDirectory, "valid-scores.txt");
try
{
    Directory.CreateDirectory(dataDirectory);
    Console.WriteLine($"Входной файл: {Path.GetFullPath(inputPath)}");

    if (!File.Exists(inputPath))
    {
        Console.WriteLine("Входной файл не найден или недоступен");
        return;
    }

    string[] lines = File.ReadAllLines(inputPath, Encoding.UTF8);
    List<int> scores = ParseScores(lines);
    int skipped = lines.Length - scores.Count;

    List<string> report = BuildReport(scores, skipped);
    File.WriteAllLines(reportPath, report, Encoding.UTF8);

    SaveValidScores(validPath, scores);

    string historyEntry = $"Сохранено: {scores.Count}; пропущено: {skipped}; сумма: {SumScores(scores)}";
    File.AppendAllText(historyPath, historyEntry + Environment.NewLine, Encoding.UTF8);

    Console.WriteLine(">>> Отчёт по файлу <<");
    foreach (string line in report)
    {
        Console.WriteLine(line);
    }
    Console.WriteLine($"Отчёт сохранён: {Path.GetFullPath(reportPath)}");
    Console.WriteLine($"История дополнена: {Path.GetFullPath(historyPath)}");
    Console.WriteLine($"Корректные результаты: {Path.GetFullPath(validPath)}");
}
catch (FileNotFoundException)
{
    Console.WriteLine("Файл для чтения не найден");
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("Нет доступа к файлу или папке");
}
catch (IOException)
{
    Console.WriteLine("Ошибка чтения или записи файла");
}


//Преобразование строк в числа
List<int> ParseScores(string[] lines)
{
    List<int> scores = new List<int>();

    foreach (string line in lines)
    {
        if (int.TryParse(line, out int score) && score >= 0 && score <= 100)
        {
            scores.Add(score);
        }
    }
    return scores;
}

int SumScores(List<int> scores)
{
    int sum = 0;
    foreach (int score in scores)
    {
        sum += score;
    }
    return sum;
}

List<string> BuildReport(List<int> scores, int skipped)
{
    List<string> report = new List<string>();
    report.Add($"Сохранено результатов: {scores.Count}");
    report.Add($"Пропущено строк: {skipped}");
    report.Add($"Сумма: {SumScores(scores)}");

    if (scores.Count == 0)
    {
        report.Add("Среднее не вычисляется: список пуст");
    }
    else
    {
        double average = (double)SumScores(scores) / scores.Count;
        report.Add($"Среднее: {average}");
    }
    report.Add("Результаты:");
    if (scores.Count == 0)
    {
        report.Add("Список пуст");
    }
    else
    {
        foreach (int score in scores)
        {
            report.Add(score.ToString());
        }
    }

    return report;
} 

//13. Самостоятельное задание
//Вариант 1. Только корректные результаты
void SaveValidScores(string path, List<int> scores)
{
    List<string> lines = new List<string>();
    foreach (int score in scores)
    {
        lines.Add(Convert.ToString(score));
    }
    File.WriteAllLines(path, lines, Encoding.UTF8);
}
