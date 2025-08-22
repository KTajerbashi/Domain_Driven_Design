namespace BaseSource.Core.Domain.Aggregates.Store.Customers.Entities;

public class Customer : AggregateRoot
{
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }

    private readonly List<CustomerAddress> _customerAddresses = new();
    public IReadOnlyCollection<CustomerAddress> CustomerAddresses => _customerAddresses.AsReadOnly();

    private Customer()
    {
    }
    public Customer(string name, string family, string email, string phone, string username, string password)
    {

    }
}
