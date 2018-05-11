using Abp.Application.Services.Dto;

namespace BulletRay.Tags.Dto
{
    public class GetAllTagDto : PagedResultRequestDto
    {
        public string TagKeyStr { get; set; }
    }
}
