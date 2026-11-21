using System.Collections.Generic;
using System.IO;
using System.Text;

//2. Пути и рабочая папка
string dataDirectory = "data";
string inputPath = Path.Combine(dataDirectory, "scores.txt");
Console.WriteLine($"Рабочая папка: {Directory.GetCurrentDirectory()}");
Console.WriteLine($"Полный путь к файлу: {Path.GetFullPath(inputPath)}");

Directory.CreateDirectory(dataDirectory);
Console.WriteLine(File.Exists(inputPath));

//3. Запись и чтение всего текста
string notePath = Path.Combine("data", "note.txt");
Directory.CreateDirectory("data");

File.WriteAllText(notePath, "Привет, файл!", Encoding.UTF8);

string text = File.ReadAllText(notePath, Encoding.UTF8);
Console.WriteLine(text);

//4. Добавление текста в конец файла
// string journalPath = Path.Combine("data", "journal.txt");

// File.WriteAllText(journalPath, "Первый запуск" + Environment.NewLine, Encoding.UTF8);
// File.AppendAllText(journalPath, "Второй запуск" + Environment.NewLine, Encoding.UTF8);

// Console.Write(File.ReadAllText(journalPath, Encoding.UTF8));

//Найдите ошибку: история потерялась
// string historyExamplePath = Path.Combine("data", "history-example.txt");
// File.WriteAllText(historyExamplePath, "Запись 1" + Environment.NewLine, Encoding.UTF8);
// File.AppendAllText(historyExamplePath, "Запись 2" + Environment.NewLine, Encoding.UTF8);
// Console.Write(File.ReadAllText(historyExamplePath, Encoding.UTF8));

//5. Файл как массив строк
string linesPath = Path.Combine("data", "lines-example.txt");
string[] sourceLines = { "80", "90", "70" };

File.WriteAllLines(linesPath, sourceLines, Encoding.UTF8);
string[] loadedLines = File.ReadAllLines(linesPath, Encoding.UTF8);

Console.WriteLine($"Строк: {loadedLines.Length}");

foreach (string line in loadedLines)
{
    Console.WriteLine(line);
}

//Преобразование строк в числа
// List<int> ParseScores(string[] lines)
// {
//     List<int> scores = new List<int>();

//     foreach (string line in lines)
//     {
//         if (int.TryParse(line, out int score) && score >= 0 && score <= 100)
//         {
//             scores.Add(score);
//         }
//     }
//     return scores;
// }

string[] testLines = { "80", "abc", "101", "90", "", "70", "2147483648" };
List<int> parsedScores = ParseScores(testLines);
Console.WriteLine($"Принято: {parsedScores.Count}");
Console.WriteLine($"Пропущено: {testLines.Length - parsedScores.Count}");

//Найдите ошибку: одна плохая строка остановила обработку
List<int> ParseScores(string[] lines)
{
    List<int> scores = new List<int>();
    foreach (string line in lines)
    { if (int.TryParse(line, out int score) && score >= 0 && score <= 100)
        {
            scores.Add(score);
        }
    }
    return scores;
}
foreach (int a in parsedScores)
{
    Console.WriteLine(a);
} 

//7. Ошибки файловых операций
string missingPath = Path.Combine("data", "missing-example.txt");
try
{
 string missingText = File.ReadAllText(missingPath, Encoding.UTF8);
 Console.WriteLine(missingText);
}
catch (FileNotFoundException)
{
 Console.WriteLine("Файл не найден");
}
catch (UnauthorizedAccessException)
{
 Console.WriteLine("Нет доступа к файлу или папке");
}
catch (IOException)
{
 Console.WriteLine("Ошибка чтения или записи файла");
}