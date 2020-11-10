namespace MyWebtoonWebProject.Common.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class IsValidImageAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is IFormFile file)
            {
                if (!(file.FileName.EndsWith(".png") || file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".jpeg")))
                {
                    return false;
                }

                if (file.Length > 10 * 1024 * 1024)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
