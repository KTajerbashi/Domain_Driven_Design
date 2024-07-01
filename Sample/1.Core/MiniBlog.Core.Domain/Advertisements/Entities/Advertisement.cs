using DDD.Core.Domain.Library.Entities;
using DDD.Core.Domain.Library.Exceptions;
using MiniBlog.Core.Domain.Advertisements.DomainEvents;
using MiniBlog.Core.Domain.Advertisements.DomainEvents.Courses;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.Domain.Resources;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlog.Core.Domain.Advertisements.Entities;
[Table("Advertisements", Schema = "Blog")]
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
        AddEvent(new AdvertisementCreated(BusinessId.Value, Title, Description, Salary, CityId, IsRemote));
        if (parameter.Courses.Count > 0)
        {
            _courses = new List<Course>();
            foreach (var course in parameter.Courses)
            {
                _courses.Add(new Course(course));
            }
        }
    }
    #endregion


    #region Commands
    public void Update(AdvertisementUpdateParameter parameter)
    {
        Title = parameter.Title;
        Description = parameter.Description;
        CityId = parameter.CityId;
        Salary = parameter.Salary;
        IsRemote = parameter.IsRemote;
        AddEvent(new AdvertisementUpdated(Id, BusinessId.Value, Title, Description, Salary, CityId, IsRemote));
    }
    public void Delete()
    {
        AddEvent(new AdvertisementDeleted(BusinessId.Value, Id));
    }

    public void Publish()
    {
        if (PublishDate.HasValue)
        {
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_VALID, nameof(PublishDate));
        }
        PublishDate = DateTime.Now;
        AddEvent(new AdvertisementPublished(BusinessId.Value, Id, PublishDate.Value));
    }
    public void UnPublish()
    {
        if (!PublishDate.HasValue)
        {
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_VALID, nameof(PublishDate));
        }
        PublishDate = null;
        AddEvent(new AdvertisementUnPublished(BusinessId.Value, Id));
    }

    public void UpdateCourse()
    {

    }
    #endregion
}
