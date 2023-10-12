using System.Threading;

namespace JobCloud.BE.Shared.Enums
{
    public enum Technology
    {
        DotNet,
        Java,
        Cpp,
        C,
        JavaScript,
        Php,
        Html,
        Css,
        Python,
        Ruby,
        Go,
        Scala,
        Mobile,
        Game,
        Admin,
        DevOps,
        UxUi,
        Pm,
        Security,
        Undefined
    }

    public static class TechnologyParser
    {
        public static string Parse(Technology technology)
        {
            return technology.ToString();
        }

        public static Technology Parse(string technologyString)
        {
            Technology technology;
            var result = Enum.TryParse(technologyString, true, out technology);

            if (result == false)
            {
                return Technology.Undefined;
            }
            return technology;
        }
    }
}
