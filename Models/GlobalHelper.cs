using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace LanguageChange.Models
{    
    public static class GlobalHelper
    {
        private static string PATH_TO_LANG_FILES = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"content\Translation\");

        /// <summary>
        /// Период обновления файлов с переводами (секунды)
        /// </summary>
        public const int UPDATE_READ_TIME = 30;

        /// <summary>
        /// Текущий язык пользователя
        /// </summary>
        public static string lang = "en";//переделать в enum

        /// <summary>
        /// Хранит переводы текста для страниц:
        /// controller -> text -> lang
        /// </summary>
        private static SortedList<string, SortedList<string, SortedList<string, string>>> text_to_lang { get; set; }

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

        /// <summary>
        /// Считывает файлы с переводами текстов для страниц
        /// </summary>
        /// <returns>Произошедшая ошибка</returns>
        public static Exception ReadTranslationFiles()
        {
            try
            {
                //text_to_lang = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
                text_to_lang = new SortedList<string, SortedList<string, SortedList<string, string>>>();

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PATH_TO_LANG_FILES);
                string[] filles = Directory.GetFiles(path);

                using (var excelPack = new ExcelPackage())
                {
                    //проходимся по файлам
                    foreach (var f in filles)
                    {
                        using (var stream = File.OpenRead(f))
                        {
                            excelPack.Load(stream);
                        }

                        var ws = excelPack.Workbook.Worksheets[1];

                        //считаем кол-во языков
                        int lang_count = 0;
                        foreach (var firstRowCell in ws.Cells[1, 2, 1, ws.Dimension.End.Column])
                        {
                            if (!string.IsNullOrEmpty(firstRowCell.Text))
                            {
                                lang_count++;
                            }
                        }

                        string controller_name = Path.GetFileNameWithoutExtension(Path.GetFileName(f));
                        text_to_lang[controller_name] = new SortedList<string, SortedList<string, string>>();

                        //проходимся по строкам
                        for (int row_num = 2; row_num <= ws.Dimension.End.Row; row_num++)
                        {
                            string word = ws.Cells[row_num, 1].Text;

                            if (!string.IsNullOrEmpty(word))
                            {
                                text_to_lang[controller_name][word] = new SortedList<string, string>();

                                //для текущего слова добавляем перевод на текущий язык
                                for (int col_num = 2; col_num <= lang_count + 1; col_num++)
                                {
                                    string val = ws.Cells[row_num, col_num].Text;
                                    string lang = ws.Cells[1, col_num].Text;

                                    text_to_lang[controller_name][word][lang] = val;
                                }
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}