using MiniBlog.Core.Domain.Advertisements.Parameters.Courses;

namespace MiniBlog.Core.Domain.Advertisements.Parameters;

public record AdvertisementCreateParameter(
    string Title, 
    string Description, 
    int Salary, 
    int CityId,
    bool IsRemote, 
    List<CourseCreateParameter> Courses);

