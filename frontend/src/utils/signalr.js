import * as signalR from '@microsoft/signalr'

const BASE_URL = import.meta.env.VITE_SIGNALR_URL || 'http://localhost:5000'

// 重连间隔策略：0s, 2s, 5s, 10s, 30s
const retryPolicy = {
  nextRetryDelayInMilliseconds(retryContext) {
    const delays = [0, 2000, 5000, 10000, 30000]
    return delays[retryContext.previousRetryCount] ?? 30000
  }
}

const connections = new Map()

/**
 * 连接到指定的SignalR Hub
 */
export const connectSignalR = async (hubName) => {
  if (connections.has(hubName)) {
    const conn = connections.get(hubName)
    if (conn.state !== signalR.HubConnectionState.Disconnected) {
      return conn
    }
  }

  const token = uni.getStorageSync('access_token')
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${BASE_URL}/hubs/${hubName}`, {
      accessTokenFactory: () => token
    })
    .withAutomaticReconnect(retryPolicy)
    .configureLogging(signalR.LogLevel.Information)
    .build()

  connection.onreconnecting((error) => {
    console.log(`SignalR reconnecting to ${hubName}:`, error)
  })

  connection.onreconnected((connectionId) => {
    console.log(`SignalR reconnected to ${hubName}:`, connectionId)
  })

  connection.onclose((error) => {
    console.log(`SignalR connection closed for ${hubName}:`, error)
    connections.delete(hubName)
  })

  try {
    await connection.start()
    console.log(`SignalR connected to ${hubName}`)
    connections.set(hubName, connection)
    return connection
  } catch (err) {
    console.error(`SignalR connection failed for ${hubName}:`, err)
    throw err
  }
}

/**
 * 断开指定Hub的连接
 */
export const disconnectSignalR = async (hubName) => {
  const conn = connections.get(hubName)
  if (conn) {
    await conn.stop()
    connections.delete(hubName)
  }
}
