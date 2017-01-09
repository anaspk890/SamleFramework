namespace GenericFramework.DbContext
{
    using GenericFramework.SystemCore;
    using System.Data;
    public class DbContext:IDbContext
    {
        public int ExecuteNonQuery(IDbCommand command)
        {
            return command.ExecuteNonQuery();
        }

        public DataTable ExecuteQuery(IDbCommand command)
        {
            return ConvertToDataTable(command.ExecuteReader());
        }

        public object ExecuteScalar(IDbCommand command)
        {
            return command.ExecuteScalar();
        }

        private DataTable ConvertToDataTable(IDataReader reader)
        {
            DataTable tb = new DataTable();
            tb.Load(reader);
            return tb;
        }
    }
}
