using JobCloud.BE.Configuration.Db.Models;
using JobCloud.BE.Shared.Enums.JustJoinIt;

namespace JobCloud.BE.Configuration.Application.DTOs
{
    public class DivNameDto
    {
        public string Div { get; set; }
        public string Name { get; set; }
    }

    public static class DivNameParser
    {
        public static DivNameDto Parse(this DivName divName) {

            return new DivNameDto
            {
                Div = divName.Div.ToString(),
                Name = divName.Name
            };
        }

        public static DivName Parse(this DivNameDto divName)
        {
            return new DivName
            {
                Div = DivParser.Parse(divName.Div),
                Name = divName.Name
            };
        }
    }
}
