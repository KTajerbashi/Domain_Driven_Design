namespace BaseSource.Core.Domain.Aggregates.Store.Orders.ValueObjects;

public class Address : BaseValueObject<Address>
{
    // ... existing properties and constructor ...

    // The delimiter must be a character that will never appear in the data.
    // Using '|' is common, but you must be certain it's escaped or forbidden in validation.
    private const char Delimiter = '|';
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    // EF Core REQUIRES this - make it private
    private Address()
    {
        // Required for EF Core only
        // Properties will be set by EF Core via reflection
    }
    // Public domain constructor for application code
    public Address(string street, string city, string state, string country, string zipCode)
    {
        Validate(street, city, state, country, zipCode);
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }
    // Copy constructor (optional)
    public Address(Address original)
    {
        Street = original.Street;
        City = original.City;
        State = original.State;
        Country = original.Country;
        ZipCode = original.ZipCode;
    }

    private void Validate(string street, string city, string state, string country, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required");
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required");
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required");
        // Add other validation as needed
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }

    public override string ToString()
    {
        // More efficient than using StringBuilder for this fixed number of components
        return $"{Street}{Delimiter}{City}{Delimiter}{State}{Delimiter}{ZipCode}{Delimiter}{Country}";
    }

    public static Address FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // Handle null/empty input. Could return null, throw exception, or return an empty Address.
            // Throwing an exception is often the safest choice.
            throw new ArgumentException("Input string cannot be null or empty.", nameof(value));
        }

        // Split the string by the delimiter
        var components = value.Split(Delimiter);

        // Ensure the split operation resulted in the expected number of parts
        if (components.Length != 4)
        {
            throw new ArgumentException($"Input string '{value}' is not in the correct format. Expected 4 parts separated by '{Delimiter}'.", nameof(value));
        }

        var street = components[0];
        var city = components[1];
        var state = components[2];
        var zipCode = components[3];
        var country = components[4];

        // Use the main constructor to create a new Address.
        // This ensures all validation rules are applied (e.g., null checks, length checks).
        return new Address(street, city, state, zipCode, country);
    }
}
