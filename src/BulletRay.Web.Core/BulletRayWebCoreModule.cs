﻿using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using BulletRay.Authentication.JwtBearer;
using BulletRay.Configuration;
using BulletRay.EntityFrameworkCore;

#if FEATURE_SIGNALR
using Abp.Web.SignalR;
#elif FEATURE_SIGNALR_ASPNETCORE
using Abp.AspNetCore.SignalR;
#endif

namespace BulletRay
{
    [DependsOn(
         typeof(BulletRayApplicationModule),
         typeof(BulletRayEntityFrameworkModule),
         typeof(AbpAspNetCoreModule)
#if FEATURE_SIGNALR 
        ,typeof(AbpWebSignalRModule)
#elif FEATURE_SIGNALR_ASPNETCORE
        ,typeof(AbpAspNetCoreSignalRModule)
#endif
     )]
    public class BulletRayWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BulletRayWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                BulletRayConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(BulletRayApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BulletRayWebCoreModule).GetAssembly());
        }
    }
}
