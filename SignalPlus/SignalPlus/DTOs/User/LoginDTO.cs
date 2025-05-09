﻿namespace SignalPlus.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
