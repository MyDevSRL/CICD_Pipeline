using Cryptography.Standard;

using Sequelizator;
using Sequelizator.Models;
using TestPipeline.Services;
using TestPipeline.Services.Abstractions;

namespace TestPipeline.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void Setup(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();

            var devCorsPolicy = "devCorsPolicy";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(devCorsPolicy, builder => {
                    builder
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


           //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //    builder.Services.AddSwaggerGen();

        }

        public static void SetupConfigurations(this WebApplicationBuilder builder)
        {
            string connectionString = Crypto.Decrypt("3t2Ix0AuCVxvfIrIfUG+9gpypXd7PclJzOtJ8dWmTcU=:qxwQMv1N64XaQBewGhipgg==:98/35Or0fODoRPRp/fnyfciQwQSJBQUqmLwx/4es0A38r8PTeeoYu0s0tgahI6T7FZVu3XxdosSKNe2FpTbLpVKn+/QeIAWxw+GdZJ83XF9eQAzDkcZ6/djIx4pKnBHJ7HotGHo0/DJt0Vosp6FexosuWeouKApTgu0MhZDGX1w=");
            builder.Services.AddSingleton<IConnectionProvider>(cp => new ConnectionProvider("DEV3_netwatch"));
            builder.Services.AddSingleton<IUserQueryService, UserQueryService>();

        }

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
           

        }
    }
}