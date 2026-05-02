namespace Phoneword;

public static class PhonewordTranslator
{
    public static string ToNumber(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return null;

        raw = raw.ToUpperInvariant();

        var newNumber = new System.Text.StringBuilder();
        foreach (var c in raw)
        {
            if (" -0123456789".Contains(c))
                newNumber.Append(c);
            else
            {
                var result = TranslateToNumber(c);
                if (result != null)
                    newNumber.Append(result);
            }
        }
        return newNumber.ToString();
    }

    static int? TranslateToNumber(char c)
    {
        if ("ABC".Contains(c)) return 2;
        if ("DEF".Contains(c)) return 3;
        if ("GHI".Contains(c)) return 4;
        if ("JKL".Contains(c)) return 5;
        if ("MNO".Contains(c)) return 6;
        if ("PQRS".Contains(c)) return 7;
        if ("TUV".Contains(c)) return 8;
        if ("WXYZ".Contains(c)) return 9;
        return null;
    }
}