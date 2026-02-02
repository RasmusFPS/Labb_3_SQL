using Labb_3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Labb_3
{
    internal class SQLmanager
    {
        internal List<Student> GetSortedStudents(string sortName, string sortOrder, out bool invalid)
        {
            using (var context = new Labb2Context())
            {
                var query = context.Students.ToList();
                invalid = false;

                if (sortName == "1")
                {
                    if (sortOrder == "2")
                        query = query.OrderByDescending(s => s.FirstName).ToList();
                    else
                        query = query.OrderBy(s => s.FirstName).ToList();
                }
                else if (sortName == "2")
                {
                    if (sortOrder == "2")
                        query = query.OrderByDescending(s => s.LastName).ToList();
                    else
                        query = query.OrderBy(s => s.LastName).ToList();
                }
                else
                {
                    invalid = true;
                    // Default sort as per original logic
                    query = query.OrderBy(s => s.LastName).ToList();
                }
                return query;
            }
        }

        internal List<Class> GetAllClasses()
        {
            using (var context = new Labb2Context())
            {
                return context.Classes.ToList();
            }
        }

        internal List<Student> GetStudentsByClass(int classId)
        {
            using (var context = new Labb2Context())
            {
                return context.Students
                    .Where(s => s.ClassId == classId)
                    .Include(s => s.Class)
                    .ToList();
            }
        }

        internal void AddStaffToDb(string fName, string lName, string role)
        {
            using (var context = new Labb2Context())
            {
                var staff = new Staff
                {
                    FirstName = fName,
                    LastName = lName,
                    Role = role
                };
                context.Staff.Add(staff);
                context.SaveChanges();
            }
        }
    }
}