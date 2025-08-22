namespace BaseSource.Core.Domain.Aggregates.Store.Customers.Entities;

public class CustomerAddress : Entity
{
    public string Name { get; private set; }

    private CustomerAddress()
    {
    }
    public CustomerAddress(string name)
    {

    }
}
