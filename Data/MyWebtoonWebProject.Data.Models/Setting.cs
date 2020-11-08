namespace MyWebtoonWebProject.Data.Models
{
    using MyWebtoonWebProject.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
