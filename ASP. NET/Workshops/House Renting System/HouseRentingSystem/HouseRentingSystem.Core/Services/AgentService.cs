﻿using HouseRentingSystem.Core.Contracts;
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

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            await repository.AddAsync<Agent>(new Agent() { UserId = userId, PhoneNumber = phoneNumber });
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string userId)
        {
            return await repository.AllReadOnly<Agent>()
                .AnyAsync(a=>a.UserId == userId);
        }

        public async Task<int?> GetAgetIdAsync(string userId)
        {
            return (await repository.AllReadOnly<Agent>()
                .FirstOrDefaultAsync(x=>x.UserId == userId))?.Id;
        }

        public async Task<bool> UserHasRentsAsync(string userId)
        {
            return await repository.AllReadOnly<House>()
                .AnyAsync(x => x.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await repository.AllReadOnly<Agent>()
                .AnyAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
