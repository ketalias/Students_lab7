using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Students_lab7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=UniversityDB;Username=postgres;Password=07042006");

            using (var context = new UniversityContext(optionsBuilder.Options))
            {
                SeedDatabase(context);

                int N = 2;

                List<string> clubNames = new List<string> { "Chess Club", "Music Club" };

                var clubsExcludingBottomN = GetClubsExcludingBottomN(context, N);
                Console.WriteLine("Клуби крім N з найменшою кількістю другокурсників:");
                foreach (var club in clubsExcludingBottomN)
                {
                    Console.WriteLine($"- {club.ClubName}");
                }

                int firstYearITStudentCount = GetFirstYearITStudentsInClubs(context, clubNames);
                Console.WriteLine($"\nСумарна кількість студентів 1-го курсу факультету 'Інформаційні технології' в заданих клубах: {firstYearITStudentCount}");

                var clubsWithMostStudents = GetClubsWithMostStudents(context);
                Console.WriteLine("\nКлуби з найбільшою кількістю студентів:");
                foreach (var clubName in clubsWithMostStudents)
                {
                    Console.WriteLine($"- {clubName}");
                }
            }
        }

        public static void SeedDatabase(UniversityContext context)
        {
            if (!context.ClubRegisters.Any())
            {
                var clubs = new List<ClubRegister>
                {
                    new ClubRegister { ClubName = "Chess Club" },
                    new ClubRegister { ClubName = "Music Club" },
                    new ClubRegister { ClubName = "Art Club" },
                    new ClubRegister { ClubName = "Sports Club" }
                };
                context.ClubRegisters.AddRange(clubs);
                context.SaveChanges(); 
            }

            if (!context.Students.Any())
            {
                var students = new List<Student>
                {
                    new Student { LastName = "Ivanov", Course = 1, Faculty = "Інформаційні технології", ClubId = 1 },
                    new Student { LastName = "Petrov", Course = 2, Faculty = "Економіка", ClubId = 2 },
                    new Student { LastName = "Sidorov", Course = 1, Faculty = "Інформаційні технології", ClubId = 1 },
                    new Student { LastName = "Koval", Course = 2, Faculty = "Фізика", ClubId = 3 },
                    new Student { LastName = "Smirnov", Course = 1, Faculty = "Інформаційні технології", ClubId = 4 }
                };
                context.Students.AddRange(students);
                context.SaveChanges();
            }
        }

        public static List<ClubRegister> GetClubsExcludingBottomN(UniversityContext context, int N)
        {
            return context.ClubRegisters
                .Where(c => c.Students.Any(s => s.Course == 2))
                .OrderBy(c => c.Students.Count(s => s.Course == 2)) 
                .Skip(N) 
                .ToList();
        }

        public static int GetFirstYearITStudentsInClubs(UniversityContext context, List<string> clubNames)
        {
            return context.Students
                .Where(s => s.Course == 1 && s.Faculty == "Інформаційні технології" && clubNames.Contains(s.Club.ClubName))
                .Count();
        }

        public static List<string> GetClubsWithMostStudents(UniversityContext context)
        {
            return context.ClubRegisters
                .OrderByDescending(c => c.Students.Count)
                .Select(c => c.ClubName)
                .ToList();
        }
    }
}



