using TestPipeline.Models;

namespace TestPipeline.Services.Abstractions
{
    public interface IUserQueryService
    {
        User GetUserById(int id);

        User GetUserByEmail(string email);

    }
}
