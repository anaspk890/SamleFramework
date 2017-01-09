
using GenericFramework.SystemCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FrameworkWebClient
{
    public class CoreManager : ISystemCoreManager
    {
        public DbEngine DbEngine { get; set; }

        public DbConnectivity DbConnctivity { get; set; }

        public IDbContext DbContext { get; set; }

        public IDbContextAPI DbContextAPI { get; set; }

        public IGenericDataMapper GenericDataMapper { get; set; }

        public IInsertGateway InsertGateway { get; set; }

        public string ConnectionString { get; set; }

        public string CommandParamPrefix { get; set; }

        public Language DefaultLanguage { get; set; }
        public IInsertConnector InsertConnector { get; set; }

        public IGenericFrameworkError GenericFrameworkError { get; set; }

        public IGenericFrameworkApplicationError GenericFrameworkApplicationError { get; set; }

        public GenericFrameworkException GenericFrameworkExceptionHandler { get; set; }
    }

    public class SystemConfig
    {
        public static ISystemCoreManager coreManger { get; set; }
    }
}