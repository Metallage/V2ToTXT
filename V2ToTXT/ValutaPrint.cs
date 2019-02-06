using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace V2ToTXT
{
    class ValutaPrint
    {
        private string outputTxt;    
        
        public ValutaPrint(string outputTxt)
        {
            this.outputTxt = outputTxt;
        }

        public void ValutaToFile(DataTable v2Table)
        {
            if ((v2Table != null)&&(v2Table.Rows.Count>0))
            {
                using (StreamWriter valWriter = new StreamWriter(outputTxt, true, Encoding.Default))
                {

                    valWriter.WriteLine(@"Курсы валют на               {0}/{1}/{2}г.", v2Table.Rows[0].Field<DateTime>("DATA").Day.ToString("D2"), v2Table.Rows[0].Field<DateTime>("DATA").Month.ToString("D2"), v2Table.Rows[0].Field<DateTime>("DATA").Year.ToString("D4"));

                    foreach(DataRow dr in v2Table.Rows)
                    {
                        string kol = dr.Field<double>("KOL").ToString("N0");
                        string buk = dr.Field<String>("BUK");
                        string okurs = dr.Field<double>("OKURS").ToString("C4");
                        string kod = dr.Field<String>("KOD");
                        valWriter.WriteLine($"{kol, 7} {buk}({kod}) = {okurs}" );
                    }

                    valWriter.WriteLine();

                }
            }
        }

        public void ApendTXT(DateTime date, DataTable valutaKurs)
        {
            if(valutaKurs.Rows.Count>0)
            {
                using (StreamWriter valWriter = new StreamWriter(outputTxt, true, Encoding.Default))
                {

                    valWriter.WriteLine(@"Курсы валют на               {0}/{1}/{2}г.", date.Day.ToString("D2"), date.Month.ToString("D2"), date.Year.ToString("D4"));

                    foreach (DataRow dr in valutaKurs.Rows)
                    {
                        string kol = dr.Field<double>("KOL").ToString("N0");
                        string buk = dr.Field<String>("BUK");
                        string okurs = dr.Field<double>("OKURS").ToString("C4");
                        string kod = dr.Field<String>("KOD");
                        valWriter.WriteLine($"{kol,7} {buk}({kod}) = {okurs}");
                    }

                    valWriter.WriteLine();

                }


            }

        }



    }
}
