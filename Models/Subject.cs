using System;
using System.Collections.Generic;

namespace Labb_3.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? Subject1 { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
