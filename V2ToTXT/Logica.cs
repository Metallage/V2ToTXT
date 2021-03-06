﻿using System;
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
        private int year;
        List<FileInfo> dbfFiles = new List<FileInfo>();



        public Logica(string inputPath, string outputPath)
        {
            this.inputPath = inputPath;
            this.outputPath = outputPath;
            this.year = DateTime.Now.Year;
            if(!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
        }

        public Logica(string inputPath, string outputPath, int year)
        {
            this.inputPath = inputPath;
            this.outputPath = outputPath;
            this.year = year;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
        }


        public void buildTXT()
        {
            DirectoryInfo dbfDir = new DirectoryInfo(inputPath);

            searchFiles(dbfDir); //ищем все вайлы валют и заданной директории

            //Отчищаем директорию вывода
            if (Directory.Exists(outputPath + year.ToString("D4") + "//"))
            {
                Directory.Delete(outputPath + year.ToString("D4") + "//", true);
            }

            DataTable valutaTabl = new DataTable();
            DataTable dates = new DataTable();

            //Перебираем все найденные файлы
            foreach (FileInfo dbfValuta in dbfFiles)
            {
                if((dbfValuta.Name.ToLower()=="v2.dbf")||(dbfValuta.Name.ToLower() == "valuta.dbf"))
                {
                    //Если нет директории вывода, создаём её
                    if (!Directory.Exists(outputPath + year + "//"))
                    {
                        Directory.CreateDirectory(outputPath + year + "//");
                    }


                    //Создаём объект ДБФки
                    DBFWork v2DBF = new DBFWork(dbfValuta.FullName);

                    //Выбираем по месяцам
                    for (int i =1; i<=12; i++ )
                    {
                        //Подготавливаем к созданию файл с названием месяца
                        string monthName = GetMonthName(i);
                        string outputTxtPath = String.Format($@"{outputPath}{year}/{monthName}");
                        ValutaPrint txtValuta = new ValutaPrint(outputTxtPath);

                        ////Устанавливаем границы месяца
                        //DateTime fromTime = new DateTime(year, i, 1);
                        //DateTime toTime = new DateTime(year, i, DateTime.DaysInMonth(year, i));

                        //Отчищаем всё предыдущие таблицы
                        dates.Clear();
                        valutaTabl.Clear();

                        //Выбираем все интересующие нас даты
                        dates = v2DBF.SelectDatesFromMonth(year, i);

                        foreach(DataRow dr in dates.Rows)
                        {
                            valutaTabl = v2DBF.SelectByDate(dr.Field<DateTime>(0));
                            txtValuta.ApendTXT(dr.Field<DateTime>(0), valutaTabl);
                        }

                    }  
                }
            }
        }

        /// <summary>
        /// Возвращает имя файла по номеру месяца
        /// </summary>
        /// <param name="monthNum">Номер месяца</param>
        /// <returns>Имя файла</returns>
        private string GetMonthName(int monthNum)
        {
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
                    monthFile = "99_Не_Знаю.txt"; //Ну мало ли
                    break;
            }
            return monthFile;
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
