using DDD.Core.Domain.Library.Entities;
using MiniBlog.Core.Domain.Advertisements.Parameters.Admins;
using MiniBlog.Core.Domain.Advertisements.Parameters.Courses;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlog.Core.Domain.Advertisements.Entities;

[Table("Courses", Schema = "Blog")]
public class Course : Entity
{
    public string Name { get; private set; }
    public int Length { get; private set; }

    [ForeignKey(nameof(Advertisement))]
    public long AdvertisementId { get; private set; }
    public Advertisement Advertisement { get; private set; }
    
    public DateTime From { get; private set; }
    public DateTime To { get; private set; }
    private List<Admin> _admins;
    public IReadOnlyList<Admin> Admins => _admins;
    private Course()
    {
        
    }
    public Course(
       CourseCreateParameter parameter)
    {
        Name = parameter.Name;
        Length = parameter.Length;
        From = parameter.From;
        To = parameter.To;
        AdvertisementId = parameter.AdvertisementId;
        if (parameter.Admins.Count > 0)
        {
            _admins = new List<Admin>();
            foreach (var item in parameter.Admins)
            {
                _admins.Add(new Admin(item));
            }
        }
    }
    public void Update(
        CourseUpdateParameter parameter
        )
    {
        Name = parameter.Name;
        Length = parameter.Length;
        From = parameter.From;
        To = parameter.To;
        AdvertisementId = parameter.AdvertisementId;
    }


}
