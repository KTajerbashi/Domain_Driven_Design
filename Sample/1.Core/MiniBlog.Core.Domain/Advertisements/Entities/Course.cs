using DDD.Core.Domain.Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlog.Core.Domain.Advertisements.Entities;

[Table("Courses", Schema ="Blog")]
public class Course : Entity
{
    public string Name { get; private set; }
    public int Length { get; private set; }
    public DateTime From { get; private set; }
    public DateTime To { get; private set; }

    public Course(string name,int length, DateTime from,DateTime to)
    {
        Name = name;
        Length = length;
        From = from;
        To = to;
    }

}
