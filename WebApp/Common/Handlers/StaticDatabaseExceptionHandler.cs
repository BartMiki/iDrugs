using Common.Interfaces;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Handlers
{
    public static class StaticDatabaseExceptionHandler
    {
        public static Result<TOut> Try<TOut>(Func<TOut> function, Type loggerType)
        {
            var logger = new MongoDbLogger(loggerType);
            var handler = new DatabaseExceptionHandler(logger);

            return handler.Try(function);
        }

        public static Result<TOut> Try<TIn,TOut>(Func<TIn,TOut> function, TIn input, Type loggerType)
        {
            var logger = new MongoDbLogger(loggerType);
            var handler = new DatabaseExceptionHandler(logger);

            return handler.Try(function, input);
        }

        public static Result Try<TIn>(Action<TIn> action, TIn input, Type loggerType)
        {
            var logger = new MongoDbLogger(loggerType);
            var handler = new DatabaseExceptionHandler(logger);

            return handler.Try(action, input);
        }

        public static Result Try(Action action, Type loggerType)
        {
            var logger = new MongoDbLogger(loggerType);
            var handler = new DatabaseExceptionHandler(logger);

            return handler.Try(action);
        }
    }
}
