﻿using System.ComponentModel.DataAnnotations;

namespace GroceryShoppingCartAPI.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
    }
}
