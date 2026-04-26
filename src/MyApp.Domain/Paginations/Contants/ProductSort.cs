namespace MyApp.Domain.Paginations.Contants
{
    public readonly record struct ProductSort(string Value)
    {
        public static ProductSort None => new(string.Empty);
        public static ProductSort PriceAsc => new("price_asc");
        public static ProductSort PriceDesc => new("price_desc");
        public static ProductSort DateAsc => new("date_asc");
        public static ProductSort DateDesc => new("date_desc");
        public static bool TryParse(string? value, out ProductSort sort)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                sort = None;
                return false;
            }          
            sort = value.Trim().ToLowerInvariant() switch
            {
                "price_asc" => PriceAsc,
                "price_desc" => PriceDesc,
                "date_asc" => DateAsc,
                "date_desc" => DateDesc,
                _ => None
            };

            return !sort.IsNone;
        }

        public bool IsNone => string.IsNullOrEmpty(Value);
    }

}