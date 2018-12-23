using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class ErrorLogModel : BaseLogModel
    {
        public ErrorLogModel() { }

        public ErrorLogModel(string objectId,
            DateTime dateTime,
            string message,
            string className,
            string stackTrace)
            : base(objectId, dateTime, message, className)
        {
            StackTrace = stackTrace;
        }

        [Display(Name = "Stos odwołań")]
        public string StackTrace { get; set; }
    }
}
