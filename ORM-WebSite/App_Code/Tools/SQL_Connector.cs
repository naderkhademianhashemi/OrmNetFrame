using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Diagnostics;

/// <summary>
/// Summary description for SQL_DB
/// </summary>
public class SQL_Connector
{
    StackTrace _stackTrace = new StackTrace();
    private SqlConnection Sqlconnection_credit_index = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ORM_ConnectionString"].ConnectionString);
    public SQL_Connector()
    {
        //Sqlconnection_credit_index.ConnectionTimeout=
        int x = Sqlconnection_credit_index.ConnectionTimeout;

        //
        // TODO: Add constructor logic here
        //
    }

    public bool Execute(String Q)
    {
        var CMD = new SqlCommand(Q, Sqlconnection_credit_index);
        Sqlconnection_credit_index.Open();
        CMD.ExecuteNonQuery();
        Sqlconnection_credit_index.Close();
        return true;
    }
    #region zero ref
    public bool Check_Query(String sql_query)
    {
        SqlCommand com = new SqlCommand(sql_query, Sqlconnection_credit_index);
        Sqlconnection_credit_index.Open();
        com.ExecuteNonQuery();
        Sqlconnection_credit_index.Close();
        return true;
    }

    public bool Check_Query(SqlCommand command)
    {
        command.Connection = Sqlconnection_credit_index;
        Sqlconnection_credit_index.Open();
        command.ExecuteScalar();
        Sqlconnection_credit_index.Close();
        return true;
    }


    public String execute_scalar(SqlCommand command)
    {
        command.Connection = Sqlconnection_credit_index;
        Sqlconnection_credit_index.Open();
        String output = command.ExecuteScalar().ToString();
        Sqlconnection_credit_index.Close();
        return output;
    }
    public DataTable Select(SqlCommand command)
    {
        Sqlconnection_credit_index.Close();
        command.Connection = Sqlconnection_credit_index;
        Sqlconnection_credit_index.Open();
        IDataReader dr = command.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        dr.Close();
        Sqlconnection_credit_index.Close();
        return dt;
    }
    #endregion
    public String execute_scalar(String sql_query)
    {
        var CMD = new SqlCommand(sql_query, Sqlconnection_credit_index);
        Sqlconnection_credit_index.Open();
        var output = CMD.ExecuteScalar().ToString();
        Sqlconnection_credit_index.Close();
        return output;
    }
    public DataTable Select(String SQL_Query)
    {
        var DA = new SqlDataAdapter(SQL_Query, Sqlconnection_credit_index);
        var DT = new DataTable();
        DA.Fill(DT);
        return DT;
    }

    public Boolean Transaction(String sqlcommand)
    {
        SqlTransaction transaction = null;
        var command = new SqlCommand();
        command.Connection = Sqlconnection_credit_index;
        Sqlconnection_credit_index.Open();
        transaction = Sqlconnection_credit_index.BeginTransaction();
        command.Transaction = transaction;
        command.CommandText = sqlcommand;
        command.ExecuteNonQuery();
        transaction.Commit();
        Sqlconnection_credit_index.Close();

        return true;
    }
}