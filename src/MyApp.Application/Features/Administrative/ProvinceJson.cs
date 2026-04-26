namespace MyApp.Application.Features.Administrative
{
    public class ProvinceJson
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? EnglishName { get; set; }
        public string AdministrativeLevel { get; set; } = null!;
        public string? Decree { get; set; }
    }
}
