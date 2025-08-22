using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;
using System.Text;

namespace BaseSource.Core.Domain.ValueObjects.Common;
public class Title : BaseValueObject<Title>
{
    #region Properties
    public string Value { get; private set; }
    #endregion

    #region Constructors and Factories
    public static Title FromString(string value) => new Title(value);
    private Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ValidationErrorIsRequire {0}", nameof(Title));
        }
        if (value.Length < 2 || value.Length > 250)
        {
            throw new DomainException("ValidationErrorStringLength {0} {1} {2}", nameof(Title), "2", "250");
        }
        Value = value;
    }
    private Title()
    {

    }
    #endregion


    #region Equality Check
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion

    #region Operator Overloading
    /// <summary>
    /// This line tells the C# compiler how to automatically convert a string into a Title object without requiring an explicit cast.
    /// </summary>
    /// <param name="title"></param>
    public static explicit operator string(Title title) => title.Value;

    /// <summary>
    /// This line tells the C# compiler how to convert a Title object back into a string, but it requires an explicit cast.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator Title(string value) => new(value);
    #endregion

    #region Methods
    public override string ToString() => Value;

    #endregion
}
