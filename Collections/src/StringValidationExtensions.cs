using System.Text.RegularExpressions;

namespace CollectionsLab;

public static class StringValidationExtensions
{
    // 010 / 011 / 012 / 015 + 8 digits (11 digits), or the same number written with +20 instead of the leading 0.
    //   \A ... \z  : whole string must match (unlike $, \z does not allow a trailing "\n")
    //   [0-9]      : ASCII digits only (\d would also accept other Unicode digits in .NET)
    private static readonly Regex EgyptianPhone =
        new(@"\A(?:\+20|0)1[0125][0-9]{8}\z", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    // Exactly 14 digits, the first one is 2 or 3.
    private static readonly Regex EgyptianNationalId =
        new(@"\A[23][0-9]{13}\z", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static bool IsValidEgyptianPhone(this string? value) =>
        !string.IsNullOrWhiteSpace(value) && EgyptianPhone.IsMatch(value);

    public static bool IsValidEgyptianNationalId(this string? value) =>
        !string.IsNullOrWhiteSpace(value) && EgyptianNationalId.IsMatch(value);
}
