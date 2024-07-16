﻿using ApiGastos.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Dtos.Responses
{
    public class SpentResponse
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public SpentMode SpentMode { get; set; }
    }
}