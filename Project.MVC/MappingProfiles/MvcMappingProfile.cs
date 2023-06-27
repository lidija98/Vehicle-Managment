using AutoMapper;
using Project.MVC.Models;
using Project.Service.DTO;
using X.PagedList;

namespace Project.MVC.MappingProfiles
{
	public class MvcMappingProfile : Profile
	{
		public MvcMappingProfile()
		{
			CreateMap<VehicleMakeDto, VehicleMakeViewModel>();
			CreateMap<VehicleMakeViewModel, VehicleMakeDto>();

			CreateMap<VehicleModelDto, VehicleModelViewModel>();
			CreateMap<VehicleModelViewModel, VehicleModelDto>();


            CreateMap(typeof(IPagedList<>), typeof(IPagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));

        }
    }

    public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
    {
        public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
        {
            var convertedList = new StaticPagedList<TDestination>(
                context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source),
                source.GetMetaData());

            return convertedList;
        }
    }

}

