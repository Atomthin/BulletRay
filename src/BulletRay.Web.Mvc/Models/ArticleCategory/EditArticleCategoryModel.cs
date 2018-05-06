using System.ComponentModel.DataAnnotations;

namespace BulletRay.Web.Models.ArticleCategory
{
    public class EditArticleCategoryModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public bool IsOpenShown { get; set; }
    }
}
