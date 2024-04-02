namespace API;

public enum TagCategory
{
    General, Augment, Auton, Deck, Style, Creme, Sensor, Protection, Weapon
}

public static class TagCategoryExtension
{
    public static string ToLocalisedString(this TagCategory category, Localisation localisation)
    {
        if (localisation == Localisation.EN) return category.ToString();

        return category switch 
        {
            TagCategory.General     => "Allmänt",
            TagCategory.Augment     => "Augment",
            TagCategory.Auton       => "Auton",
            TagCategory.Deck        => "Deck",
            TagCategory.Style       => "Stil",
            TagCategory.Creme       => "Kräm",
            TagCategory.Sensor      => "Sensor",
            TagCategory.Protection  => "Skydd",
            TagCategory.Weapon      => "Vapen",
            _ => ""
        };
    }
}
