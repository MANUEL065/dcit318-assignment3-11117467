using System;
using System.Collections.Generic;
using System.IO;

namespace SchoolGradingSystem
{
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();

            using (var reader = new StreamReader(inputFilePath))
            {
                string? line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    var parts = line.Split(',');

                    if (parts.Length != 3)
                        throw new MissingFieldException($"Line {lineNumber}: Missing data → \"{line}\"");

                    if (!int.TryParse(parts[0].Trim(), out int id))
                        throw new FormatException($"Line {lineNumber}: Invalid ID format → \"{line}\"");

                    string fullName = parts[1].Trim();

                    if (!int.TryParse(parts[2].Trim(), out int score))
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format → \"{line}\"");

                    students.Add(new Student(id, fullName, score));
                }
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
                }
            }
        }

        public void Run()
        {
            try
            {
                string inputPath = "students.txt";
                string outputPath = "report.txt";

                if (!File.Exists(inputPath))
                {
                    Console.WriteLine($"Error: The file '{inputPath}' was not found.");
                    Console.WriteLine("Creating a sample students.txt file...");

                    File.WriteAllLines(inputPath, new[]
                    {
                        "101, Alice Smith, 84",
                        "102, Bob Johnson, 72",
                        "103, Charlie Brown, 67",
                        "104, Diana Prince, 54",
                        "105, Evan Davis, 43"
                    });

                    Console.WriteLine($"Sample '{inputPath}' created. Please run the program again.");
                    return;
                }

                var students = ReadStudentsFromFile(inputPath);

                Console.WriteLine("\n=== Student Results ===");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
                }

                WriteReportToFile(students, outputPath);

                Console.WriteLine($"\nReport generated successfully and saved to '{outputPath}'.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: File not found - {ex.Message}");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
