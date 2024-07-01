using DDD.Core.Domain.Library.Entities;
using MiniBlog.Core.Domain.Advertisements.Parameters.Admins;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlog.Core.Domain.Advertisements.Entities;

[Table("Admins", Schema = "Blog")]
public class Admin : Entity
{
    public string Title { get; private set; }
    public int RoleId { get; private set; }

    [ForeignKey(nameof(Course))]
    public long CourseId { get; private set; }
    public Course Course { get; private set; }

    private Admin()
    {
        
    }
    public Admin(AdminCreateParameter parameter)
    {
        Title = parameter.Title;
        RoleId = parameter.RoleId;
        CourseId = parameter.CourseId;
    }

    public void Update(AdminUpdateParameter parameter)
    {
        Title = parameter.Title;
        RoleId = parameter.RoleId;
        CourseId = parameter.CourseId;
    }
}
