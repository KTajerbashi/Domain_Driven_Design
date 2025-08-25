using System.ComponentModel;

namespace BaseSource.Kernel.Utilities.Extensions;
public static class EnumExtensions
{
    /// <summary>
    /// برای دریافت توضیحات یک ویژگی از enum اگر [Description] داشته باشد از این متد استفاده می‌شود.
    /// </summary>
    /// <param name="enumValue">مقداری که قرار است توضحیات آن دریافت شود</param>
    /// <returns>متن داخل [Description] در صورتی که وجود داشته باشد و در غیراین صورت عنوان enums ارسال شده</returns>
    public static string GetEnumDescription(this Enum enumValue)
    {
        if (enumValue is null)
            return "";
        var memberInfo = enumValue.GetType().GetField(enumValue.ToString());
        var attributes = memberInfo!.GetCustomAttributes(typeof(DescriptionAttribute), false);
        var description = attributes != null ? ((DescriptionAttribute)attributes.FirstOrDefault()!).Description : enumValue.ToString();
        return description;
    }


    /// <summary>
    /// تبدیل byte به Enum
    /// </summary>
    public static TEnum To<TEnum>(this byte value)
        where TEnum : Enum
    {
        if (Enum.IsDefined(typeof(TEnum), value))
        {
            return (TEnum)(object)value;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(value),
                $"The value {value} is not defined in enum {typeof(TEnum).Name}");
        }
    }
}
