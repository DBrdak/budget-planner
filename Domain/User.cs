using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}