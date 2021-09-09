using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace LanguageChange.Models
{
    public static class GlobalHelper
    {
        private static string PATH_TO_LANG_FILES = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"content\Translation\");

        /// <summary>
        /// Текущий язык пользователя
        /// </summary>
        public static string lang = "en";//переделать в enum

        /// <summary>
        /// Хранит переводы текста для страниц:
        /// controller -> text -> lang
        /// </summary>
        public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> text_to_lang { get; set; }

        /// <summary>
        /// Считывает файлы с переводами текстов для страниц
        /// </summary>
        /// <returns>Произошедшая ошибка</returns>
        public static Exception ReadTranslationFiles()
        {
            try
            {
                text_to_lang = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PATH_TO_LANG_FILES);
                DirectoryInfo dir = new DirectoryInfo(path);
                string[] filles = Directory.GetFiles(path);

                foreach (var f in filles)
                {
                    Excel.Application ex = new Excel.Application();

                    Excel.Workbook workbook = ex.Workbooks.Open(f,
                      Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                      Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                      Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                      Type.Missing, Type.Missing);

                    Excel.Worksheet sheet = (Excel.Worksheet)ex.Workbooks[1].Sheets[1];

                    var range = sheet.UsedRange;

                    //Выясняем кол-во языков
                    dynamic val = null;
                    int index = 2;
                    int lang_count = 0;
                    while((val = range.Cells[1, index].Value2) != null)
                    {
                        index++;
                        lang_count++;
                    }

                    string controller_name = Path.GetFileNameWithoutExtension(Path.GetFileName(f));
                    text_to_lang[controller_name] = new Dictionary<string, Dictionary<string, string>>();
                    //проходимся по файлу
                    for (int i = 2; i <= range.Rows.Count; i++)
                    {
                        if(range.Cells[i, 1].Value2 != null)
                        {
                            string word = range.Cells[i, 1].Value2.ToString();
                            text_to_lang[controller_name][word] = new Dictionary<string, string>();

                            //к каждому слову добавляем перевод на текущем языке
                            for (int j = 2; j <= lang_count + 1; j++)
                            {
                                val = range.Cells[i, j].Value2.ToString();
                                string lang = range.Cells[1, j].Value2.ToString();

                                text_to_lang[controller_name][word][lang] = val;
                            }
                        }                        
                    }

                    workbook.Close(false, Type.Missing, Type.Missing);
                    ex.Quit();

                    Marshal.ReleaseComObject(workbook);//иначе excel останется висеть в памяти
                    workbook = null;
                }


                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        //public static MvcHtmlString GetTranslate(this HtmlHelper helper, string controller_name, string text)
        public static string GetTranslate(string controller_name, string text)
        {
            try
            {
                if (lang == "ru")
                    return text;
                else
                    return text_to_lang[controller_name][text][lang];
            }
            catch
            {
                return text;
            }
        }
    }
}