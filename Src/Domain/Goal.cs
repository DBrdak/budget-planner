﻿namespace Domain
{
    public class Goal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }

        public double CurrentAmount { get; set; }

        public double RequiredAmount { get; set; }

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }

        public IEnumerable<Saving> Savings { get; set; } = new List<Saving>();
    }
}