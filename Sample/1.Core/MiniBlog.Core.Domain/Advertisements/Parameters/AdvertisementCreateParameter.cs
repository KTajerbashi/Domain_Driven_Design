namespace MiniBlog.Core.Domain.Advertisements.Parameters;

public record AdvertisementCreateParameter(string Title,string Description,int Salary,int CityId,bool IsRemote);

