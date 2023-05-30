using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.DTO
{
    public class GoalName
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public GoalName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}