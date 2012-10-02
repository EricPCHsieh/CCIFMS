using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Devart.Data.Oracle;

namespace CCIFMS
{
  public class OraDBUtil
  {
    public static string OraConnectionString
    {
      // change here for database connection
      // target direct connect to oracle
      get
      {
        return System.Configuration.ConfigurationManager.ConnectionStrings["SFISconnectionstring"].ConnectionString;
      }
    }

    public static Dictionary<string, object> ExecuteStoreProc(string procedureName, Dictionary<string, object> parameters)
    {
      // add comment for testing
      var result = new Dictionary<string, object>();
      using (OracleConnection connection = new OracleConnection(connectionString: OraConnectionString))
      {
        connection.Open();
        using (OracleCommand command = new OracleCommand())
        {
          command.Connection = connection;
          command.CommandType = CommandType.StoredProcedure;
          command.CommandText = procedureName;
          command.ParameterCheck = true;
          command.PassParametersByName = true;
          // setup parameters
          foreach (var param in parameters)
          {
            var p = command.Parameters.Add(new OracleParameter(param.Key, param.Value));
            p.Direction = ParameterDirection.InputOutput;
          }
          command.ExecuteNonQuery();
          // get output value
          foreach (OracleParameter param in command.Parameters)
          {
            result.Add(param.ParameterName, param.Value);
          }
        }
        connection.Close();
      }
      return result;
    }

    public static object ExecuteSQL(string sqlstring, Dictionary<string, object> parameters)
    {
      object result = null;
      using (OracleConnection connection = new OracleConnection(connectionString: OraConnectionString))
      {
        connection.Open();
        using (OracleCommand command = new OracleCommand())
        {
          command.Connection = connection;
          command.CommandType = CommandType.Text;
          command.CommandText = sqlstring;
          // setup parameters
          if (parameters != null)
          {
            foreach (var param in parameters)
            {
              var p = command.Parameters.Add(new OracleParameter(param.Key, param.Value));
              p.Direction = ParameterDirection.InputOutput;
            }
          }
          using (var rd = command.ExecuteReader())
          {
            if (rd.Read())
            {
              result = rd[0];
            }
          }
        }
        connection.Close();
      }
      return result;
    }
    public static object ExecuteSQL(string sqlstring)
    {
      return ExecuteSQL(sqlstring, null);
    }
  }
}
