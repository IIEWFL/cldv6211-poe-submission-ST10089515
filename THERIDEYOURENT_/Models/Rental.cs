using System;
using System.Collections.Generic;

namespace THERIDEYOURENT_.Models;

public partial class Rental
{
    public int RentalId { get; set; }

    public int? CarNo { get; set; }

    public int? InspectorNo { get; set; }

    public int? DriverId { get; set; }

    public int? RentalFee { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Car? CarNoNavigation { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Inspector? InspectorNoNavigation { get; set; }

   
}
