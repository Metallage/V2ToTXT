using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace V2ToTXT
{
    class Logica
    {
        private string inputPath;
        private string outputPath;
        List<FileInfo> dbfFiles = new List<FileInfo>();

        public Logica(string inputPath, string outputPath)
        {
            this.inputPath = inputPath;
            this.outputPath = outputPath;
            if(!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
        }


        public void buildTXT()
        {
            DirectoryInfo dbfDir = new DirectoryInfo(inputPath);

            searchFiles(dbfDir);

            if (Directory.Exists(outputPath + DateTime.Now.Year.ToString("D4") + "//"))
            {
                Directory.Delete(outputPath + DateTime.Now.Year.ToString("D4") + "//", true);
            }


            foreach(FileInfo dbfValuta in dbfFiles)
            {
                if(dbfValuta.Name.ToLower()=="v2.dbf")
                {
                    DBFWork v2DBF = new DBFWork(dbfValuta.FullName);
                    DataTable v2DT = v2DBF.ReadDBF();
                    string yearDir = v2DT.Rows[0].Field<DateTime>("DATA").Year.ToString("D4");

                    if (yearDir == DateTime.Now.Year.ToString("D4"))
                    {
                        if(!Directory.Exists(outputPath+yearDir+"//"))
                        {
                            Directory.CreateDirectory(outputPath + yearDir + "//");
                        }

                        int monthNum = v2DT.Rows[0].Field<DateTime>("DATA").Month;
                        string monthFile;

                        switch (monthNum)
                        {
                            case 1:
                                monthFile = "01_Январь.txt";
                                break;
                            case 2:
                                monthFile = "02_Февраль.txt";
                                break;
                            case 3:
                                monthFile = "03_Март.txt";
                                break;
                            case 4:
                                monthFile = "04_Апрель.txt";
                                break;
                            case 5:
                                monthFile = "05_Май.txt";
                                break;
                            case 6:
                                monthFile = "06_Июнь.txt";
                                break;
                            case 7:
                                monthFile = "07_Июль.txt";
                                break;
                            case 8:
                                monthFile = "08_Август.txt";
                                break;
                            case 9:
                                monthFile = "09_Сентябрь.txt";
                                break;
                            case 10:
                                monthFile = "10_Октябрь.txt";
                                break;
                            case 11:
                                monthFile = "11_Ноябрь.txt";
                                break;
                            case 12:
                                monthFile = "12_Декабрь.txt";
                                break;
                            default:
                                monthFile = "99_Не_Знаю.txt";
                                break;
                        }


                        string outputFile = String.Format($@"{outputPath}{yearDir}/{monthFile}");

                        ValutaPrint txtValuta = new ValutaPrint(outputFile);
                        txtValuta.ValutaToFile(v2DT);
                    }
                }
            }


        }

        /// <summary>
        /// Рекурсивно выбирает все файлы из указанной директории
        /// </summary>
        /// <param name="targetDir">Директория поиска</param>
        private void searchFiles(DirectoryInfo targetDir)
        {
            //Вбиваем всё что нашли
            dbfFiles.AddRange(targetDir.GetFiles()); 

            //Ищем глубже
            foreach(DirectoryInfo recursiveDir in targetDir.GetDirectories())
            {
                searchFiles(recursiveDir);
            }
        }

    }
}
