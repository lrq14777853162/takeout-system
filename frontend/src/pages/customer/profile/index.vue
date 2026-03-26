<template>
  <view class="container">
    <!-- 用户头部 -->
    <view class="profile-header">
      <view class="avatar-wrap">
        <image class="avatar" src="/static/default-avatar.png" mode="aspectFill" />
      </view>
      <view class="user-info">
        <text class="username">{{ userStore.userName || '未登录' }}</text>
        <text class="user-role">{{ roleText }}</text>
      </view>
    </view>

    <!-- 功能菜单 -->
    <view class="menu-section">
      <view class="menu-item" @click="nav('/pages/customer/order-list/index')">
        <text class="menu-icon">📦</text>
        <text class="menu-label">我的订单</text>
        <text class="menu-arrow">›</text>
      </view>
      <view class="menu-item" @click="nav('/pages/customer/address/index')">
        <text class="menu-icon">📍</text>
        <text class="menu-label">收货地址</text>
        <text class="menu-arrow">›</text>
      </view>
      <view class="menu-item" @click="nav('/pages/customer/coupons/index')">
        <text class="menu-icon">🎫</text>
        <text class="menu-label">优惠券</text>
        <text class="menu-arrow">›</text>
      </view>
    </view>

    <view class="menu-section">
      <view v-if="userStore.role === 'merchant'" class="menu-item" @click="nav('/pages/merchant/dashboard/index')">
        <text class="menu-icon">🏪</text>
        <text class="menu-label">商家工作台</text>
        <text class="menu-arrow">›</text>
      </view>
      <view v-if="userStore.role === 'rider'" class="menu-item" @click="nav('/pages/rider/home/index')">
        <text class="menu-icon">🚴</text>
        <text class="menu-label">骑手工作台</text>
        <text class="menu-arrow">›</text>
      </view>
      <view v-if="userStore.role === 'admin'" class="menu-item" @click="nav('/pages/admin/dashboard/index')">
        <text class="menu-icon">⚙️</text>
        <text class="menu-label">后台管理</text>
        <text class="menu-arrow">›</text>
      </view>
    </view>

    <view class="menu-section">
      <view class="menu-item logout" @click="handleLogout">
        <text class="menu-icon">🚪</text>
        <text class="menu-label logout-text">退出登录</text>
        <text class="menu-arrow">›</text>
      </view>
    </view>
  </view>
</template>

<script setup>
import { computed } from 'vue'
import { useUserStore } from '../../../store/user'

const userStore = useUserStore()

const roleText = computed(() => {
  const map = { customer: '普通用户', merchant: '商家', rider: '骑手', admin: '管理员' }
  return map[userStore.role] || '用户'
})

function nav(url) { uni.navigateTo({ url }) }

async function handleLogout() {
  const { confirm } = await new Promise(r => uni.showModal({ title: '提示', content: '确认退出登录？', success: r }))
  if (!confirm) return
  userStore.logout()
  uni.reLaunch({ url: '/pages/common/login/index' })
}
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; }
.profile-header { background: linear-gradient(135deg, #ff6b35, #ff9500); padding: 60rpx 30rpx 80rpx; display: flex; align-items: center; }
.avatar-wrap { width: 120rpx; height: 120rpx; border-radius: 60rpx; overflow: hidden; border: 4rpx solid rgba(255,255,255,0.8); }
.avatar { width: 100%; height: 100%; }
.user-info { margin-left: 24rpx; }
.username { font-size: 36rpx; font-weight: bold; color: #fff; display: block; }
.user-role { font-size: 24rpx; color: rgba(255,255,255,0.85); margin-top: 6rpx; display: block; }
.menu-section { background: #fff; margin: 16rpx; border-radius: 16rpx; overflow: hidden; }
.menu-item { display: flex; align-items: center; padding: 32rpx 30rpx; border-bottom: 1rpx solid #f5f5f5; }
.menu-item:last-child { border-bottom: none; }
.menu-icon { font-size: 40rpx; margin-right: 20rpx; }
.menu-label { flex: 1; font-size: 28rpx; color: #333; }
.menu-arrow { font-size: 36rpx; color: #ccc; }
.logout-text { color: #ff3b30; }
</style>
