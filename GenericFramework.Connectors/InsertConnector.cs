namespace GenericFramework.Connectors
{
    using GenericFramework.SystemCore;
    using System.Collections.Generic;
    using System.Data;
    public class InsertConnector:IInsertConnector
    {
        public ISystemCoreManager SystemCoreManager { get; set; }
        IInsertGateway insertGateway;

        public IInsertResult Insert(IInsertReq req, IInsertResult result, string commandText, string cmdParamPrefix, List<IDbDataParameter> InsertReqOutParamsList, System.Data.CommandType cmdType, bool ignoreConflict)
        {
            ErrorCode errorCode = ErrorCode.Other;
            if (req.ValidateRequest(req, out errorCode))
            {
                this.insertGateway = this.insertGateway ?? this.SystemCoreManager.InsertGateway;
                return this.insertGateway.InsertSingle(req, result, cmdType, commandText, cmdParamPrefix, InsertReqOutParamsList, ignoreConflict);
            }
            result.HasError = 1;
            //result.Message = prepare error message
            return result;
        }
    }
}
