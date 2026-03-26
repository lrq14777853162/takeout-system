<template>
  <view class="container">
    <view class="header">
      <text class="title">后台管理</text>
      <text class="subtitle">{{ today }}</text>
    </view>

    <!-- 核心指标 -->
    <view class="stats-section">
      <view class="stats-row">
        <view class="stat-card primary">
          <text class="stat-icon">📦</text>
          <text class="stat-num">{{ stats.totalOrders || 0 }}</text>
          <text class="stat-label">总订单数</text>
        </view>
        <view class="stat-card success">
          <text class="stat-icon">💰</text>
          <text class="stat-num">¥{{ (stats.totalRevenue || 0).toFixed(0) }}</text>
          <text class="stat-label">总营收</text>
        </view>
      </view>
      <view class="stats-row">
        <view class="stat-card warning">
          <text class="stat-icon">🏪</text>
          <text class="stat-num">{{ stats.pendingMerchants || 0 }}</text>
          <text class="stat-label">待审商家</text>
        </view>
        <view class="stat-card info">
          <text class="stat-icon">🚴</text>
          <text class="stat-num">{{ stats.pendingRiders || 0 }}</text>
          <text class="stat-label">待审骑手</text>
        </view>
      </view>
      <view class="stats-row">
        <view class="stat-card">
          <text class="stat-icon">👥</text>
          <text class="stat-num">{{ stats.totalUsers || 0 }}</text>
          <text class="stat-label">注册用户</text>
        </view>
        <view class="stat-card">
          <text class="stat-icon">🔄</text>
          <text class="stat-num">{{ stats.pendingRefunds || 0 }}</text>
          <text class="stat-label">待处理退款</text>
        </view>
      </view>
    </view>

    <!-- 管理模块 -->
    <view class="modules">
      <view class="section-title"><text>管理模块</text></view>
      <view class="module-grid">
        <view class="module-item" @click="nav('/pages/admin/merchant-audit/index')">
          <text class="module-icon">🏪</text>
          <text class="module-name">商家审核</text>
          <view v-if="stats.pendingMerchants > 0" class="badge">{{ stats.pendingMerchants }}</view>
        </view>
        <view class="module-item" @click="nav('/pages/admin/rider-audit/index')">
          <text class="module-icon">🚴</text>
          <text class="module-name">骑手审核</text>
          <view v-if="stats.pendingRiders > 0" class="badge">{{ stats.pendingRiders }}</view>
        </view>
        <view class="module-item" @click="nav('/pages/admin/merchant-list/index')">
          <text class="module-icon">📋</text>
          <text class="module-name">商家管理</text>
        </view>
        <view class="module-item" @click="nav('/pages/admin/rider-list/index')">
          <text class="module-icon">👨‍💼</text>
          <text class="module-name">骑手管理</text>
        </view>
        <view class="module-item" @click="nav('/pages/admin/order-manage/index')">
          <text class="module-icon">📦</text>
          <text class="module-name">订单管理</text>
        </view>
        <view class="module-item" @click="nav('/pages/admin/refund-manage/index')">
          <text class="module-icon">🔄</text>
          <text class="module-name">退款管理</text>
          <view v-if="stats.pendingRefunds > 0" class="badge">{{ stats.pendingRefunds }}</view>
        </view>
        <view class="module-item" @click="nav('/pages/admin/user-manage/index')">
          <text class="module-icon">👥</text>
          <text class="module-name">用户管理</text>
        </view>
        <view class="module-item" @click="nav('/pages/admin/finance/index')">
          <text class="module-icon">💹</text>
          <text class="module-name">财务管理</text>
        </view>
        <view class="module-item" @click="nav('/pages/admin/coupon-manage/index')">
          <text class="module-icon">🎫</text>
          <text class="module-name">优惠券</text>
        </view>
        <view class="module-item" @click="nav('/pages/admin/category-manage/index')">
          <text class="module-icon">🗂️</text>
          <text class="module-name">分类管理</text>
        </view>
        <view class="module-item" @click="nav('/pages/admin/statistics/index')">
          <text class="module-icon">📊</text>
          <text class="module-name">数据统计</text>
        </view>
        <view class="module-item" @click="nav('/pages/admin/system-config/index')">
          <text class="module-icon">⚙️</text>
          <text class="module-name">系统配置</text>
        </view>
      </view>
    </view>
  </view>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { adminApi } from '../../../api/admin'
import dayjs from 'dayjs'

const stats = ref({})
const today = dayjs().format('YYYY年MM月DD日')

function nav(url) { uni.navigateTo({ url }) }

onMounted(async () => {
  try {
    const data = await adminApi.getDashboard()
    stats.value = data
  } catch (e) {
    // silent
  }
})
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; }
.header { background: linear-gradient(135deg, #667eea, #764ba2); padding: 40rpx 30rpx; }
.title { font-size: 40rpx; font-weight: bold; color: #fff; display: block; }
.subtitle { font-size: 26rpx; color: rgba(255,255,255,0.8); margin-top: 6rpx; display: block; }
.stats-section { padding: 16rpx; }
.stats-row { display: flex; gap: 16rpx; margin-bottom: 16rpx; }
.stat-card { flex: 1; background: #fff; border-radius: 16rpx; padding: 24rpx 20rpx; display: flex; flex-direction: column; align-items: center; box-shadow: 0 2rpx 12rpx rgba(0,0,0,0.06); position: relative; }
.stat-icon { font-size: 40rpx; margin-bottom: 8rpx; }
.stat-num { font-size: 32rpx; font-weight: bold; color: #333; }
.stat-label { font-size: 22rpx; color: #999; margin-top: 4rpx; }
.stat-card.primary .stat-num { color: #ff6b35; }
.stat-card.success .stat-num { color: #4cd964; }
.stat-card.warning .stat-num { color: #ff9500; }
.stat-card.info .stat-num { color: #007aff; }
.modules { background: #fff; margin: 0 16rpx 16rpx; border-radius: 16rpx; padding: 24rpx; }
.section-title { font-size: 30rpx; font-weight: bold; color: #333; margin-bottom: 20rpx; }
.module-grid { display: flex; flex-wrap: wrap; gap: 16rpx; }
.module-item { width: calc(25% - 12rpx); display: flex; flex-direction: column; align-items: center; padding: 20rpx 0; background: #fafafa; border-radius: 12rpx; position: relative; }
.module-icon { font-size: 40rpx; }
.module-name { font-size: 22rpx; color: #333; margin-top: 8rpx; text-align: center; }
.badge { position: absolute; top: 8rpx; right: 8rpx; background: #ff3b30; color: #fff; font-size: 18rpx; min-width: 32rpx; height: 32rpx; border-radius: 16rpx; display: flex; align-items: center; justify-content: center; padding: 0 6rpx; }
</style>
