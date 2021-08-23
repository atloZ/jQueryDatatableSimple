using jQueryDatatableMVCSimple.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class DbConn
    {
        #region DB processes
        public SqlConnection Connection { get; set; }

        public static DbConn Instance()
        {
            return new DbConn();
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                string connstring = "Server=localhost;Database=AdventureWorks2019;Trusted_Connection=True;";
                Connection = new SqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        private static List<string> SendQuery(string query)
        {
            List<string> data = new List<string>();
            var dbCon = Instance();
            if (dbCon.IsConnect())
            {
                var cmd = new SqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        data.Add(reader[i].ToString());
                    }
                }
            }
            dbCon.Close();

            return data;
        }

        public void Close()
        {
            this.Connection.Close();
        }
        #endregion

        public static string test()
        {
            string k = "";
            List<string> s = SendQuery("SELECT top(2)" +
                "[AddressID] ,[AddressLine1] ," +
                "[AddressLine2] ,[City] ," +
                "[StateProvinceID] ,[PostalCode] ," +
                "[SpatialLocation] ,[rowguid] ," +
                "[ModifiedDate] " +
                "FROM " +
                "[AdventureWorks2019].[Person].[Address]");
            for (int i = 0; i < s.Count; i++)
            {
                k += s[i] + "\n";
            }
            return k;
        }

        #region Data query
        public DataModel getAllData()
        {
            List<string> row = SendQuery(
                "SELECT " +
                "[AddressID] ,[AddressLine1] ," +
                "[AddressLine2] ,[City] ," +
                "[StateProvinceID] ,[PostalCode] ," +
                "[SpatialLocation] ,[rowguid] ," +
                "[ModifiedDate] " +
                "FROM " +
                "[AdventureWorks2019].[Person].[Address]");



            return null;
        }
        #endregion

    }
}