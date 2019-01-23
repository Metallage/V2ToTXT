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
            Logica mainLogica = new Logica(@"C:\temp\v2\", @"C:\temp\v2txt\");
            mainLogica.buildTXT();
        }
    }
}
