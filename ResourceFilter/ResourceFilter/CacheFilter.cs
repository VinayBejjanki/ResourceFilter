using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace ResourceFilter.ResourceFilter
{
    public class CacheFilter : Attribute, IResourceFilter
    {
        public static Dictionary<string, object> _cache = new Dictionary<string, object>();

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var path = context.HttpContext.Request.Path.ToString();

            if (_cache.ContainsKey(path))
            {
                context.Result = new OkObjectResult(_cache[path]);
                Console.WriteLine($"Cache HIT for {path}");
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var path = context.HttpContext.Request.Path.ToString();

            if (context.Result is ObjectResult result)
            {
                _cache[path] = result.Value!;
                Console.WriteLine($"Cache STORED for {path}");
            }
        }
    }
}

