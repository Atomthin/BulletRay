using Abp.Application.Services.Dto;

namespace BulletRay.ArticleCategorys.Dto
{
    public class ArticleCategoryIdDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsSelected { get; set; }
    }
}
