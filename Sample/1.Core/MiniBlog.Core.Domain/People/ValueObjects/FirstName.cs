using DDD.Core.Domain.Library.Exceptions;
using DDD.Core.Domain.Library.ValueObjects;
using MiniBlog.Core.Domain.Resources;

namespace MiniBlog.Core.Domain.People.ValueObjects
{
    public class FirstName : BaseValueObject<FirstName>
    {
        public static FirstName FromString(string value) => new(value);
        //public static FirstName FromGuid(Guid value) => new() { Value = value };

        public string Value { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public FirstName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidValueObjectStateException(MessagePatterns.EmptyStringValidationMessage, nameof(FirstName));
            }
            if (value.Length < 2 || value.Length > 50)
            {
                throw new InvalidValueObjectStateException(MessagePatterns.StringLengthValidationMessage, nameof(FirstName), "2", "50");
            }
            Value = value;
        }
        public static explicit operator string(FirstName firstName) => firstName.Value.ToString();
        public static implicit operator FirstName(string value) => new(value);
    }
}
