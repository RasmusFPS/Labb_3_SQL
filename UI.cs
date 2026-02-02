using System;

namespace Labb_3
{
    public static class UI
    {
        private static SQLmanager db = new SQLmanager();

        public static void Run()
        {
            bool run = true;

            while (run)
            {
                Console.WriteLine("Database Console");
                Console.WriteLine("1.Get all Students");
                Console.WriteLine("2.Get Students in Class");
                Console.WriteLine("3.Add Personal");
                Console.WriteLine("4.Exit");
                Console.Write("Choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        GetStudents();
                        break;
                    case "2":
                        GetStudentsInClass();
                        break;
                    case "3":
                        Addstaff();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("INVALID");
                        break;
                }
            }
        }

        static void GetStudents()
        {
            Console.Clear();

            Console.WriteLine("Do you wish to sort by firstname(1) or lastname(2) ");
            string sortName = Console.ReadLine();

            Console.WriteLine("Do you wish to sort 1:(A-Ö) or 2:(Ö-A)");
            string sortOrder = Console.ReadLine();

            var students = db.GetSortedStudents(sortName, sortOrder, out bool invalid);

            if (invalid)
            {
                Console.WriteLine("INVALID CHOICE SORTS LASTNAME (A-Ö)");
            }

            foreach (var S in students)
            {
                Console.WriteLine($"Student Id:{S.StudentId} {S.FirstName} {S.LastName}");
            }

            Console.ReadLine();
            Console.Clear();
        }

        static void GetStudentsInClass()
        {
            Console.Clear();

            var classes = db.GetAllClasses();
            foreach (var c in classes)
            {
                Console.WriteLine($"ClassID {c.ClassId} {c.ClassName}");
            }

            Console.WriteLine();
            Console.Write("Choose ClassID:");
            if (int.TryParse(Console.ReadLine(), out int ClassId))
            {
                var students = db.GetStudentsByClass(ClassId);

                if (students.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Students in Class {ClassId}");

                    foreach (var s in students)
                    {
                        Console.WriteLine($"{s.FirstName} {s.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("No student found or wrong ID");
                }
            }
            else
            {
                Console.WriteLine("Incorrect ID");
                Console.Clear();
            }

            Console.ReadKey();
            Console.Clear();
        }

        static void Addstaff()
        {
            Console.Clear();
            Console.WriteLine("Add personal");

            Console.Write("FirstName:");
            string Fname = Console.ReadLine();

            Console.Write("Lastname:");
            string Lname = Console.ReadLine();

            Console.Write("Role:");
            string role = Console.ReadLine();

            db.AddStaffToDb(Fname, Lname, role);

            Console.WriteLine("Staff Added to the DB");
            Console.Clear();
        }
    }
}