using CollectionsLab;

// (input, which method, expected result)
var cases = new (string? Input, string Method, bool Expected)[]
{
    // The table from the assignment
    ("01012345678",    "phone", true),    // valid
    ("+201512345678",  "phone", true),    // +20 form
    ("01312345678",    "phone", false),   // 013 is not a valid prefix
    ("0101234567",     "phone", false),   // only 10 digits
    ("0101234567a",    "phone", false),   // contains a letter
    ("29901011234567", "id",    true),
    ("19901011234567", "id",    false),   // must start with 2 or 3
    ("2990101123456",  "id",    false),   // only 13 digits

    // Extra edge cases: null / empty / spaces must return false and never throw
    (null,             "phone", false),
    ("",               "phone", false),
    ("   ",            "phone", false),
    (null,             "id",    false),
    ("",               "id",    false),
    ("   ",            "id",    false),

    // A few more valid / invalid forms
    ("01112345678",    "phone", true),
    ("01212345678",    "phone", true),
    ("+201012345678",  "phone", true),
    ("+2001012345678", "phone", false),   // 0 must be replaced by +20, not added to it
    ("30001011234567", "id",    true),
};

var failures = 0;
foreach (var (input, method, expected) in cases)
{
    var actual = method == "phone"
        ? input.IsValidEgyptianPhone()
        : input.IsValidEgyptianNationalId();

    var ok = actual == expected;
    if (!ok) failures++;

    var shown = input is null ? "null" : $"\"{input}\"";
    var name = method == "phone" ? "IsValidEgyptianPhone" : "IsValidEgyptianNationalId";
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {shown,-18} {name,-26} expected {expected,-5} got {actual}");
}

Console.WriteLine();
Console.WriteLine(failures == 0
    ? $"All {cases.Length} checks passed."
    : $"{failures} of {cases.Length} checks FAILED.");

return failures == 0 ? 0 : 1;
