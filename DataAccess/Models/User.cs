﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class User
{
    public string UserId { get; set; }

    public string Password { get; set; }

    public string UserName { get; set; }

    public int? UserRole { get; set; }
}
