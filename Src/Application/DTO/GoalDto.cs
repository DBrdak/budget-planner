﻿namespace Application.DTO
{
    public class GoalDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public double CurrentAmount { get; set; }
        public double RequiredAmount { get; set; }
    }
}