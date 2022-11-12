using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LoginResultDTO
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; } = "";
        public string AuthorizationType { get; set; } = "";
        public string CreateDate { get; set; } = "";
        public string ExpirationDate { get; set; } = "";
        public string AccessToken { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Name { get; set; } = "";
    }
}