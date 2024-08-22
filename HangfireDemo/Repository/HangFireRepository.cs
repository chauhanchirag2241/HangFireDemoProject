using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HangfireDemo.Repository
{
    public class HangFireRepository
    {
        public void createRecurringJob() {
            Console.WriteLine("Recurring Job Executed.");
        }
    }
}
