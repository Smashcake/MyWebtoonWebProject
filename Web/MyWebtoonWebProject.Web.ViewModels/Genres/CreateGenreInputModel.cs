namespace MyWebtoonWebProject.Web.ViewModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGenreInputModel
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "Desired genre name")]
        public string Name { get; set; }
    }
}
