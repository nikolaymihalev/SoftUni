using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Services.House
{
    public class HouseService : IHouseService
    {
        public Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
        {
            throw new NotImplementedException();
        }
    }
}