﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Articles.Dto
{
    [AutoMapTo(typeof(Article))]
    public class CreateArticleDto : EntityDto<long>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string ShortDesc { get; set; }
        public string CoverImgUrl { get; set; }
        public int? Tag { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
