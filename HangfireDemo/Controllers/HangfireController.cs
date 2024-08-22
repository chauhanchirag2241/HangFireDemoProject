using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangfireDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        [HttpPost]
        [Route("BackgroundJob")]
        public ActionResult CreateBackgroundJob()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Background Job Triggered."));
            return Ok();
        }

        [HttpPost]
        [Route("CreateScheduleJob")]
        public ActionResult CreateSheduleJob()
        {
            var sheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var datetimeOffset = new DateTimeOffset(sheduleDateTime);
            BackgroundJob.Schedule(() => Console.WriteLine("Scheduled Job Triggered"), datetimeOffset);
            return Ok();
        }

        [HttpPost]
        [Route("CreateContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var sheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var datetimeOffset = new DateTimeOffset(sheduleDateTime);

            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Scheduled Job 2 Triggered"), datetimeOffset);

            var job2Id = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation Job 1 Triggered"));
            var job3Id = BackgroundJob.ContinueJobWith(job2Id, () => Console.WriteLine("Continuation Job 2 Triggered"));
            var job4Id = BackgroundJob.ContinueJobWith(job3Id, () => Console.WriteLine("Continuation Job 3 Triggered"));
            return Ok();
        }

        [HttpPost]
        [Route("CreateRecurringJob")]
        public ActionResult CreateRecurringJob()
        {
            RecurringJob.AddOrUpdate("RecurringJob1", () => Console.WriteLine("Recurring Job Triggered"), Cron.MinuteInterval(1));
            return Ok();
        }
    }
}
