using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using TD.Common;
using static SwaggerHelper.CustomApiVersion;
namespace Api
{
    //关闭swagger的方法：删除生成的xml文件
    //关闭log4net的方法：删除log4net.config文件
    public class Startup
    {
        public string ApiName = "TD";//api名称
        public static ILoggerRepository repository { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = LogManager.CreateRepository("Api"); //项目名称
                                                             //指定配置文件
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().AddWebApiConventions().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #region token
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            JwtSettings setting = new JwtSettings();
            Configuration.Bind("JwtSettings", setting);
            JwtHelper.Settings = setting;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(config =>
            {
                config.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var t = context.Request.Query["access_token"];
                        if (!string.IsNullOrWhiteSpace(t))
                        {//通过url或cookie获取token
                            context.Token = t;

                        }
                        else//通过Authorization头获取token
                        {
                            if (context.Request.Headers.ContainsKey("Authorization"))
                            {
                                var avalue = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"][0]);
                                context.Token = avalue.Parameter;
                            }
                        }

                        return Task.CompletedTask;
                    }


                };
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = setting.Audience,//Audience
                    ValidIssuer = setting.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey))//拿到SecurityKey

                };

            });
            #endregion
            #region AutoMap
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
            #region swagger
            services.AddScoped<SwaggerGenerator>();//GetSwagger获取swagger.json的核心代码在这里面，这里我们用ioc容器存储对象，后面直接调里面的获取json的方法。
            services.AddSwaggerGen(c =>
            {
                //版本控制： 遍历出全部的版本，做文档信息展示
                typeof(SwaggerHelper.CustomApiVersion.ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        // {ApiName} 定义成全局变量，方便修改
                        Version = version,
                        Title = $"{ApiName} 接口文档",
                        Description = $"{ApiName} HTTP API " + version,
                        TermsOfService = null,
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "", Email = "", Url = null }
                    });
                });
                //固定版本
                //c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                //{
                //    Version = "v1",
                //    Title = "2.0接口" //typeof(Startup).GetTypeInfo().Assembly.GetName().Name,
                //});

                // Set the comments path for the Swagger JSON and UI.
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Api.XML");
                var xmlModelPath = Path.Combine(AppContext.BaseDirectory, "Dtos.xml");//Dtos类库
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
                if (File.Exists(xmlModelPath))
                {
                    c.IncludeXmlComments(xmlModelPath);
                }

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//Jwt default param name
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,//Jwt store address
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey//Security scheme type
                });
                //Add authentication type
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, Array.Empty<string>() 
                    }
                });
                
                #region Swagger参数自定义
                //c.OperationFilter<SwaggerUploadFileFilter>();//文件上传参数
                #endregion

                #region Swagger文档过滤
                //c.DocumentFilter<RemoveBogusDefinitionsDocumentFilter>();//过滤model
                #endregion
            });
            services.AddMvcCore().AddApiExplorer();

            #endregion
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseDefaultFiles(options);


            var provider = new FileExtensionContentTypeProvider();

            provider.Mappings[".mtl"] = "application/octet-stream";
            provider.Mappings[".obj"] = "application/octet-stream";


            var cachePeriod = env.IsDevelopment() ? "0" : "30";
            app.UseStaticFiles(new StaticFileOptions
            {

                OnPrepareResponse = ctx =>
                {

                    ctx.Context.Response.Headers.Add("Cache-Control", $"public, max-age={cachePeriod}");
                },
                ContentTypeProvider = provider
            });
            #region swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 

            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {

                c.ShowExtensions();

                //固定版本
                //c.SwaggerEndpoint("v1/swagger.json", "Zeiot.PlatformApi");

                //版本控制-根据版本名称倒序 遍历展示
                typeof(ApiVersions).GetEnumNames().OrderBy(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                // Other
                //c.DocumentTitle = "TUDOU";
                //css注入
                //c.InjectStylesheet("/swagger-common.css");//自定义样式
                //c.InjectStylesheet("/buzyload/app.min.css");//等待load遮罩层样式
                //                                            //js注入
                //c.InjectJavascript("/jquery/jquery.js");//jquery 插件
                //c.InjectJavascript("/buzyload/app.min.js");//loading 遮罩层js
                //c.InjectJavascript("/swagger-lang.js");//我们自定义的js
            });
            #endregion


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
