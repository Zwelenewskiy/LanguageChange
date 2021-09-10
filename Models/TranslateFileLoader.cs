using Newtonsoft.Json;
using OfficeOpenXml;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

namespace LanguageChange.Models
{
    public class TranslateFileLoader: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await new Task(() => { });
        }
    }
}