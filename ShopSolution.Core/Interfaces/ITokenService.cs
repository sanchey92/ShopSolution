using ShopSolution.Core.Entities.Identity;

namespace ShopSolution.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}