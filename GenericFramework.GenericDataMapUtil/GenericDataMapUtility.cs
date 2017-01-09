namespace GenericFramework.GenericDataMapUtil
{
    using GenericFramework.GenericUtils;
    using GenericFramework.SystemCore;
    using System;
    using System.Data;
    using System.Reflection;
    public sealed class GenericDataMapUtility : IGenericDataMapper
    {
        public ISystemCoreManager SystemCoreManager { get; set; }
        bool ValidateObject(System.Data.IDbCommand command)
        {
            if (command == null)
                throw new GenericFrameworkException(new GenericFrameworkError()
                {
                    ErrorType = ErrorType.AppError,
                    ErrorCode = ErrorCode.AppError_InvalidCommandObject
                });
            return true;
        }

        public bool MapCommandFromBusinessObject<T>(T sourceData, ref IDbCommand command, string cmdPrefix, bool ignoreConflict)
        {
            bool status = false;
            if (ValidateObject(command))
            {
                try
                {
                    foreach (PropertyInfo property in sourceData.GetType().GetProperties())
                    {
                        object bindValue = property.GetValue(sourceData, null);
                        if (property != null)
                        {
                            if (property.PropertyType == typeof(string))
                            {
                                string tempString = Convert.ToString(bindValue);
                                if (string.IsNullOrEmpty(tempString))
                                {
                                    bindValue = null;
                                }
                            }
                        }
                        System.Data.IDbDataParameter param =
                            ObjectFactory.CreateParameterObject(this.SystemCoreManager.DbEngine, this.SystemCoreManager.DbConnctivity);
                        param.ParameterName = cmdPrefix + property.Name;
                        param.Value = bindValue;
                        command.Parameters.Add(param);
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new GenericFrameworkException(new GenericFrameworkError()
                    {
                        ErrorType = ErrorType.SysError,
                        ErrorCode = ErrorCode.SysError_GenericDataMapUtilityError_MapCommandFromBusinessObject,
                        ErrorMessage = ex.Message
                    });
                }
            }
            return status;
        }

        public bool MapBusinssObjectFromCommand<T>(ref T destinationObject, IDbCommand command, string cmdPrefix, bool ignoreConflict)
        {
            bool status = false;
            if (ValidateObject(command))
            {
                try
                {
                    foreach (System.Data.IDbDataParameter param in command.Parameters)
                    {
                        if (param != null && param.ParameterName != null)
                        {
                            string _paramName = param.ParameterName.ToLower().StartsWith(cmdPrefix.ToLower()) ?
                                    param.ParameterName.Substring(cmdPrefix.Length,
                                    (param.ParameterName.Trim().Length - cmdPrefix.Length)).Trim().ToLower() :
                                    param.ParameterName.Trim().ToLower();

                            PropertyInfo property = destinationObject.GetType().GetProperty(_paramName);

                            if (property == null)
                            {
                                property = destinationObject.GetType().GetProperty(_paramName,
                                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            }

                            if (property == null)
                            {
                                if (ignoreConflict) continue;

                                throw new GenericFrameworkException(new GenericFrameworkError()
                                {
                                    ErrorType = ErrorType.AppError,
                                    ErrorCode = ErrorCode.AppError_PropertyNameNotFoundInCommandParameterNameCollection,
                                    ErrorMessage = "Parameter Name : '" + param.ParameterName + "'"
                                });
                            }

                            if (property.Name.ToLower().Trim() == _paramName)
                            {
                                object value = param.Value;
                                if (property.CanWrite)
                                {
                                    try
                                    {
                                        //property.SetValue(destinationObject, param.Value);
                                        property.SetValue(destinationObject, param.Value, null);
                                        status = true;
                                    }
                                    catch
                                    {
                                        if (value is DBNull) continue;

                                        if (value.GetType() != property.PropertyType)
                                        {
                                            Type type = property.PropertyType;
                                            property.SetValue(destinationObject, Convert.ChangeType(value, type), null);
                                            status = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ignoreConflict) continue;
                            throw new GenericFrameworkException(new GenericFrameworkError()
                            {
                                ErrorType = ErrorType.AppError,
                                ErrorCode = ErrorCode.AppError_CommandObjectDoesNotContainFieldWithPrpertyName,
                                ErrorMessage = "CommandText : '" + command.CommandText + "'"
                            });

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new GenericFrameworkException(new GenericFrameworkError()
                    {
                        ErrorType = ErrorType.SysError,
                        ErrorCode = ErrorCode.SysError_GenericDataMapUtilityError_MapBusinssObjectFromCommand,
                        ErrorMessage = ex.Message
                    });
                }
            }
            return status;
        }

        public bool SetPropertyValue<T>(T destinationObj, string propertyName, object value, bool ignoreCase)
        {
            PropertyInfo property = destinationObj.GetType().GetProperty(propertyName);
            if (property == null && ignoreCase)
            {
                property = destinationObj.GetType().GetProperty(propertyName,
                                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            }
            if (property == null) throw new ArgumentNullException("Proprty '" + propertyName + "' not found for Type '" +
                  destinationObj.GetType().Name + "'");
            try
            {
                property.SetValue(destinationObj, value, null);
            }
            catch
            {
                Type type = property.PropertyType;
                try
                {
                    object _data = Convert.ChangeType(value, type);
                    property.SetValue(destinationObj, _data, null);
                }
                catch
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        object _data = Convert.ChangeType(value, type.GetGenericArguments()[0]);
                        property.SetValue(destinationObj, _data, null);
                    }
                }
            }

            return property != null;
        }
    }
}
