namespace GenericFramework.GenericUtils
{
    using GenericFramework.SystemCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    public sealed class ObjectFactory
    {
        static IGenericFrameworkError error;
        public static ISystemCoreManager SystemCoreManager { get; set; }
        ObjectFactory()
        {
            error = error ?? (IGenericFrameworkError)Activator.CreateInstance(SystemCoreManager.GenericFrameworkError.GetType());
        }



        public static object CreateObject<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public static object CreateObject<T>(object[] paramList)
        {
            return Activator.CreateInstance(typeof(T), paramList);
        }

        public static object CreateObject(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static object CreateObject(Type type, object[] paramList)
        {
            return Activator.CreateInstance(type, paramList);
        }

        public static IList CreateObjectList(Type type)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(type);
            var instance = Activator.CreateInstance(constructedListType);
            return (IList)instance;
        }

        public static IList CreateObjectList<T>()
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(typeof(T));
            var instance = Activator.CreateInstance(constructedListType);
            return (IList)instance;
        }

        public static IList CreateObjectList(Type type, object[] paramList)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(type);
            var instance = Activator.CreateInstance(constructedListType, paramList);
            return (IList)instance;
        }

        public static IList CreateObjectList<T>(object[] paramList)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(typeof(T));
            var instance = Activator.CreateInstance(constructedListType, paramList);
            return (IList)instance;
        }

        public static IDbCommand CreateCommandObject(DbEngine dbEngine = DbEngine.MSSQLServer,
            DbConnectivity connectivity = DbConnectivity.AdoDotNet)
        {
            switch (connectivity)
            {
                case DbConnectivity.Odbc:
                    return (IDbCommand)CreateObject<System.Data.Odbc.OdbcCommand>();
                case DbConnectivity.OleDb:
                    return (IDbCommand)CreateObject<System.Data.OleDb.OleDbCommand>();
                case DbConnectivity.AdoDotNet:
                    return CreateCommandObject(dbEngine);
                default:
                    error.ErrorCode = ErrorCode.AppError_InvalidDbConnectivity;
                    error.ErrorType = ErrorType.AppError;
                    throw new GenericFrameworkException(error);
            }

        }

        public static IDbCommand CreateCommandObject(DbEngine dbEngine)
        {
            switch (dbEngine)
            {
                case DbEngine.MSSQLServer:
                    return (IDbCommand)CreateObject<System.Data.SqlClient.SqlCommand>();
                case DbEngine.MySql:
                    return (IDbCommand)CreateObject<MySql.Data.MySqlClient.MySqlCommand>();
                case DbEngine.Oracle:
                    return (IDbCommand)CreateObject<Oracle.ManagedDataAccess.Client.OracleCommand>();
                default:
                    error.ErrorCode = ErrorCode.AppError_InvalidDbEngine;
                    error.ErrorType = ErrorType.AppError;
                    throw new GenericFrameworkException(error);
            }
        }

        public static IDbConnection CreateConnectionObject(DbEngine dbEngine = DbEngine.MSSQLServer,
            DbConnectivity connectivity = DbConnectivity.AdoDotNet)
        {
            switch (connectivity)
            {
                case DbConnectivity.Odbc:
                    return (IDbConnection)CreateObject<System.Data.Odbc.OdbcConnection>();
                case DbConnectivity.OleDb:
                    return (IDbConnection)CreateObject<System.Data.OleDb.OleDbConnection>();
                case DbConnectivity.AdoDotNet:
                    return CreateConnectionObject(dbEngine);
                default:
                    error.ErrorCode = ErrorCode.AppError_InvalidDbConnectivity;
                    error.ErrorType = ErrorType.AppError;
                    throw new GenericFrameworkException(error);
            }

        }

        public static IDbConnection CreateConnectionObject(DbEngine dbEngine)
        {
            switch (dbEngine)
            {
                case DbEngine.MSSQLServer:
                    return (IDbConnection)CreateObject<System.Data.SqlClient.SqlConnection>();
                case DbEngine.MySql:
                    return (IDbConnection)CreateObject<MySql.Data.MySqlClient.MySqlConnection>();
                case DbEngine.Oracle:
                    return (IDbConnection)CreateObject<Oracle.ManagedDataAccess.Client.OracleConnection>();
                default:
                    error.ErrorCode = ErrorCode.AppError_InvalidDbEngine;
                    error.ErrorType = ErrorType.AppError;
                    throw new GenericFrameworkException(error);
            }
        }

        public static IDbDataParameter CreateParameterObject(DbEngine dbEngine = DbEngine.MSSQLServer,
            DbConnectivity connectivity = DbConnectivity.AdoDotNet)
        {
            switch (connectivity)
            {
                case DbConnectivity.Odbc:
                    return CreateParameterObject(dbEngine);
                case DbConnectivity.OleDb:
                    return CreateParameterObject(dbEngine);
                case DbConnectivity.AdoDotNet:
                    return CreateParameterObject(dbEngine);
                default:
                    error.ErrorCode = ErrorCode.AppError_InvalidDbConnectivity;
                    error.ErrorType = ErrorType.AppError;
                    throw new GenericFrameworkException(error);
            }
        }

        public static IDbDataParameter CreateParameterObject(DbEngine dbEngine = DbEngine.MSSQLServer)
        {
            switch (dbEngine)
            {
                case DbEngine.MSSQLServer:
                    return (System.Data.IDbDataParameter)CreateObject<System.Data.SqlClient.SqlParameter>();
                case DbEngine.MySql:
                    return (System.Data.IDbDataParameter)CreateObject<MySql.Data.MySqlClient.MySqlParameter>();
                case DbEngine.Oracle:
                    return (System.Data.IDbDataParameter)CreateObject<Oracle.ManagedDataAccess.Client.OracleParameter>();
                default:
                    error.ErrorCode = ErrorCode.AppError_InvalidDbEngine;
                    error.ErrorType = ErrorType.AppError;
                    throw new GenericFrameworkException(error);
            }
        }

        public static System.Data.IDbDataParameter CreateParameterObject(string paramName, System.Data.ParameterDirection paramDirection,
            object paramValue, DbEngine dbEngine = DbEngine.MSSQLServer, DbConnectivity connectivity = DbConnectivity.AdoDotNet)
        {
            System.Data.IDbDataParameter param = CreateParameterObject(dbEngine, connectivity);
            param.ParameterName = paramName;
            param.Direction = paramDirection;
            param.Value = paramValue;
            return param;
        }

        public static System.Data.IDbDataParameter CreateParameterObject(string paramName, System.Data.ParameterDirection paramDirection,
            DbType dbType, object paramValue, int Size, DbEngine dbEngine = DbEngine.MSSQLServer, DbConnectivity connectivity = DbConnectivity.AdoDotNet)
        {
            System.Data.IDbDataParameter param = CreateParameterObject(dbEngine, connectivity);
            param.ParameterName = paramName;
            param.Direction = paramDirection;
            param.Value = paramValue;
            param.DbType = dbType;
            param.Size = Size;
            return param;
        }

        public static bool PrepareInsertReqOutParams(ref IDbCommand command, List<IDbDataParameter> outParamList, DbEngine dbEngine = DbEngine.MSSQLServer,
            DbConnectivity connectivity = DbConnectivity.AdoDotNet)
        {
            int _rowsCount = command.Parameters != null ? command.Parameters.Count : 0;
            foreach (IDbDataParameter param in outParamList) command.Parameters.Add(param);
            return command.Parameters.Count == outParamList.Count + _rowsCount;
        }

        public static List<System.Data.IDbDataParameter> CreateParameterList(DbEngine dbEngine = DbEngine.MSSQLServer, DbConnectivity connectivity = DbConnectivity.AdoDotNet)
        {
            switch (connectivity)
            {
                case DbConnectivity.Odbc:
                    return new List<System.Data.Odbc.OdbcParameter>().Cast<System.Data.IDbDataParameter>().ToList();
                case DbConnectivity.OleDb:
                    return new List<System.Data.OleDb.OleDbParameter>().Cast<System.Data.IDbDataParameter>().ToList();
                case DbConnectivity.AdoDotNet:
                    return new List<System.Data.SqlClient.SqlParameter>().Cast<System.Data.IDbDataParameter>().ToList();
                default:
                    error.ErrorCode = ErrorCode.AppError_InvalidDbConnectivity;
                    error.ErrorType = ErrorType.AppError;
                    throw new GenericFrameworkException(error);
            }
        }

        public static List<System.Data.IDbDataParameter> CreateParameterList(DbEngine dbEngine = DbEngine.MSSQLServer)
        {
            switch (dbEngine)
            {
                case DbEngine.MSSQLServer:
                    return new List<System.Data.SqlClient.SqlParameter>().Cast<System.Data.IDbDataParameter>().ToList();
                case DbEngine.MySql:
                    return new List<MySql.Data.MySqlClient.MySqlParameter>().Cast<System.Data.IDbDataParameter>().ToList();
                case DbEngine.Oracle:
                    return new List<Oracle.ManagedDataAccess.Client.OracleParameter>().Cast<System.Data.IDbDataParameter>().ToList();
                default:
                    error.ErrorCode = ErrorCode.AppError_InvalidDbEngine;
                    error.ErrorType = ErrorType.AppError;
                    throw new GenericFrameworkException(error);
            }
        }
    }
}
