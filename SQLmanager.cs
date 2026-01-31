using Labb_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Labb_3
{
    internal class SQLmanager
    {
        internal void run()
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


                string? choice = Console.ReadLine();
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
            using (var context = new Labb2Context())
            {
                Console.Clear();

                Console.WriteLine("Do you wish to sort by firstname(1) or lastname(2) ");
                string sortName = Console.ReadLine();

                Console.WriteLine("Do you wish to sort 1:(A-Ö) or 2:(Ö-A)");
                string sortOrder = Console.ReadLine();

                var query = context.Students.AsQueryable();

                if (sortName == "1")
                {
                    if (sortOrder == "2")
                        query = query.OrderByDescending(s => s.FirstName);
                    else
                        query = query.OrderBy(s => s.FirstName);
                }
                else if (sortName == "2")
                {
                    if (sortOrder == "2")
                        query = query.OrderByDescending(s => s.LastName);
                    else
                        query = query.OrderBy(s => s.LastName);
                }
                else
                {
                    Console.WriteLine("INVALID CHOICE SORTS LASTNAME (A-Ö)");
                }

                var student = query.ToList();

                foreach (var S in student)
                {
                    Console.WriteLine($"Student Id:{S.StudentId} {S.FirstName} {S.LastName}");
                }

                Console.ReadLine();
                Console.Clear();

            }
        }

        static void GetStudentsInClass()
        {
            using (var context = new Labb2Context())
            {
                Console.Clear();

                var classes = context.Classes.ToList();
                foreach (var c in classes)
                {
                    Console.WriteLine($"ClassID {c.ClassId} {c.ClassName}");
                }

                Console.WriteLine();
                Console.Write("Choose ClassID:");
                if (int.TryParse(Console.ReadLine(), out int Class))
                {
                    var student = context.Students
                                        .Where(s => s.ClassId == Class)
                                        .Include(s => s.Class)
                                        .ToList();
                    if (student.Any())
                    {
                        Console.Clear();
                        Console.WriteLine($"Students in Class {Class}");

                        foreach (var s in student)
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

        }

        static void Addstaff()
        {
            using (var context = new Labb2Context())
            {
                Console.Clear();
                Console.WriteLine("Add personal");

                Console.Write("FirstName:");
                string Fname = Console.ReadLine();

                Console.Write("Lastname:");
                string Lname = Console.ReadLine();

                Console.Write("Role:");
                string role = Console.ReadLine();

                var staff = new Staff
                {
                    FirstName = Fname,
                    LastName = Lname,
                    Role = role
                };

                context.Staff.Add(staff);
                context.SaveChanges();

                Console.WriteLine("Staff Added to the DB");
            }
        }
    }
}
