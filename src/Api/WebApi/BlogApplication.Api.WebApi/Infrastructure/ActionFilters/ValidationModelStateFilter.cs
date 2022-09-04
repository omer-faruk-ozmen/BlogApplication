﻿using BlogApplication.Api.WebApi.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApplication.Api.WebApi.Infrastructure.ActionFilters
{
    public class ValidationModelStateFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var messages = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => !string.IsNullOrEmpty(x.ErrorMessage) ? x.ErrorMessage : x.Exception?.Message)
                    .Distinct().ToList();

                var result = new ValidationResponseModel(messages);
                context.Result = new BadRequestObjectResult(result);

                return;
            }

            await next();
        }
    }
}
