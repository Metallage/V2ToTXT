using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V2ToTXT
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "in//";
            string output = "valuta//";
            int year = 2018;
            bool yearIs = true;
            //if (args.Length > 0)
            //{
            //    int i = 0;
            //    while ( i < args.Length)
            //    {
            //        string arg = args[i];
            //        if (arg.ToLower() == "-s")
            //        {
            //            source = args[i + 1];
            //            i = +2;
            //        }
            //        else if (arg.ToLower() == "-o")
            //        {
            //            output = args[i + 1];
            //            i = +2;
            //        }
            //        else if (arg.ToLower() == "-y")
            //        {
            //            yearIs = Int32.TryParse(args[i + 1], out year);
            //            i = +2;
            //        }
            //        else
            //        {
            //            i++;
            //        }
            //    }

                if(yearIs)
                {
                    Logica mainLogica = new Logica(source, output, year);
                    mainLogica.buildTXT();
                }
                else
                {
                    Logica mainLogica = new Logica(source, output);
                    mainLogica.buildTXT();
                }


            //}

           
        }
    }
}
