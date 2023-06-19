using System;
using UniqueTodoApplication.Enum;

namespace UniqueTodoApplication.Models
{
    public class NotificationRequest
    { 
       
       
       public string Name{get;set;}

       public DateTime OriginalTime{get;set;}

       public DateTime TimeInterval{get;set;}

       public Priority Priority{get;set;}

       public string Description{get;set;}

       public string Subject { get; set; }

       public string ToEmail { get; set; }
    }
}