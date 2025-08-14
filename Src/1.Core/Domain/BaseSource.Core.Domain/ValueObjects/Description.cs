﻿namespace BaseSource.Core.Domain.ValueObjects;

public class Description : BaseValueObject<Description>
{
    #region Properties
    public string Value { get; private set; }
    #endregion

    #region Constructors and Factories
    public static Description FromString(string value) => new(value);
    private Description(string value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > 500)
        {
            throw new DomainException("ValidationErrorIsRequire", nameof(Description), "0", "500");
        }

        Value = value;
    }
    private Description()
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
    public static explicit operator string(Description description) => description.Value;

    public static implicit operator Description(string value) => new(value);
    #endregion
}

