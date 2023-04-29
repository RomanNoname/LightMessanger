using LightMessanger.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LightMessanger.WEB.Filters
{
    public class NonExistenChatFilter:Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var service = context.HttpContext.RequestServices.GetService(typeof(IGroupsService)) as IGroupsService;

            if (context.ActionArguments.ContainsKey("currentChat"))
            {
                var currentChat = context?.ActionArguments["currentChat"]?.ToString();
                if (await service.GetValueByСonditionAsync(u => u.Name, currentChat) is null)
                    context.Result = new RedirectToActionResult("Index", "Home", new { message = "Lesson does not exist" });
                else
                    await next();
            }
            else
                await next();
        }
    }
}
