using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCApi.Extensions
{
    /// <summary>
    /// 自定义路由 /api/{version=v1}/[controler]/[action]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 自定义路由构造函数
        /// </summary>
        /// <param name="actionName"></param>
        public CustomRouteAttribute(string actionName = "[action]") : base("/api/{X-Version}/[controller]/" + actionName)
        {
        }
        /// <summary>
        /// 自定义路由构造函数
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="version"></param>
        public CustomRouteAttribute(ApiVersions version, string actionName = "[action]") : base($"/api/{version.ToString()}/[controller]/{actionName}")
        {
            GroupName = version.ToString();
        }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
    }

    /// <summary>
    /// Api接口版本 每次新版本增加一个
    /// </summary>
    public enum ApiVersions
    {
        /// <summary>
        /// v1
        /// </summary>
        v1 = 1,

        /// <summary>
        /// v2
        /// </summary>
        v2 = 2,
    }
}