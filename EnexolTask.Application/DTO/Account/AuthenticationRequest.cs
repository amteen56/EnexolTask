using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.DTO.Account
{
    public class AuthenticationRequest
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
