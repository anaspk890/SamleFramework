namespace GenericFramework.Gateway
{
    using GenericFramework.SystemCore;
    using System.Data;
    public abstract class GatewayBase
    {
        protected  IDbContextAPI dbApi { get; set; }
        protected  IDbCommand command;
        protected  IDbConnection connection;
        protected  IDbTransaction transaction;
        protected  string connectionString;
        protected  IGenericDataMapper genericDataMapper;
    }
}
