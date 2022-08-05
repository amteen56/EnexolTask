using EnexolTask.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Domain
{
    public class Student : BaseDomainEntity
    {
        public string Name { get; set; }
        public string? FatherName { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public char Gender { get; set; }
        public string? PicURL { get; set; }
    }
}
