using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string StudentId { get; set; } = "";
    }
}