using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.DTO.Account
{
    public class AuthenticationResponse
    {
        public string JWToken { get; set; }
        public string Image { get; set; }
        public string ErrorMsg { get; set; }
    }
}
