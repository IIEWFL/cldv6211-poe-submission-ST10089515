using System;
using System.Collections.Generic;

namespace THERIDEYOURENT_.Models;

public partial class Login
{
    public int LoginId { get; set; }

    public int? Username { get; set; }

    public string? Password { get; set; }

    public virtual Inspector? UsernameNavigation { get; set; }
}
