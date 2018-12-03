using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Web.Filters
{
    public class VerifyGuestBookExistsAttribute : TypeFilterAttribute
    {
        public VerifyGuestBookExistsAttribute() : base(typeof(VerifyGuestBookExistsFilter))
        {
        }
        public class VerifyGuestBookExistsFilter : IAsyncActionFilter
        {
            private readonly IRepository repository;

            public VerifyGuestBookExistsFilter(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id") && context.ActionArguments["id"] is int id)
                {
                    if (repository.GetById<GuestBook>(id) == null)
                    {
                        context.Result = new NotFoundObjectResult(id);
                        return;
                    }
                }
                await next();
            }
        }
    }
}
