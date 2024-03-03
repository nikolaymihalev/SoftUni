using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();
        Task<IEnumerable<HouseCategoryServiceModel>> AllCategories();
        Task<bool> CategoryExists(int categoryId);
        Task<int> Create(HouseFormViewModel model, int agentId);
    }
}