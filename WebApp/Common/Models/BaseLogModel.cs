using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class BaseLogModel
    {
        public BaseLogModel(string objectId, 
            DateTime dateTime, 
            string message, 
            string className)
        {
            ObjectId = objectId;
            DateTime = dateTime;
            Message = message;
            ClassName = className;
        }

        public BaseLogModel() { }

        [Display(Name = "Id")]
        public string ObjectId { get; set; }
        [Display(Name = "Data zdarzenia")]
        public DateTime DateTime { get; set; }
        [Display(Name = "Wiadomość")]
        public string Message { get; set; }
        [Display(Name = "Miejsce zdarzenia")]
        public string ClassName { get; set; }
    }
}
