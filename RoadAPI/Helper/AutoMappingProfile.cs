using AutoMapper;
using RoadAPI.Data;
using RoadAPI.Models;

namespace RoadAPI.Helper
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<NewsModel, News>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => Guid.NewGuid())
               )
                .ForMember(dest => dest.PathToImage, opt => opt.Ignore());
            ;

            CreateMap<News, NewsModel>();
            CreateMap<News, NewsViewModel>();

            CreateMap<Report, ReportModel>().ReverseMap();

            CreateMap<AccountModel, Account>()
                   .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => Guid.NewGuid())
               )
                .ForMember(dest => dest.PathToImage, opt => opt.Ignore());
            CreateMap<Account, AccountViewModel>();
            //CreateMap<ImageReport, ImageReportModel>();

        }
    }
}
