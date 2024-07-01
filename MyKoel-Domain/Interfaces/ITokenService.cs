using API.Entities;

namespace MyKoel_Domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, int expiryMin = 0);
    }
}
