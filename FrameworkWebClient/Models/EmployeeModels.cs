
using GenericFramework.SystemCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FrameworkWebClient.Models
{

    public class GenericModel
    {
        IInsertConnector insertConnector;
        public GenericModel(IInsertConnector insertConnector)
        {
            this.insertConnector = insertConnector;
        }
        public IInsertResult InsertItem(IInsertReq reqModel,IInsertResult resultModel,string commandText,string cmdPrefix, CommandType cmdType,List<IDbDataParameter> outParamsList,bool ignoreConflict)
        {
            return this.insertConnector.Insert(reqModel, resultModel, commandText, cmdPrefix, outParamsList, cmdType, ignoreConflict);
        }
    }
    public abstract class EmployeeBase
    {
        public virtual string EmpName { get; set; }
        public virtual string DepName { get; set; }
    }

    public class EmployeeInsertReq : EmployeeBase, IInsertReq
    {
        public bool ValidateRequest(IInsertReq req, out ErrorCode errorCode)
        {
            errorCode = ErrorCode.None;
            return true;
        }
    }

    public class EmployeeInsertResult : EmployeeBase, IInsertResult
    {
        public long? NewId { get; set; }
        public int? ErrorCode { get; set; }
        public int? HasError { get; set; }
        public string Message { get; set; }
    }
}