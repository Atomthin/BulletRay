using System.ComponentModel.DataAnnotations;

namespace BulletRay.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}