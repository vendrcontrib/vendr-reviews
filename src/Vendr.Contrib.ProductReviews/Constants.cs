namespace Vendr.Contrib.ProductReviews
{
    /// <summary>
    /// Constants all the identifiers
    /// </summary>
    public static partial class Constants
    {
        // generic constants can go here

        public static class DatabaseSchema
        {
            public const string TableNamePrefix = "vendr";

            public static class Tables
            {
                public const string ProductReviews = TableNamePrefix + "ProductReviews";
            }
        }

        public static class Trees
        {
            public static class Icons
            {
                /// <summary>
                /// System review icon
                /// </summary>
                public const string Review = "icon-rate";
            }
        }

    }
}