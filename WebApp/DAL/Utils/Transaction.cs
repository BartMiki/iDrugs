using Common.Utils;
using static Common.Handlers.StaticDatabaseExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL.Utils
{
    public static class TransactionExtension
    {
        /// <summary>
        /// Transaction wraper to properly handle Commit and Rollback operation.
        /// </summary>
        /// <param name="action">
        /// Action that you want to perform on database, no need to defining Transaction logic.
        /// </param>
        public static Result BeginTransaction(this iDrugsEntities context, Action action, Type loggerType,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var result = Try(() => {
                using (var transaction = context.Database.BeginTransaction(isolationLevel))
                {
                    try
                    {
                        action?.Invoke();
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }, loggerType);

            return result;
        }

        public static Result<T> BeginTransaction<T>(this iDrugsEntities context, Func<T> func)
        {
            throw new NotImplementedException();
        }
    }
}
