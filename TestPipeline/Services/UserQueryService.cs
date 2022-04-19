using Sequelizator;
using Sequelizator.Models;
using TestPipeline.Models;
using TestPipeline.Services.Abstractions;

namespace TestPipeline.Services
{
    public class UserQueryService : IUserQueryService
    {

        private readonly IConnectionProvider connectionProvider;


        public UserQueryService(IConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public User GetUserById(int userId)
        {
            string query = $"select idutente as 'Id', utente as 'Name', emailutente as 'Email', abilitato as 'Enabled', idcliente as 'ClientId' from ConnessioniUtenti where idutente  = {userId}";
            var res = connectionProvider.ExecuteSqlQuery<IEnumerable<User>>(new Query(query));
            return res.FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            string query = $"select idutente as 'Id', utente as 'Name', emailutente as 'Email', abilitato as 'Enabled', idcliente as 'ClientId' from ConnessioniUtenti where emailutente = '{email}'";
            var res = connectionProvider.ExecuteSqlQuery<IEnumerable<User>>(new Query(query));
            return res.FirstOrDefault();
        }
    }
}
