namespace MyApp.Application.Features.Administrative
{
    public class CommuneJson
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? EnglishName { get; set; }
        public string AdministrativeLevel { get; set; } = null!;
        public string ProvinceCode { get; set; } = null!;
        public string? Decree { get; set; }
    }
}
