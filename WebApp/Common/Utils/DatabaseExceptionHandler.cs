using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class DatabaseExceptionHandler
    {
        public static Result<TOut> Try<TOut>(Func<TOut> function)
        {
            try
            {
                var result = function.Invoke();
                return result;
            }
            catch(Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                return Result<TOut>.Failure(sqlEx.Errors[0].Message);
            }
            catch (Exception ex) when (ex.InnerException != null && ex.InnerException.InnerException is SqlException sqlEx)
            {
                return Result<TOut>.Failure(sqlEx.Errors[0].Message);
            }
            catch (Exception ex)
            {
                return Result<TOut>.Failure(ex.Message);
            }
        }

        public static Result<TOut> Try<TIn,TOut>(Func<TIn,TOut> function, TIn input)
        {
            try
            {
                var result = function.Invoke(input);
                return result;
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                return Result<TOut>.Failure(sqlEx.Message);
            }
            catch (Exception ex)
            {
                return Result<TOut>.Failure(ex.Message);
            }
        }

        public static Result Try<TIn>(Action<TIn> action, TIn input)
        {
            try
            {
                action.Invoke(input);
                return Result.Success();
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                return Result.Failure(sqlEx.Message);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public static Result Try(Action action)
        {
            try
            {
                action.Invoke();
                return Result.Success();
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                return Result.Failure(sqlEx.Message);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
