using Quartz;
using Quartz.Impl;

namespace LanguageChange.Models
{
    public class TranslateFileLoadScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<TranslateFileLoader>().Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("trigger1", "group1")     
                .StartNow()                            
                .WithSimpleSchedule(x => x            
                    .WithIntervalInSeconds(GlobalHelper.UPDATE_READ_TIME)         
                    .RepeatForever())                   
                .Build();                               

            await scheduler.ScheduleJob(job, trigger);        
        }
    }
}