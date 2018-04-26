using BulletRay.Web.Models.Common;

namespace BulletRay.Web.Models.ArticleCategory
{
    public class ArticleCategoryQuery: DataTableParametersModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
