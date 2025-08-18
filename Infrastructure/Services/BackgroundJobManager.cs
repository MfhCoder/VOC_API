using Application.Interfaces;
using Hangfire;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class BackgroundJobManager : IBackgroundJobManager
    {
        public void Enqueue<T>(Expression<Action<T>> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }

        public string Schedule<T>(Expression<Action<T>> methodCall, DateTime delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }
    }
}
