using BulletRay.Utility;

namespace BulletRay.CommonDto
{
    public class UpdataLikeOrUnLikeDto
    {
        public long Id { get; set; }
        public EnumHelper.LikeType LikeType { get; set; }
    }
}
