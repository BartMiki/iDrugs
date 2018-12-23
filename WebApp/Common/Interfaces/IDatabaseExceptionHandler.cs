using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IDatabaseExceptionHandler<T> : IDatabaseExceptionHandler{}

    public interface IDatabaseExceptionHandler
    {
        Result<TOut> Try<TOut>(Func<TOut> function);
        Result<TOut> Try<TIn, TOut>(Func<TIn, TOut> function, TIn input);
        Result Try<TIn>(Action<TIn> action, TIn input);
        Result Try(Action action);
    }
}
