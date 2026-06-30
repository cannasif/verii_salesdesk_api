using salesdesk_api.Modules.Identity.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using System.Threading.Tasks;

namespace salesdesk_api.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IUserSessionCacheService _userSessionCacheService;

        public NotificationHub(
            IMemoryCache memoryCache,
            IUserSessionCacheService userSessionCacheService)
        {
            _memoryCache = memoryCache;
            _userSessionCacheService = userSessionCacheService;
        }

        // İstemci bağlandığında çalışır
        public override async Task OnConnectedAsync()
        {
            var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var sessionClaim = Context.User?.FindFirst(ClaimTypes.Sid)?.Value
                ?? Context.User?.FindFirst("sid")?.Value
                ?? Context.User?.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti)?.Value;

            if (!long.TryParse(userIdClaim, out var userId) || !Guid.TryParse(sessionClaim, out var sessionId))
            {
                Context.Abort();
                return;
            }

            var cacheKey = _userSessionCacheService.GetCacheKey(sessionId);
            if (_memoryCache.TryGetValue<long>(cacheKey, out var cachedUserId))
            {
                if (cachedUserId != userId)
                {
                    Context.Abort();
                    return;
                }
            }
            else
            {
                var restored = await _userSessionCacheService.RestoreSessionAsync(sessionId, userId, Context.ConnectionAborted);
                if (!restored)
                {
                    Context.Abort();
                    return;
                }
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
