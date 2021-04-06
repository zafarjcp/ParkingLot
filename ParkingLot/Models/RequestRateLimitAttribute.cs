using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ParkingLot.Api.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestRateLimitAttribute : ActionFilterAttribute
    {
        private static Dictionary<string, User> rateInfo = new Dictionary<string, User>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ipAddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
            User user = null;
            var memoryKey = ipAddress.ToString();

            if (!rateInfo.TryGetValue(memoryKey, out user))
            {
                rateInfo.Add(memoryKey, new User { HitCount = 1, MaxHitsAllowed = 10, LastHitTime = DateTime.Now, MaxTimeLimit = DateTime.Now.AddSeconds(10) });
            }
            else
            {
                if (user != null)
                {
                    if (user.HitCount < 10)
                    {
                        user.HitCount++;
                        user.LastHitTime = DateTime.Now;
                    }
                    else if (user.HitCount >= 10 && user.MaxTimeLimit > user.LastHitTime && user.MaxTimeLimit.Subtract(user.LastHitTime) < TimeSpan.FromSeconds(10))
                    {
                        user.LastHitTime = DateTime.Now;
                        context.Result = new ContentResult
                        {
                            Content = $"Requests are limited to 10, every 10 seconds from a single IP({ipAddress})",
                        };
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    }
                    else if (user.HitCount >= 10 && user.MaxTimeLimit < user.LastHitTime)
                    {
                        user.HitCount = 1;
                        user.LastHitTime = DateTime.Now;
                        user.MaxTimeLimit = DateTime.Now.AddSeconds(10);
                    }
                }
            }
        }
    }

    public class User
    {
        public int HitCount { get; set; }
        public int MaxHitsAllowed { get; set; }
        public DateTime LastHitTime { get; set; }
        public DateTime MaxTimeLimit { get; set; }
    }

}
