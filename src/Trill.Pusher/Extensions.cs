using System.Linq;
using System.Text;
using Convey.MessageBrokers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Trill.Pusher
{
    internal static class Extensions
    {
        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
        {
            if (accessor.HttpContext is null)
            {
                return null;
            }

            if (!accessor.HttpContext.Request.Headers.TryGetValue("Correlation-Context", out var json))
            {
                return null;
            }

            var value = json.FirstOrDefault();

            return string.IsNullOrWhiteSpace(value) ? null : JsonSerializer.Deserialize<CorrelationContext>(value);
        }
        
        internal static string GetSpanContext(this IMessageProperties messageProperties, string header)
        {
            if (messageProperties is null)
            {
                return string.Empty;
            }

            if (messageProperties.Headers.TryGetValue(header, out var span) && span is byte[] spanBytes)
            {
                return Encoding.UTF8.GetString(spanBytes);
            }

            return string.Empty;
        }
    }
}