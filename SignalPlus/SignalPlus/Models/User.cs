﻿namespace SignalPlus.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        // Navigation property - list of signals submitted by the user
        public ICollection<Signal> Signals { get; set; } = new List<Signal>();
    }

}
