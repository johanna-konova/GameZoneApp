namespace GameZoneApp.Infrastructure.Data
{
    public static class DataConstants
    {
        public static class Game
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMaxLength = 2048;

            public const string ReleasedOnDateTimeFormat = "yyyy-MM-dd";
        }

        public static class Ganre
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 25;
        }
    }
}
