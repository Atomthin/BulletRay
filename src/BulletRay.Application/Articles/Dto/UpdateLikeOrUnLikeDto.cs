using BulletRay.Utility;

namespace BulletRay.Articles.Dto
{
    public class UpdateLikeOrUnLikeDto
    {
        public long ArticleId { get; set; }
        public EnumHelper.LikeType LikeType { get; set; }
    }
}
