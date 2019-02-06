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
            string source = null;
            string output = null;
            int year = 0;
            bool yearIs = false;
            if (args.Length > 0)
            {
                
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i].ToLower();
                    switch (arg)
                    {
                        case "-sdir":
                            if (args.Length > i+1)
                            {
                                source = args[i + 1];

                            }
                            continue;
                        case "-o":
                            if (args.Length > i+1)
                            {
                                output = args[i + 1];

                            }
                            continue;

                        case "-y":
                            if (args.Length > i+1)
                            {
                                yearIs = Int32.TryParse(args[i + 1], out year);

                            }
                            continue;
                    }
                }
                if (source != null && output != null)
                {
                    if (yearIs)
                    {
                        Logica mainLogica = new Logica(source, output, year);
                        mainLogica.buildTXT();
                    }
                    else
                    {
                        Logica mainLogica = new Logica(source, output);
                        mainLogica.buildTXT();
                    }

                }
                else
                {
                    if (source == null)
                    {
                        Console.WriteLine("Не задан путь к DBF");
                    }
                    if (output == null)
                    {
                        Console.WriteLine("Не задан путь вывода");
                    }
                }

            }
        }
    }
}
