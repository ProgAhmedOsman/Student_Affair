//using APP.SharedKernel.Constants;
//using APP.SharedKernel.DistributedLock.DistributedLock.Sql.Clients;
//using APP.SharedKernel.DistributedLock.DistributedLock.Sql.Contracts;
 

//namespace Linkdev.Framework.DistributedLock.Sql
//{
//    public class DistributedLockDependenciesLoader : IDependenciesLoader
//    {
//        public void Load(IConfiguration configuration, IServiceCollection services)
//        {
//            services.AddSingleton(configuration.GetSection(AppSetingKeyConstanta.SqlDistributedLockSection).Get<DistributedLockConfiguration>());
//            services.AddScoped<IDistributedLockClient, DistributedLockClient>();
//        }
//    }
//}
