namespace GenericFramework.Gateway
{
    using GenericFramework.SystemCore;
    using GenericFramework.DbContextAPI;
    using System.Data;
    using GenericFramework.GenericUtils;
    using System.Collections.Generic;
    public class InsertGateway : GatewayBase, IInsertGateway
    {
        public ISystemCoreManager SystemCoreManager { get; set; }
        public IInsertResult InsertSingle(IInsertReq req, IInsertResult result, CommandType cmdType, string cmdText, string cmdParamPrefix, List<IDbDataParameter> InsertReqOutParamsList, bool ignoreConflict)
        {
            this.command = this.command ??
                ObjectFactory.CreateCommandObject(SystemCoreManager.DbEngine, SystemCoreManager.DbConnctivity);
            this.genericDataMapper = this.SystemCoreManager.GenericDataMapper ?? this.SystemCoreManager.GenericDataMapper;

            if (this.SystemCoreManager.GenericDataMapper.MapCommandFromBusinessObject<IInsertReq>(req, ref this.command, cmdParamPrefix, ignoreConflict))
            {
                this.connection = this.connection ??
                    ObjectFactory.CreateConnectionObject(SystemCoreManager.DbEngine, SystemCoreManager.DbConnctivity);
                this.connectionString = SystemCoreManager.ConnectionString;
                this.dbApi = this.dbApi ?? this.SystemCoreManager.DbContextAPI;

                if (ObjectFactory.PrepareInsertReqOutParams(ref this.command, InsertReqOutParamsList,
                    this.SystemCoreManager.DbEngine, this.SystemCoreManager.DbConnctivity))
                {
                    this.dbApi.ExcecuteNonQuery(this.command, cmdText, cmdType, this.connection, this.connectionString);

                    this.SystemCoreManager.GenericDataMapper.MapBusinssObjectFromCommand(ref result, this.command, cmdParamPrefix, ignoreConflict);
                    return result;
                }
                throw new GenericFrameworkException(new GenericFrameworkError()
                {
                    ErrorType = ErrorType.SysError,
                    ErrorCode = ErrorCode.SysError_GenericDataMapUtilityError_MapCommandFromBusinessObject,
                    ErrorMessage = "Failed to Populate OutParams"
                });

            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {

                ErrorType = ErrorType.SysError,
                ErrorCode = ErrorCode.SysError_GenericDataMapUtilityError_MapCommandFromBusinessObject,
                ErrorMessage = "Failed to Populate Command from Type " + req.GetType().ToString()
            });
        }
    }
}
