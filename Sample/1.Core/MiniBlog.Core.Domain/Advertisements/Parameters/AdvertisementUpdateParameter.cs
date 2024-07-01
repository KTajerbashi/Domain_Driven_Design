using MiniBlog.Core.Domain.Advertisements.Parameters.Admins;
using MiniBlog.Core.Domain.Advertisements.Parameters.Courses;

namespace MiniBlog.Core.Domain.Advertisements.Parameters;

public sealed record class AdvertisementUpdateParameter(
    string Title,
    string Description,
    int Salary,
    int CityId, 
    bool IsRemote,
    List<CourseUpdateParameter> Courses);
