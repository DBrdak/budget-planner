using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AccountNameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public AccountNameDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}