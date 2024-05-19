using DDD.Core.Domain.Library.Exceptions;
using DDD.Core.Domain.Library.ValueObjects;
using MiniBlog.Core.Domain.Resources;

namespace MiniBlog.Core.Domain.People.ValueObjects
{
    public class LastName : BaseValueObject<LastName>
    {
        public static LastName FromString(string value) => new(value);
        //public static FirstName FromGuid(Guid value) => new() { Value = value };

        public string Value { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public LastName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidValueObjectStateException(MessagePatterns.EmptyStringValidationMessage, nameof(LastName));
            }
            if (value.Length < 2 || value.Length > 50)
            {
                throw new InvalidValueObjectStateException(MessagePatterns.StringLengthValidationMessage, nameof(LastName), "2", "50");
            }
            Value = value;
        }
        public static explicit operator string(LastName lastName) => lastName.Value.ToString();
        public static implicit operator LastName(string value) => new(value);
    }
}
