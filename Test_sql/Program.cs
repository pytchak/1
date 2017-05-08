using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace AdoNetConsoleApp
{
    class Program
    {

      
        private static 
            void GetTableNames(DataSet dataSet)
        {
            // Print each table's TableName.
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine(table.TableName);
            }
        }

        public static List<string> GetTables(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable schema = connection.GetSchema("Tables");
                List<string> TableNames = new List<string>();
                foreach (DataRow row in schema.Rows)
                {
                    TableNames.Add(row[2].ToString());
                }
                
                
                Console.WriteLine(TableNames[0]+"   sadasdasd    "+ TableNames[1]);

                

                return TableNames;
            }
        }

        public static void CreateTable(string connectionString)
        {
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand commandSql=new SqlCommand("create table userssss (" +
            //        "id_user int(10) AUTO_INCREMENT," +
            //        "name varchar(20) NOT NULL," +
            //        "email varchar(50) NOT NULL," +
            //        "password varchar(15) NOT NULL," +
            //        "PRIMARY KEY(id_user)" +
            //        "); ",connection);
            //    /commandSql.ExecuteNonQuery();

                
                SqlConnection con = new SqlConnection(connectionString);

                string sql = "create table test3(id int identity(1,1) primary key, SecondField varchar(10))";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            //}
        }



        static void Main(string[] args)
        {
            string connectionSqlString = @"Data Source=.\SQLEXPRESS;Initial Catalog=testDB;Integrated Security=True";
            string connectionOleString = "Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source=F:\С#\Test_sql\Test_sql\444.mdb";
            CreateTable(connectionSqlString);
            using (SqlConnection connectionsql = new SqlConnection(connectionSqlString))
            {
                using (OleDbConnection connectionOle = new OleDbConnection(connectionOleString))
                {
                    connectionsql.Open();
                    connectionOle.Open();
                    OleDbCommand commandOle = new OleDbCommand("select * from Student", connectionOle);
                   // OleDbDataAdapter da = new OleDbDataAdapter("SHOW TABLES", connectionOle);
                    SqlCommand commandSql;
                    OleDbDataReader readerOle = commandOle.ExecuteReader();
                   
                    for (int i = 0; i < readerOle.FieldCount; i++)
                    {
                        string nextColumnName = readerOle.GetName(i);
                        Console.Write(nextColumnName + ";");

                    }
                    Console.WriteLine(readerOle.FieldCount);
                    //DataSet dataset = new DataSet();
                    //da.Fill(dataset);
                    //GetTableNames(dataset);

                    GetTables(connectionSqlString);




                    while (readerOle.Read()) 
                    {
                        
                        object id = readerOle.GetValue(0);
                        object name = readerOle.GetValue(1);
                        object age = readerOle.GetValue(2);
                        // string sqlExpression = String.Format("INSERT INTO Users (Name, Age) VALUES ('{0}', {1})", name, age);
                        commandSql = new SqlCommand(String.Format("INSERT INTO Students" +
                            " (Name, Age) VALUES ('{0}', {1})", name, age),connectionsql);
                        commandSql.ExecuteNonQuery();
                        Console.WriteLine("{0} \t{1} \t{2}", id, name, age);
                    }
                    commandSql = new SqlCommand(("SHOW TABLES"), connectionsql);
                     //readerOle = commandOle.ExecuteReader();
                    //Console.Write();
                }
            }
            
            Console.ReadKey();
        }
           
    }
}