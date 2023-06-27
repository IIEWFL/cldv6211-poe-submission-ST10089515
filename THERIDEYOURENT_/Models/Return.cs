using System;
using System.Collections.Generic;

namespace THERIDEYOURENT_.Models
{
    public partial class Return
    {
        public int ReturnId { get; set; }
        public int? CarNo { get; set; }
        public int? InspectorNo { get; set; }
        public int? DriverId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? ElapsedDate { get; set; }
        public int? Fine { get; set; }
        public virtual Car? CarNoNavigation { get; set; }
        public virtual Driver? Driver { get; set; }
        public virtual Inspector? InspectorNoNavigation { get; set; }

       
    }
}
