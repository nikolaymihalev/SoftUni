namespace HouseRentingSystem.Core.Contracts
{
    public interface IAgentService
    {
        Task<bool> ExistsByIdAsync(string userId);
        Task<bool> UserWithPhoneNumberExists(string phoneNumber);
        Task<bool> UserHasRents(string userId);
        Task Create(string userId, string phoneNumber);
    }
}