﻿using AutoMapper;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.Domain.Advertisements.Parameters.Courses;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Update;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.Update;

public class UpdateAdvertisementProfile : Profile
{
    public UpdateAdvertisementProfile()
    {
        CreateMap<AdvertisementUpdateCommand, AdvertisementUpdateParameter>().ReverseMap();
        CreateMap<CoursesUpdateCommand, CourseUpdateParameter>().ReverseMap();
        //CreateMap<List<AdvertisementCoursesUpdateCommand>, List<AdvertisementCourseUpdateParameter>>().ReverseMap();
    }
}
