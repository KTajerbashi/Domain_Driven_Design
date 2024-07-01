using MiniBlog.Core.Domain.Advertisements.Parameters.Admins;

namespace MiniBlog.Core.Domain.Advertisements.Parameters.Courses;

public record CourseUpdateParameter(
    string Name,
    int Length,
    DateTime From,
    DateTime To,
    long AdvertisementId,
    List<AdminUpdateParameter> Admins);
