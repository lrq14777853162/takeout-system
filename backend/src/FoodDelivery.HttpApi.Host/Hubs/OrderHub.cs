using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FoodDelivery.Hubs;

/// <summary>订单实时通知 Hub</summary>
[Authorize]
public class OrderHub : Hub
{
    private readonly ILogger<OrderHub> _logger;

    public OrderHub(ILogger<OrderHub> logger)
    {
        _logger = logger;
    }

    /// <summary>骑手上报实时位置</summary>
    public async Task UpdateRiderLocation(double lat, double lng)
    {
        var riderId = Context.UserIdentifier;
        _logger.LogInformation("Rider {RiderId} location updated: {Lat},{Lng}", riderId, lat, lng);

        // 广播骑手位置给关注该骑手的客户
        await Clients.Group($"rider_{riderId}").SendAsync("RiderLocationUpdated", new
        {
            RiderId = riderId,
            Lat = lat,
            Lng = lng,
            Timestamp = DateTime.UtcNow
        });
    }

    /// <summary>加入订单房间，监听该订单的状态更新</summary>
    public async Task JoinOrderGroup(string orderId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"order_{orderId}");
        _logger.LogInformation("Connection {ConnectionId} joined order group {OrderId}", Context.ConnectionId, orderId);
    }

    /// <summary>离开订单房间</summary>
    public async Task LeaveOrderGroup(string orderId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"order_{orderId}");
        _logger.LogInformation("Connection {ConnectionId} left order group {OrderId}", Context.ConnectionId, orderId);
    }

    /// <summary>发送订单状态更新通知</summary>
    public static async Task NotifyOrderStatusChanged(IHubContext<OrderHub> hubContext, string orderId, int status, string statusName)
    {
        await hubContext.Clients.Group($"order_{orderId}").SendAsync("OrderStatusChanged", new
        {
            OrderId = orderId,
            Status = status,
            StatusName = statusName,
            Timestamp = DateTime.UtcNow
        });
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}
