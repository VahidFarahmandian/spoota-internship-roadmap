﻿using System.ComponentModel.DataAnnotations;

namespace FirstWeb.API.Model.DTO
{
    public class AddProductRequestDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "Name has to be a minimum of 5 characters")]
        [MaxLength(15,ErrorMessage = "Name has to be a maximum of 15 characters")]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Name has to be a minimum of 5 characters")]
        [MaxLength(10, ErrorMessage = "Name has to be a maximum of 10 characters")]
        public string Category { get; set; } = String.Empty;

        [Required]
        public decimal Price { get; set; }
    }
}
