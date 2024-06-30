using DDD.Core.Domain.Library.Entities;
using MiniBlog.Core.Domain.Advertisements.DomainEvents;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlog.Core.Domain.Advertisements.Entities;

[Table("Courses", Schema ="Blog")]
public class Course : Entity
{
    public string Name { get; private set; }
    public int Lenght { get; private set; }
    public DateTime From { get; private set; }
    public DateTime To { get; private set; }
}
[Table("Advertisements", Schema ="Blog")]
public class Advertisement : AggregateRoot
{
    #region Properties
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int CityId { get; private set; }
    public int Salary { get; private set; }
    public bool IsRemote { get; private set; }
    public DateTime? PublishDate { get; private set; }

    private List<Course> _courses;
    public IReadOnlyList<Course> Courses => _courses;



    #endregion


    #region Constructors
    private Advertisement()
    {
    }
    public Advertisement(AdvertisementCreateParameter parameter)
    {
        Title = parameter.Title;
        Description = parameter.Description;
        CityId = parameter.CityId;
        Salary = parameter.Salary;
        IsRemote = parameter.IsRemote;
        AddEvent(new AdvertisementCreated(BusinessId.Value,Title,Description,Salary,CityId,IsRemote));
    }
    #endregion


    #region Commands
    #endregion
}
