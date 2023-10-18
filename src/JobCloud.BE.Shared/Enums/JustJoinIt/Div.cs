namespace JobCloud.BE.Shared.Enums.JustJoinIt
{
    public enum Div
    {
        Core,
        Experience,
        OperatingMode,
        TechStack,
        Salary,
        Company
    }

    public static class DivParser
    {
        public static string Parse(Div div)
        {
            return div.ToString();
        }

        public static Div Parse(string divString)
        {
            Div div;
            var result = Enum.TryParse(divString, true, out div);

            if (result == false)
            {
                throw new InvalidCastException(divString);
            }
            return div;
        }
    }
}
