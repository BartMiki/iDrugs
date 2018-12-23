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
    public class DatabaseExceptionHandler<T> : IDatabaseExceptionHandler<T>
    {
        private readonly DatabaseExceptionHandler _handler;

        public DatabaseExceptionHandler(ILogger<T> logger)
        {
            _handler = new DatabaseExceptionHandler(logger);
        }

        public Result<TOut> Try<TOut>(Func<TOut> function) => _handler.Try(function);

        public Result<TOut> Try<TIn, TOut>(Func<TIn, TOut> function, TIn input) => _handler.Try(function, input);

        public Result Try<TIn>(Action<TIn> action, TIn input) => _handler.Try(action, input);

        public Result Try(Action action) => _handler.Try(action);
    }

    public class DatabaseExceptionHandler : IDatabaseExceptionHandler
    {
        private readonly ILogger _logger;

        public DatabaseExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Result<TOut> Try<TOut>(Func<TOut> function)
        {
            try
            {
                var result = function.Invoke();
                return result;
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                //var msg = sqlEx.Message;
                var msg = sqlEx.Errors[0].Message;
                _logger.LogError(msg, ex);
                return Result<TOut>.Failure(msg);
            }
            catch (Exception ex) when (ex.InnerException != null && ex.InnerException.InnerException is SqlException sqlEx)
            {
                var msg = sqlEx.Errors[0].Message;
                _logger.LogError(msg, ex);
                return Result<TOut>.Failure(msg);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                _logger.LogError(msg, ex);
                return Result<TOut>.Failure(msg);
            }
        }

        public Result<TOut> Try<TIn, TOut>(Func<TIn, TOut> function, TIn input)
        {
            try
            {
                var result = function.Invoke(input);
                return result;
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                //var msg = sqlEx.Message;
                var msg = sqlEx.Errors[0].Message;
                _logger.LogError(msg, ex);
                return Result<TOut>.Failure(msg);
            }
            catch (Exception ex) when (ex.InnerException != null && ex.InnerException.InnerException is SqlException sqlEx)
            {
                var msg = sqlEx.Errors[0].Message;
                _logger.LogError(msg, ex);
                return Result<TOut>.Failure(msg);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                _logger.LogError(msg, ex);
                return Result<TOut>.Failure(msg);
            }
        }

        public Result Try<TIn>(Action<TIn> action, TIn input)
        {
            try
            {
                action.Invoke(input);
                return Result.Success();
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                //var msg = sqlEx.Message;
                var msg = sqlEx.Errors[0].Message;
                _logger.LogError(msg, ex);
                return Result.Failure(msg);
            }
            catch (Exception ex) when (ex.InnerException != null && ex.InnerException.InnerException is SqlException sqlEx)
            {
                var msg = sqlEx.Errors[0].Message;
                _logger.LogError(msg, ex);
                return Result.Failure(msg);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                _logger.LogError(msg, ex);
                return Result.Failure(msg);
            }
        }

        public Result Try(Action action)
        {
            try
            {
                action.Invoke();
                return Result.Success();
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                //var msg = sqlEx.Message;
                var msg = sqlEx.Errors[0].Message;
                _logger.LogError(msg, ex);
                return Result.Failure(msg);
            }
            catch (Exception ex) when (ex.InnerException != null && ex.InnerException.InnerException is SqlException sqlEx)
            {
                var msg = sqlEx.Errors[0].Message;
                _logger.LogError(msg, ex);
                return Result.Failure(msg);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                _logger.LogError(msg, ex);
                return Result.Failure(msg);
            }
        }
    }
}
