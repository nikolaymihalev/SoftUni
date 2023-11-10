namespace FastFood.Core.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Core.ViewModels.Categories;
    using FastFood.Core.ViewModels.Employees;
    using FastFood.Core.ViewModels.Items;
    using FastFood.Core.ViewModels.Orders;
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

            //Item
            CreateMap<Category, CreateItemViewModel>()
                .ForMember(ci=>ci.CategoryId, c=>c.MapFrom(x=>x.Id));

            CreateMap<Item, ItemsAllViewModels>()
                .ForMember(i => i.Category, c => c.MapFrom(x => x.Category.Name));
            
            CreateMap<Order, OrderAllViewModel>()
                .ForMember(i => i.Customer, c => c.MapFrom(x => x.Customer));
        }
    }
}
