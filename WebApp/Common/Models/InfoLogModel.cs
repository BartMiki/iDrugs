using System;

namespace Common.Models
{
    public class InfoLogModel : BaseLogModel
    {
        public InfoLogModel(string objectId,
            DateTime dateTime,
            string message,
            string className,
            string args) : base(objectId, dateTime, message, className)
        {
            Args = args;
        }

        public InfoLogModel() { }

        public string Args { get; set; }
    }
}
