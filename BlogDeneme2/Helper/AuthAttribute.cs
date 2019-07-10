using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BlogDeneme2.Helper
{
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public string[] RoleName { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.Session.GetString("UserRole");
            if (role == null)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
            else
            {               
                var isCheck = false;
                foreach (var item in RoleName)
                {
                    if(item == role)
                    {
                        isCheck = true;
                        break;
                    }
                }

                if(isCheck == false)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                }
            }
        }
       
    }
}
