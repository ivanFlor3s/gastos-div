﻿using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Dtos
{
    public class UserCredentialsDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
