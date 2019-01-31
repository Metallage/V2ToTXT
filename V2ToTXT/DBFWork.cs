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
        //private long[] years;
        private OdbcConnection conDBF = null;


        public DBFWork(string dbfFilePath)
        {
            this.conDBF = new OdbcConnection();
            conDBF.ConnectionString = @"Driver={Microsoft Access dBase Driver (*.dbf, *.ndx, *.mdx)}; datasource=dBase Files;";
            this.dbfFilePath = dbfFilePath;
            //years = new long[2];
            //DateTime year1 = new DateTime(DateTime.Now.Year,1,1);
            //DateTime year2 = new DateTime(DateTime.Now.Year + 1, 1, 1);
            //years[0] = (long)year1.ToOADate();
            //years[1] = (long)year2.ToOADate();
        }

        public DBFWork(string dbfFilePath, int year)
        {
            this.conDBF = new OdbcConnection();
            conDBF.ConnectionString = @"Driver={Microsoft Access dBase Driver (*.dbf, *.ndx, *.mdx)}; datasource=dBase Files;";
            this.dbfFilePath = dbfFilePath;
            //years = new long[2];
            //DateTime year1 = new DateTime(year, 1, 1);
            //DateTime year2 = new DateTime(year + 1, 1, 1);
            //years[0] = (long)year1.ToOADate();
            //years[1] = (long)year2.ToOADate();
        }


        //public DataTable ReadDBF()
        //{
        //    DataTable resultTable = new DataTable();

        //    conDBF.Open();
        //    OdbcCommand dbfCommand = conDBF.CreateCommand();
        //    dbfCommand.CommandText = $"SELECT * FROM {dbfFilePath} as V2 WHERE V2.DATA >= {years[0]} AND V2.DATA < {years[1]}; ";
        //    resultTable.Load(dbfCommand.ExecuteReader());
        //    conDBF.Close();

        //    return resultTable;          
        //}

        public DataTable ReadbyDate(DateTime dateFrom, DateTime dateTo)
        {
            DataTable resultTable = new DataTable();
            long minDate = (long)dateFrom.Date.ToOADate();
            long maxDate = (long)dateTo.Date.ToOADate();

            conDBF.Open();

            OdbcCommand dbfCommand = conDBF.CreateCommand();
            dbfCommand.CommandText = $"SELECT * FROM {dbfFilePath} as V2 WHERE V2.DATA >= {minDate} AND V2.DATA <= {maxDate}; ";
            resultTable.Load(dbfCommand.ExecuteReader());
            conDBF.Close();

            return resultTable;
        }

        public bool CheckCount(DateTime periodFrom, DateTime periodTo)
        {
            bool hasRows = false;

            long minDate = (long)periodFrom.Date.ToOADate();
            long maxDate = (long)periodTo.Date.ToOADate();

            DataTable rowsCount = new DataTable();

            conDBF.Open();

            OdbcCommand dbfCommand = conDBF.CreateCommand();
            dbfCommand.CommandText = $"SELECT COUNT(*) FROM {dbfFilePath} as V2 WHERE V2.DATA >= {minDate} AND V2.DATA <= {maxDate}; ";
            rowsCount.Load(dbfCommand.ExecuteReader());
            conDBF.Close();

            if(rowsCount.Rows[0].Field<int>(0)!=0)
            {
                hasRows = true;
            }

            return hasRows;
        }

    }
}
