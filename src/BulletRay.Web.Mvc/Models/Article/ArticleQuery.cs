using BulletRay.Web.Models.Common;

namespace BulletRay.Web.Models.Article
{
    public class ArticleQuery : DataTableParametersModel
    {
        public string UserName { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
    }
}
