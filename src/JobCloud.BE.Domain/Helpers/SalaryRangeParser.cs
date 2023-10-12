using JobCloud.BE.Domain.Models;

namespace JobCloud.BE.Domain.Helpers
{
    public static class SalaryRangeParser
    {
        public static SalaryRange Parse(string value)
        {
            if (!value.Contains("-"))
            {
                throw new ArgumentException("Invalid input format. Expected format: 'number-number'.");
            }

            var parts = value.Split('-');

            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid input format. Expected format: 'number-number'.");
            }

            if (!decimal.TryParse(parts[0], out decimal value1))
            {
                throw new ArgumentException("Invalid value for number 1.");
            }
            if (!decimal.TryParse(parts[1], out decimal value2))
            {
                throw new ArgumentException("Invalid value for number 2.");
            }
            return new SalaryRange(value1, value2);
        }

        public static string Parse(SalaryRange value)
        {
            return $"{value.MinValue}-{value.MaxValue}";
        }
    }
}
