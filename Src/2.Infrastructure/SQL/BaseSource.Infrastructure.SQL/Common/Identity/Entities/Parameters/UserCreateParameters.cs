namespace BaseSource.Infrastructure.SQL.Common.Identity.Entities.Parameters;

public record UserCreateParameters(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string NationalCode
    );