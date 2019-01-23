using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Data.OleDb;

namespace V2ToTXT
{
    class DBFWork
    {
        private string dbfFilePath;
        private OdbcConnection conDBF = null;


        public DBFWork(string dbfFilePath)
        {
            this.conDBF = new OdbcConnection();
            conDBF.ConnectionString = @"Driver={Microsoft Access dBase Driver (*.dbf, *.ndx, *.mdx)}; datasource=dBase Files;";
            this.dbfFilePath = dbfFilePath;


        }

        public DataTable ReadDBF()
        {
            DataTable resultTable = new DataTable();

            conDBF.Open();
            OdbcCommand dbfCommand = conDBF.CreateCommand();
            dbfCommand.CommandText = "SELECT * FROM " + dbfFilePath;
            resultTable.Load(dbfCommand.ExecuteReader());
            conDBF.Close();

            return resultTable;          
        }

    }
}
