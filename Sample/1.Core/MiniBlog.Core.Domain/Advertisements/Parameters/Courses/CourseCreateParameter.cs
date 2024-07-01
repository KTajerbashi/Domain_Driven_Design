using MiniBlog.Core.Domain.Advertisements.Parameters.Admins;

namespace MiniBlog.Core.Domain.Advertisements.Parameters.Courses;

public record CourseCreateParameter(
    string Name,
    int Length,
    DateTime From,
    DateTime To,
    long AdvertisementId,
    List<AdminCreateParameter> Admins);

