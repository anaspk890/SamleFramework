namespace GenericFramework.SystemCore
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    #region enums
    public enum DbEngine { MSSQLServer = 1, MySql = 2, Oracle = 3 }
    public enum DbConnectivity { AdoDotNet = 1, Odbc = 2, OleDb = 3 }

    public enum ErrorCode
    {
        None = 0,
        AppError_InvalidDbExcecutionContext = 10001,
        AppError_InvalidCommandObject = 10002,
        AppError_InvalidCommandText = 10003,
        AppError_InvalidConnectionObject = 10004,
        AppError_InvalidConnectionString = 10005,
        AppError_InvalidTransactionObject = 10006,
        AppError_InvalidDbConnectivity = 10007,
        AppError_InvalidDbEngine = 10008,
        AppError_PropertyNameNotFoundInCommandParameterNameCollection = 10009,
        AppError_CommandObjectDoesNotContainFieldWithPrpertyName = 10010,

        SysError_GenericDataMapUtilityError_MapCommandFromBusinessObject = 40001,
        SysError_GenericDataMapUtilityError_MapBusinssObjectFromCommand = 40002,

        Other = 99999
    }

    public enum ErrorType
    {
        AppError = 10000,
        DbError = 20000,
        ValidationError = 30000,
        SysError = 40000
    }

    public enum Language
    {
        English = 1001,
        Arabic = 1002
    }
    #endregion

    #region Interfaces

    public interface IDbContext
    {
        int ExecuteNonQuery(IDbCommand command);
        DataTable ExecuteQuery(IDbCommand command);
        object ExecuteScalar(IDbCommand command);
    }
    public interface IDbContextAPI
    {
        ISystemCoreManager SystemCoreManager { get; set; }
        int ExcecuteNonQuery(IDbCommand command, string commandText, CommandType commandType);
        int ExcecuteNonQuery(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString);
        int ExcecuteNonQuery(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString, IDbTransaction transaction);

        DataTable ExcecuteQuery(IDbCommand command, string commandText, CommandType commandType);
        DataTable ExcecuteQuery(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString);
        DataTable ExcecuteQuery(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString, IDbTransaction transaction);


        object ExcecuteScalar(IDbCommand command, string commandText, CommandType commandType);
        object ExcecuteScalar(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString);
        object ExcecuteScalar(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString, IDbTransaction transaction);
    }

    public interface IInsertReq
    {
        bool ValidateRequest(IInsertReq req, out ErrorCode errorCode);
    }
    public interface IResult
    {
        int? ErrorCode { get; set; }
        int? HasError { get; set; }
        string Message { get; set; }
    }
    public interface IInsertResult : IResult
    {
        long? NewId { get; set; }
    }

    public interface IInsertGateway
    {
        ISystemCoreManager SystemCoreManager { get; set; }
        IInsertResult InsertSingle(IInsertReq req, IInsertResult result, CommandType cmdType, string cmdText, string cmdParamPrefix, List<IDbDataParameter> InsertReqOutParamsList, bool ignoreConflict);
    }


    public interface IInsertConnector
    {
        ISystemCoreManager SystemCoreManager { get; set; }
        IInsertResult Insert(IInsertReq req, IInsertResult result, string commandText, string cmdParamPrefix, List<IDbDataParameter> InsertReqOutParamsList, CommandType cmdType, bool ignoreConflict);
    }


    public interface IGenericDataMapper
    {
        ISystemCoreManager SystemCoreManager { get; set; }
        bool MapCommandFromBusinessObject<T>(T sourceData, ref IDbCommand command, string cmdPrefix, bool ignoreConflict);
        bool MapBusinssObjectFromCommand<T>(ref T destinationObject, IDbCommand command, string cmdPrefix, bool ignoreConflict);
        bool SetPropertyValue<T>(T destinationObj, string propertyName, object value, bool ignoreCase);
    }

    public interface ISystemCoreManager
    {
        DbEngine DbEngine { get; set; }
        DbConnectivity DbConnctivity { get; set; }
        IDbContext DbContext { get; set; }
        IDbContextAPI DbContextAPI { get; set; }
        IGenericDataMapper GenericDataMapper { get; set; }
        IInsertGateway InsertGateway { get; set; }
        string ConnectionString { get; set; }
        string CommandParamPrefix { get; set; }
        Language DefaultLanguage { get; set; }
        IInsertConnector InsertConnector { get; set; }
        IGenericFrameworkError GenericFrameworkError { get; set; }
        IGenericFrameworkApplicationError GenericFrameworkApplicationError { get; set; }
        GenericFrameworkException GenericFrameworkExceptionHandler { get; set; }
    }

    #endregion

    #region ErrorHandler
    public interface IGenericFrameworkError
    {
        ErrorCode ErrorCode { get; set; }
        ErrorType ErrorType { get; set; }
        string ErrorMessage { get; set; }
    }
    public interface IGenericFrameworkApplicationError
    {
        Language Language { get; set; }
        IGenericFrameworkError Error { get; set; }
    }

    public partial class GenericFrameworkError : IGenericFrameworkError
    {
        public ErrorType ErrorType { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }


    public class GenericFrameworkException : System.Exception
    {
        public IGenericFrameworkError error { get; set; }
        public GenericFrameworkException()
        {

        }
        public GenericFrameworkException(IGenericFrameworkError error)
        {
            // Handle error
        }
    }

    public partial class GenericFrameworkApplicationError : IGenericFrameworkApplicationError
    {
        public Language Language { get; set; }
        public IGenericFrameworkError Error { get; set; }
    }
    #endregion
}
