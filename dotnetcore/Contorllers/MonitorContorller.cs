/* Show map of route.
 * Request {host}/monitor/routes
 * ref: https://github.com/aspnet/Mvc/issues/6330#issuecomment-304368572
 */
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LoginServer.Controllers
{
    [Route("monitor")]
    public class MonitorController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;

        public MonitorController(IActionDescriptorCollectionProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("routes")]
        public IActionResult Get()
        {
            var routes = _provider.ActionDescriptors.Items.Select(x => new
            {
                Invocation = x.DisplayName,
                Action = x.RouteValues["Action"],
                Controller = x.RouteValues["Controller"],
                Name = x.AttributeRouteInfo.Name,
                Template = x.AttributeRouteInfo.Template,
                ActionDescriptor = x.ActionConstraints
            }).ToList();

            return Ok(routes);
        }
    }
}
