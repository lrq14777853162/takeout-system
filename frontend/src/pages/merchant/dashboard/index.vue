<template>
  <view class="container">
    <!-- 头部欢迎 -->
    <view class="header">
      <view class="header-left">
        <text class="welcome">{{ merchant.name || '商家工作台' }}</text>
        <text class="subtitle">{{ isOpen ? '营业中' : '已打烊' }}</text>
      </view>
      <switch :checked="isOpen" @change="toggleOpen" color="#ff6b35" />
    </view>

    <!-- 今日数据卡片 -->
    <view class="stats-grid">
      <view class="stat-card">
        <text class="stat-num">{{ stats.todayOrderCount || 0 }}</text>
        <text class="stat-label">今日订单</text>
      </view>
      <view class="stat-card">
        <text class="stat-num">¥{{ (stats.todayRevenue || 0).toFixed(0) }}</text>
        <text class="stat-label">今日营收</text>
      </view>
      <view class="stat-card">
        <text class="stat-num">{{ stats.pendingOrderCount || 0 }}</text>
        <text class="stat-label">待处理</text>
      </view>
      <view class="stat-card">
        <text class="stat-num">{{ stats.rating?.toFixed(1) || '5.0' }}</text>
        <text class="stat-label">评分</text>
      </view>
    </view>

    <!-- 快捷功能 -->
    <view class="quick-actions">
      <view class="section-title"><text>快捷功能</text></view>
      <view class="action-grid">
        <view class="action-item" @click="nav('/pages/merchant/order-manage/index')">
          <text class="action-icon">📋</text>
          <text class="action-label">订单管理</text>
        </view>
        <view class="action-item" @click="nav('/pages/merchant/product-manage/index')">
          <text class="action-icon">🍱</text>
          <text class="action-label">商品管理</text>
        </view>
        <view class="action-item" @click="nav('/pages/merchant/statistics/index')">
          <text class="action-icon">📊</text>
          <text class="action-label">数据统计</text>
        </view>
        <view class="action-item" @click="nav('/pages/merchant/finance/index')">
          <text class="action-icon">💰</text>
          <text class="action-label">财务管理</text>
        </view>
        <view class="action-item" @click="nav('/pages/merchant/category-manage/index')">
          <text class="action-icon">🗂️</text>
          <text class="action-label">分类管理</text>
        </view>
        <view class="action-item" @click="nav('/pages/merchant/shop-settings/index')">
          <text class="action-icon">⚙️</text>
          <text class="action-label">店铺设置</text>
        </view>
      </view>
    </view>

    <!-- 待处理订单 -->
    <view class="pending-orders">
      <view class="section-title">
        <text>待处理订单</text>
        <text class="view-all" @click="nav('/pages/merchant/order-manage/index')">查看全部</text>
      </view>
      <view v-if="pendingOrders.length === 0" class="empty-orders">
        <text class="empty-text">暂无待处理订单 🎉</text>
      </view>
      <view
        v-for="order in pendingOrders"
        :key="order.id"
        class="pending-card"
        @click="nav(`/pages/merchant/order-manage/index?id=${order.id}`)"
      >
        <view class="pending-info">
          <text class="pending-no">订单号: {{ order.orderNo }}</text>
          <text class="pending-amount">¥{{ order.totalAmount?.toFixed(2) }}</text>
        </view>
        <view class="pending-items">
          <text class="pending-products">{{ order.items?.map(i => i.productName).join('、') }}</text>
        </view>
        <view class="pending-footer">
          <text class="pending-time">{{ formatTime(order.creationTime) }}</text>
          <button class="btn-accept" @click.stop="acceptOrder(order.id)">接单</button>
        </view>
      </view>
    </view>
  </view>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { merchantApi } from '../../../api/merchant'
import { orderApi } from '../../../api/order'
import { connectSignalR, disconnectSignalR } from '../../../utils/signalr'
import { formatDate } from '../../../utils/format'

const merchant = ref({})
const stats = ref({})
const isOpen = ref(false)
const pendingOrders = ref([])

function formatTime(t) { return formatDate(t, 'HH:mm') }
function nav(url) { uni.navigateTo({ url }) }

async function loadData() {
  try {
    const m = await merchantApi.getMyMerchant()
    merchant.value = m
    isOpen.value = m.isOpen || false
    const orders = await orderApi.getMerchantOrders(m.id, { status: 1, maxResultCount: 5 })
    pendingOrders.value = orders.items || []
    stats.value = {
      todayOrderCount: orders.totalCount || 0,
      todayRevenue: 0,
      pendingOrderCount: pendingOrders.value.length,
      rating: m.rating
    }
  } catch (e) {
    // silent fail
  }
}

async function toggleOpen(e) {
  isOpen.value = e.detail.value
  try {
    await merchantApi.update(merchant.value.id, { isOpen: isOpen.value })
    uni.showToast({ title: isOpen.value ? '已开启营业' : '已打烊', icon: 'none' })
  } catch (e) {
    isOpen.value = !isOpen.value
  }
}

async function acceptOrder(id) {
  await orderApi.accept(id)
  uni.showToast({ title: '已接单', icon: 'success' })
  loadData()
}

let signalrConn = null

onMounted(async () => {
  await loadData()
  try {
    signalrConn = await connectSignalR('order')
    signalrConn.on('NewOrder', () => {
      uni.vibrateShort()
      loadData()
    })
  } catch (e) {
    // signalr optional
  }
})

onUnmounted(() => {
  disconnectSignalR('order')
})
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; }
.header { background: linear-gradient(135deg, #ff6b35, #ff9500); padding: 40rpx 30rpx; display: flex; justify-content: space-between; align-items: center; }
.welcome { font-size: 36rpx; font-weight: bold; color: #fff; display: block; }
.subtitle { font-size: 24rpx; color: rgba(255,255,255,0.85); margin-top: 6rpx; display: block; }
.stats-grid { display: flex; padding: 20rpx 16rpx; gap: 16rpx; }
.stat-card { flex: 1; background: #fff; border-radius: 16rpx; padding: 24rpx 16rpx; text-align: center; box-shadow: 0 2rpx 12rpx rgba(0,0,0,0.06); }
.stat-num { font-size: 36rpx; font-weight: bold; color: #ff6b35; display: block; }
.stat-label { font-size: 22rpx; color: #999; margin-top: 6rpx; display: block; }
.quick-actions, .pending-orders { background: #fff; margin: 16rpx; border-radius: 16rpx; padding: 24rpx; }
.section-title { display: flex; justify-content: space-between; align-items: center; margin-bottom: 20rpx; }
.section-title text { font-size: 30rpx; font-weight: bold; color: #333; }
.view-all { font-size: 26rpx; color: #ff6b35; font-weight: normal; }
.action-grid { display: flex; flex-wrap: wrap; gap: 16rpx; }
.action-item { width: calc(33.333% - 12rpx); display: flex; flex-direction: column; align-items: center; padding: 24rpx 0; background: #fafafa; border-radius: 12rpx; }
.action-icon { font-size: 48rpx; }
.action-label { font-size: 24rpx; color: #333; margin-top: 10rpx; }
.pending-card { background: #fafafa; border-radius: 12rpx; padding: 20rpx; margin-bottom: 16rpx; }
.pending-info { display: flex; justify-content: space-between; }
.pending-no { font-size: 24rpx; color: #999; }
.pending-amount { font-size: 28rpx; color: #ff6b35; font-weight: bold; }
.pending-products { font-size: 26rpx; color: #333; margin: 10rpx 0; display: block; }
.pending-footer { display: flex; justify-content: space-between; align-items: center; margin-top: 10rpx; }
.pending-time { font-size: 24rpx; color: #999; }
.btn-accept { background: #ff6b35; color: #fff; font-size: 26rpx; padding: 10rpx 32rpx; border-radius: 40rpx; border: none; }
.empty-orders { text-align: center; padding: 40rpx 0; }
.empty-text { font-size: 28rpx; color: #999; }
</style>
