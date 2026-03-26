<template>
  <view class="container">
    <!-- 在线状态头部 -->
    <view class="header" :class="isOnline ? 'header-online' : 'header-offline'">
      <view class="status-area">
        <text class="status-text">{{ isOnline ? '在线接单中' : '已下线' }}</text>
        <text class="status-sub">{{ isOnline ? '新订单将自动推送' : '点击上线开始接单' }}</text>
      </view>
      <switch :checked="isOnline" @change="toggleOnline" :color="isOnline ? '#4cd964' : '#ccc'" />
    </view>

    <!-- 今日数据 -->
    <view class="today-stats">
      <view class="stat-item">
        <text class="stat-val">{{ todayStats.orderCount || 0 }}</text>
        <text class="stat-key">今日订单</text>
      </view>
      <view class="stat-divider"></view>
      <view class="stat-item">
        <text class="stat-val">¥{{ (todayStats.earnings || 0).toFixed(2) }}</text>
        <text class="stat-key">今日收入</text>
      </view>
      <view class="stat-divider"></view>
      <view class="stat-item">
        <text class="stat-val">{{ todayStats.deliveryCount || 0 }}</text>
        <text class="stat-key">配送完成</text>
      </view>
    </view>

    <!-- 当前任务 -->
    <view v-if="activeOrder" class="active-order">
      <view class="section-title"><text>📍 当前配送任务</text></view>
      <view class="active-card" @click="goDelivering">
        <view class="active-info">
          <text class="active-shop">{{ activeOrder.merchantName }}</text>
          <text class="active-addr">{{ activeOrder.deliveryAddress }}</text>
        </view>
        <view class="active-status">
          <text class="active-status-text">{{ formatStatus(activeOrder.status) }}</text>
          <text class="active-arrow">›</text>
        </view>
      </view>
    </view>

    <!-- 可接订单列表 -->
    <view class="available-orders" v-if="isOnline">
      <view class="section-title">
        <text>附近待取订单</text>
        <text class="refresh-btn" @click="loadAvailableOrders">刷新</text>
      </view>
      <view v-if="availableOrders.length === 0" class="empty-orders">
        <text class="empty-text">暂无可接订单，稍后刷新</text>
      </view>
      <view
        v-for="order in availableOrders"
        :key="order.id"
        class="available-card"
      >
        <view class="avail-header">
          <text class="avail-shop">{{ order.merchantName }}</text>
          <text class="avail-fee">+¥{{ order.deliveryFee?.toFixed(2) || '0.00' }}</text>
        </view>
        <view class="avail-route">
          <view class="route-point">
            <text class="point-dot dot-from">●</text>
            <text class="route-addr">{{ order.merchantAddress }}</text>
          </view>
          <view class="route-point">
            <text class="point-dot dot-to">●</text>
            <text class="route-addr">{{ order.deliveryAddress }}</text>
          </view>
        </view>
        <view class="avail-footer">
          <text class="avail-distance">约 {{ order.distance?.toFixed(1) || '?' }} km</text>
          <button class="btn-grab" @click="grabOrder(order.id)">接单</button>
        </view>
      </view>
    </view>

    <!-- 快捷导航 -->
    <view class="quick-nav">
      <view class="nav-item" @click="uni.navigateTo({ url: '/pages/rider/order-history/index' })">
        <text class="nav-icon">📋</text>
        <text class="nav-label">历史订单</text>
      </view>
      <view class="nav-item" @click="uni.navigateTo({ url: '/pages/rider/earnings/index' })">
        <text class="nav-icon">💰</text>
        <text class="nav-label">收入明细</text>
      </view>
      <view class="nav-item" @click="uni.navigateTo({ url: '/pages/rider/profile/index' })">
        <text class="nav-icon">👤</text>
        <text class="nav-label">个人信息</text>
      </view>
    </view>
  </view>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { riderApi } from '../../../api/rider'
import { orderApi } from '../../../api/order'
import { connectSignalR, disconnectSignalR } from '../../../utils/signalr'
import { formatOrderStatus } from '../../../utils/format'

const isOnline = ref(false)
const todayStats = ref({})
const activeOrder = ref(null)
const availableOrders = ref([])

function formatStatus(s) { return formatOrderStatus(s) }

async function toggleOnline(e) {
  const online = e.detail.value
  try {
    if (online) {
      await riderApi.goOnline()
      isOnline.value = true
      uni.showToast({ title: '已上线', icon: 'success' })
      loadAvailableOrders()
    } else {
      await riderApi.goOffline()
      isOnline.value = false
      uni.showToast({ title: '已下线', icon: 'none' })
    }
  } catch (e) {
    // revert
  }
}

async function loadAvailableOrders() {
  try {
    const res = await orderApi.getList({ status: 1, maxResultCount: 10 })
    availableOrders.value = res.items || []
  } catch (e) {
    availableOrders.value = []
  }
}

async function grabOrder(id) {
  try {
    await orderApi.pickUp(id)
    uni.showToast({ title: '接单成功', icon: 'success' })
    loadAvailableOrders()
  } catch (e) {
    // error handled
  }
}

function goDelivering() {
  uni.navigateTo({ url: `/pages/rider/delivering/index?id=${activeOrder.value.id}` })
}

let signalrConn = null

onMounted(async () => {
  try {
    const info = await riderApi.getMyInfo()
    isOnline.value = info.isOnline || false
    if (isOnline.value) loadAvailableOrders()
    signalrConn = await connectSignalR('rider')
    signalrConn.on('OrderAssigned', (order) => {
      activeOrder.value = order
      uni.vibrateShort()
      uni.showToast({ title: '有新的配送任务！', icon: 'none' })
    })
  } catch (e) {
    // silent
  }
})

onUnmounted(() => disconnectSignalR('rider'))
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; }
.header { padding: 40rpx 30rpx; display: flex; justify-content: space-between; align-items: center; }
.header-online { background: linear-gradient(135deg, #4cd964, #00c853); }
.header-offline { background: linear-gradient(135deg, #999, #bbb); }
.status-text { font-size: 36rpx; font-weight: bold; color: #fff; display: block; }
.status-sub { font-size: 24rpx; color: rgba(255,255,255,0.85); margin-top: 6rpx; display: block; }
.today-stats { background: #fff; display: flex; align-items: center; margin: 0 16rpx; border-radius: 16rpx; margin-top: -20rpx; box-shadow: 0 4rpx 20rpx rgba(0,0,0,0.08); padding: 30rpx 0; }
.stat-item { flex: 1; text-align: center; }
.stat-val { font-size: 36rpx; font-weight: bold; color: #ff6b35; display: block; }
.stat-key { font-size: 22rpx; color: #999; margin-top: 6rpx; display: block; }
.stat-divider { width: 2rpx; height: 60rpx; background: #eee; }
.active-order, .available-orders, .quick-nav { background: #fff; margin: 16rpx; border-radius: 16rpx; padding: 24rpx; }
.section-title { display: flex; justify-content: space-between; margin-bottom: 16rpx; font-size: 30rpx; font-weight: bold; color: #333; }
.refresh-btn { font-size: 26rpx; color: #ff6b35; font-weight: normal; }
.active-card { background: #fff3ee; border-radius: 12rpx; padding: 20rpx; display: flex; justify-content: space-between; align-items: center; }
.active-shop { font-size: 28rpx; font-weight: bold; color: #333; display: block; }
.active-addr { font-size: 24rpx; color: #666; margin-top: 6rpx; display: block; }
.active-status-text { font-size: 24rpx; color: #ff6b35; }
.active-arrow { font-size: 36rpx; color: #ff6b35; margin-left: 8rpx; }
.available-card { background: #fafafa; border-radius: 12rpx; padding: 20rpx; margin-bottom: 16rpx; }
.avail-header { display: flex; justify-content: space-between; margin-bottom: 16rpx; }
.avail-shop { font-size: 28rpx; font-weight: bold; color: #333; }
.avail-fee { font-size: 28rpx; color: #ff6b35; font-weight: bold; }
.route-point { display: flex; align-items: center; margin-bottom: 8rpx; }
.point-dot { font-size: 20rpx; margin-right: 12rpx; }
.dot-from { color: #ff6b35; }
.dot-to { color: #4cd964; }
.route-addr { font-size: 24rpx; color: #666; }
.avail-footer { display: flex; justify-content: space-between; align-items: center; margin-top: 12rpx; }
.avail-distance { font-size: 24rpx; color: #999; }
.btn-grab { background: #ff6b35; color: #fff; font-size: 26rpx; padding: 10rpx 32rpx; border-radius: 40rpx; border: none; }
.quick-nav { display: flex; justify-content: space-around; }
.nav-item { display: flex; flex-direction: column; align-items: center; padding: 10rpx; }
.nav-icon { font-size: 48rpx; }
.nav-label { font-size: 24rpx; color: #333; margin-top: 8rpx; }
.empty-orders { text-align: center; padding: 40rpx; }
.empty-text { font-size: 28rpx; color: #999; }
</style>
