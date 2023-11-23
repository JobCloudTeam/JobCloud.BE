﻿namespace JobCloud.BE.JustJoinIt.Application.Models
{
    public class Offer
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string SalaryUOP { get; set; }
        public string SalaryB2B { get; set; }
        public string BaseTechnology { get; set; }
        public List<string> TechStack { get; set; }
    }
}