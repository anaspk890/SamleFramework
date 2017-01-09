namespace GenericFramework.DbContextAPI
{
    using GenericFramework.SystemCore;
    using GenericFramework.DbContext;
using System.Data;
    public class DbContextAPI:IDbContextAPI
    {
        public ISystemCoreManager SystemCoreManager { get; set; }
        
        public int ExcecuteNonQuery(IDbCommand command, string commandText, CommandType commandType)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType))
            {
                PrepareDbExcecutionContext(command, commandText, commandType);
                return SystemCoreManager.DbContext.ExecuteNonQuery(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }

        public int ExcecuteNonQuery(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType, connection, connectionString))
            {
                PrepareDbExcecutionContext(command, commandText, commandType, connection, connectionString);
                return this.SystemCoreManager.DbContext.ExecuteNonQuery(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }

        public int ExcecuteNonQuery(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString, IDbTransaction transaction)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType, connection, connectionString, transaction))
            {
                PrepareDbExcecutionContext(command, commandText, commandType, connection, connectionString, transaction);
                return this.SystemCoreManager.DbContext.ExecuteNonQuery(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }


        public DataTable ExcecuteQuery(IDbCommand command, string commandText, CommandType commandType)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType))
            {
                PrepareDbExcecutionContext(command, commandText, commandType);
                return this.SystemCoreManager.DbContext.ExecuteQuery(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }

        public DataTable ExcecuteQuery(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType, connection, connectionString))
            {
                PrepareDbExcecutionContext(command, commandText, commandType, connection, connectionString);
                return this.SystemCoreManager.DbContext.ExecuteQuery(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }

        public DataTable ExcecuteQuery(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString, IDbTransaction transaction)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType, connection, connectionString, transaction))
            {
                PrepareDbExcecutionContext(command, commandText, commandType, connection, connectionString, transaction);
                return this.SystemCoreManager.DbContext.ExecuteQuery(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }


        public object ExcecuteScalar(IDbCommand command, string commandText, CommandType commandType)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType))
            {
                PrepareDbExcecutionContext(command, commandText, commandType);
                return this.SystemCoreManager.DbContext.ExecuteScalar(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }

        public object ExcecuteScalar(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType, connection, connectionString))
            {
                PrepareDbExcecutionContext(command, commandText, commandType, connection, connectionString);
                return this.SystemCoreManager.DbContext.ExecuteScalar(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }

        public object ExcecuteScalar(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString, IDbTransaction transaction)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType, connection, connectionString, transaction))
            {
                PrepareDbExcecutionContext(command, commandText, commandType, connection, connectionString, transaction);
                return this.SystemCoreManager.DbContext.ExecuteScalar(command);
            }
            throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidDbExcecutionContext
            });
        }


        private void PrepareDbExcecutionContext(IDbCommand command, string commandText, CommandType commandType)
        {
            command.CommandText = commandText;
            command.CommandType = commandType;
        }

        private void PrepareDbExcecutionContext(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString)
        {
            PrepareDbExcecutionContext(command, commandText, commandType);
            if (connection.ConnectionString == null) connection.ConnectionString = connectionString;
            if (command.Connection == null) command.Connection = connection;
            if (connection.ConnectionString == null ||
                string.IsNullOrEmpty(connection.ConnectionString) ||
                string.IsNullOrWhiteSpace(connection.ConnectionString) ||
                connection.ConnectionString.Trim().Length == 0) connection.ConnectionString = connectionString;
            if (connection.State != ConnectionState.Open) connection.Open();
        }

        private void PrepareDbExcecutionContext(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString, IDbTransaction transaction)
        {
            PrepareDbExcecutionContext(command, commandText, commandType, connection, connectionString);
            command.Transaction = transaction;
        }


        private bool ValidateDbExcecutionContext(IDbCommand command, string commandText, CommandType commandType)
        {
            if (command == null) throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidCommandObject
            });
            else if (string.IsNullOrEmpty(commandText)) throw new GenericFrameworkException(new GenericFrameworkError()
            {
                ErrorType = ErrorType.AppError,
                ErrorCode = ErrorCode.AppError_InvalidCommandText
            });
            else
                return true;
        }

        private bool ValidateDbExcecutionContext(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType))
            {
                if (connection == null) throw new GenericFrameworkException(new GenericFrameworkError()
                {
                    ErrorType = ErrorType.AppError,
                    ErrorCode = ErrorCode.AppError_InvalidConnectionObject
                });
                else if (string.IsNullOrEmpty(connectionString)) throw new GenericFrameworkException(new GenericFrameworkError()
                {
                    ErrorType = ErrorType.AppError,
                    ErrorCode = ErrorCode.AppError_InvalidConnectionString
                });
                return true;
            }
            return false;
        }

        private bool ValidateDbExcecutionContext(IDbCommand command, string commandText, CommandType commandType, IDbConnection connection, string connectionString, IDbTransaction transaction)
        {
            if (ValidateDbExcecutionContext(command, commandText, commandType, connection, connectionString))
            {
                if (transaction == null) throw new GenericFrameworkException(new GenericFrameworkError()
                {
                    ErrorType = ErrorType.AppError,
                    ErrorCode = ErrorCode.AppError_InvalidTransactionObject
                });
                return true;
            }
            return false;
        }

        internal static DataTable PrepareDataTableFromQueryResult(IDataReader reader)
        {
            DataTable tb = new DataTable();
            tb.Load(reader);
            return tb;
        }
    }
    }