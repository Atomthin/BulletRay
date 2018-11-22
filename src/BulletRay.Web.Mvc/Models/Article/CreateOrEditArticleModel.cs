namespace BulletRay.Web.Models.Article
{
    public class CreateOrEditArticleModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDesc { get; set; }
        public string CoverImgUrl { get; set; }
        public string TagStr { get; set; }
        public long? TagNum { get; set; }
        public bool IsTop { get; set; }
        public int CategoryId { get; set; }
    }
}
