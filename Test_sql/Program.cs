using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace AdoNetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=testDB;Integrated Security=True";
            using (SqlConnection connectionsql = new SqlConnection(connectionString))
            {
                using (OleDbConnection connectionOle = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source=F:\С#\Test_sql\Test_sql\444.mdb"))
                {
                    connectionsql.Open();
                    connectionOle.Open();
                    OleDbCommand commandOle = new OleDbCommand("select * from Student", connectionOle);
                    SqlCommand commandSql;
                    OleDbDataReader da = commandOle.ExecuteReader();


                    while (da.Read()) 
                    {
                        
                        object id = da.GetValue(0);
                        object name = da.GetValue(1);
                        object age = da.GetValue(2);
                        // string sqlExpression = String.Format("INSERT INTO Users (Name, Age) VALUES ('{0}', {1})", name, age);
                        commandSql = new SqlCommand(String.Format("INSERT INTO Students" +
                            " (Name, Age) VALUES ('{0}', {1})", name, age),connectionsql);
                        commandSql.ExecuteNonQuery();
                        Console.WriteLine("{0} \t{1} \t{2}", id, name, age);
                    }
                    //Console.Write();
                }
            }
            
            Console.ReadKey();
        }
           
    }
}