# 🌟 Resource Filter in ASP.NET Core

Resource Filters are used to **run code before and after an action method executes** in ASP.NET Core.  
They help in **caching, logging, and controlling request flow**.

---

## 🛠 Why We Use Resource Filters
- **💾 Caching:** Avoid hitting the database repeatedly by storing responses.  / below code for caching 
- **📝 Logging:** Track requests and responses easily.  
- **⛔ Short-circuiting requests:** Stop processing if certain conditions are not met.  

---

## ⚙️ How It Works
Resource Filters have **two main methods**:  
1. **`OnResourceExecuting`** → Runs **before the action method**.  
2. **`OnResourceExecuted`** → Runs **after the action method**.  

> This allows developers to **pre-process requests** or **post-process responses** efficiently.  

---

## 💻 Example Code
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CacheFilter : Attribute, IResourceFilter
{
    private static Dictionary<string, object> cache = new Dictionary<string, object>();

    // Runs before action method
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        var path = context.HttpContext.Request.Path.ToString();
        if (cache.ContainsKey(path)) 
        {
            // Return cached result and skip action
            context.Result = new OkObjectResult(cache[path]);
        }
    }

    // Runs after action method
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        var path = context.HttpContext.Request.Path.ToString();
        if (!cache.ContainsKey(path) && context.Result is OkObjectResult okResult)
        {
            // Save action result to cache
            cache[path] = okResult.Value;
        }
    }
}


Before action: Check if result is cached → return cached result.

After action: Save the result in cache for future requests.

Technologies Used:

ASP.NET Core / Web API Core
C#
Filters: IResourceFilter / IAsyncResourceFilter
