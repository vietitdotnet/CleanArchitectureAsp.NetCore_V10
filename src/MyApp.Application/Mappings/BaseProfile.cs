using AutoMapper;

namespace MyApp.Application.Mappings
{

    /// <summary>
    /// Base class cho mọi Profile mapping.
    /// Giúp chuẩn hóa cấu trúc Manager / Request / Response.
    /// </summary>
    public abstract class BaseProfile : Profile
    {
        public BaseProfile()
        {
        }

    }
}

