using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Infrastructure.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repository;

        public AgentService(IRepository _repository)
        {
            repository = _repository;
        }

        public Task Create(string userId, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByIdAsync(string userId)
        {
            return await repository.AllReadOnly<Agent>()
                .AnyAsync(a=>a.UserId == userId);
        }

        public Task<bool> UserHasRents(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
