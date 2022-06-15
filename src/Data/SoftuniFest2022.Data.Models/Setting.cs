namespace SoftuniFest2022.Data.Models
{
    using SoftuniFest2022.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
