using System;
using System.Reflection;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceHost
{
   
    public class SecurityPageFilter:IPageFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
            return;
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var handlerPermission =
                context.HandlerMethod.MethodInfo.GetCustomAttribute(typeof(NeedPermissionAttribute)) as NeedPermissionAttribute;
            if(handlerPermission is null)
                return;

            if(!_authHelper.GetPermissionsUser().Contains(handlerPermission.Permission))
                context.HttpContext.Response.Redirect("/Account");

            
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            return;
        }
    }
}
