﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ActivityAttendee
    {
        public string? UserId { get; set; }
        public User User { get; set; } = null!;
        public string? ActivityId { get; set; }
        public Activity Activity { get; set; }= null!;
        public bool IsHost { get; set; } = false;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
