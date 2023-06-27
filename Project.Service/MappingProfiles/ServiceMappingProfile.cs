using AutoMapper;
using Project.Service.Models;
using Project.Service.DTO;
using X.PagedList;

namespace Project.Service.MappingProfiles
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDto>();
            CreateMap<VehicleMakeDto, VehicleMake>();

            CreateMap<VehicleModel, VehicleModelDto>();
            CreateMap<VehicleModelDto, VehicleModel>();

            CreateMap<IPagedList<VehicleMake>, IPagedList<VehicleMakeDto>>()
                .ConvertUsing<PagedListConverter<VehicleMake, VehicleMakeDto>>();

            CreateMap<IPagedList<VehicleModel>, IPagedList<VehicleModelDto>>()
                .ConvertUsing<PagedListConverter<VehicleModel, VehicleModelDto>>();
        }
    }

    public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
    {
        public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
        {
            var convertedList = new StaticPagedList<TDestination>(
                context.Mapper.Map<List<TDestination>>(source),
                source.PageNumber,
                source.PageSize,
                source.TotalItemCount);

            return convertedList;
        }
    }
}
