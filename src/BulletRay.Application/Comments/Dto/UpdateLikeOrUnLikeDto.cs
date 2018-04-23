using BulletRay.Utility;

namespace BulletRay.Comments.Dto
{
    public class UpdateLikeOrUnLikeDto
    {
        public long CommentId { get; set; }
        public EnumHelper.LikeType LikeType { get; set; }
    }
}
