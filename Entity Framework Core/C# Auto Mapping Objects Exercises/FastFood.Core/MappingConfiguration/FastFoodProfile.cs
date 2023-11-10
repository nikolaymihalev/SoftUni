namespace FastFood.Core.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Core.ViewModels.Categories;
    using FastFood.Core.ViewModels.Employees;
    using FastFood.Core.ViewModels.Items;
    using FastFood.Models;
    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId, y => y.MapFrom(z => z.Id));

            //Categories
            CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x=>x.Name, y=> y.MapFrom(z=>z.CategoryName));

            CreateMap<Category, CategoryAllViewModel>();

            //Employees
            CreateMap<RegisterEmployeeInputModel, Employee>();

            CreateMap<Category, CreateItemViewModel>();
            
        }
    }
}
