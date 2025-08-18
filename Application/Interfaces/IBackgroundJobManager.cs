using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBackgroundJobManager
    {
        void Enqueue<T>(Expression<Action<T>> methodCall);
        string Schedule<T>(Expression<Action<T>> methodCall, DateTime delay);
    }
}
