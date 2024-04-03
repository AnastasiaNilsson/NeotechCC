namespace API;

public enum Localisation
{
    SE, EN
}

public static class LocalisationExtension
{
    public static Localisation ToLocalisationEnum(this string localisationString)
    {
        var localisationExists = Enum.TryParse<Localisation>(localisationString.ToUpper(), out var localisationEnum);
        if (!localisationExists) throw new ArgumentException("Requested localisation does not exist");

        return localisationEnum;
    }
}