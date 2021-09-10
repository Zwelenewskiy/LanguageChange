using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LanguageChange.Models
{
    public class TranslateFileLoader: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine(DateTime.Now);

            //await new Task(() => 
            //{
                GlobalHelper.ReadTranslationFiles();//Возврат ошибки??
            //});
        }
    }
}