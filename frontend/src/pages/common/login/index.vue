<template>
  <view class="container">
    <view class="logo-area">
      <text class="logo-icon">🍜</text>
      <text class="logo-title">外卖平台</text>
      <text class="logo-subtitle">美食，随时送达</text>
    </view>

    <!-- 角色选择 -->
    <view class="role-tabs">
      <view
        v-for="r in roles"
        :key="r.value"
        class="role-tab"
        :class="{ active: selectedRole === r.value }"
        @click="selectedRole = r.value"
      >
        <text>{{ r.label }}</text>
      </view>
    </view>

    <view class="form-card">
      <view class="input-group">
        <text class="input-label">账号</text>
        <input
          v-model="username"
          class="input-field"
          placeholder="请输入账号"
          :placeholder-style="placeholderStyle"
        />
      </view>
      <view class="input-group">
        <text class="input-label">密码</text>
        <input
          v-model="password"
          class="input-field"
          placeholder="请输入密码"
          password
          :placeholder-style="placeholderStyle"
        />
      </view>

      <button class="btn-login" :disabled="loading" @click="handleLogin">
        <text>{{ loading ? '登录中...' : '登录' }}</text>
      </button>
    </view>

    <view class="footer-links">
      <text class="link-text">还没有账号？立即注册</text>
    </view>
  </view>
</template>

<script setup>
import { ref } from 'vue'
import { authApi } from '../../../api/auth'
import { useUserStore } from '../../../store/user'

const userStore = useUserStore()
const username = ref('')
const password = ref('')
const loading = ref(false)
const placeholderStyle = 'color: #999; font-size: 28rpx;'

const selectedRole = ref('customer')
const roles = [
  { label: '用户', value: 'customer' },
  { label: '商家', value: 'merchant' },
  { label: '骑手', value: 'rider' },
  { label: '管理员', value: 'admin' }
]

const roleRedirect = {
  customer: '/pages/customer/home/index',
  merchant: '/pages/merchant/dashboard/index',
  rider: '/pages/rider/home/index',
  admin: '/pages/admin/dashboard/index'
}

async function handleLogin() {
  if (!username.value.trim()) {
    uni.showToast({ title: '请输入账号', icon: 'none' })
    return
  }
  if (!password.value) {
    uni.showToast({ title: '请输入密码', icon: 'none' })
    return
  }
  loading.value = true
  try {
    const res = await authApi.login({ userNameOrEmailAddress: username.value, password: password.value })
    userStore.setToken(res.access_token, res.refresh_token)
    const profile = await authApi.getProfile()
    userStore.setUserInfo(profile)
    const redirect = roleRedirect[selectedRole.value] || roleRedirect.customer
    uni.reLaunch({ url: redirect })
  } catch (e) {
    // error toast handled by request.js
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.container {
  min-height: 100vh;
  background: linear-gradient(160deg, #ff6b35 0%, #ff9500 100%);
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 100rpx 40rpx 60rpx;
}
.logo-area {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 60rpx;
}
.logo-icon {
  font-size: 100rpx;
}
.logo-title {
  font-size: 52rpx;
  font-weight: bold;
  color: #fff;
  margin-top: 16rpx;
}
.logo-subtitle {
  font-size: 28rpx;
  color: rgba(255,255,255,0.85);
  margin-top: 8rpx;
}
.role-tabs {
  display: flex;
  background: rgba(255,255,255,0.2);
  border-radius: 50rpx;
  padding: 6rpx;
  margin-bottom: 40rpx;
  width: 100%;
}
.role-tab {
  flex: 1;
  text-align: center;
  padding: 16rpx 0;
  border-radius: 44rpx;
  font-size: 26rpx;
  color: rgba(255,255,255,0.8);
  transition: all 0.2s;
}
.role-tab.active {
  background: #fff;
  color: #ff6b35;
  font-weight: bold;
}
.form-card {
  background: #fff;
  border-radius: 24rpx;
  padding: 48rpx 40rpx;
  width: 100%;
  box-shadow: 0 8rpx 40rpx rgba(0,0,0,0.12);
}
.input-group {
  margin-bottom: 32rpx;
}
.input-label {
  font-size: 26rpx;
  color: #666;
  margin-bottom: 12rpx;
  display: block;
}
.input-field {
  width: 100%;
  height: 88rpx;
  background: #f7f7f7;
  border-radius: 12rpx;
  padding: 0 28rpx;
  font-size: 28rpx;
  color: #333;
  box-sizing: border-box;
}
.btn-login {
  width: 100%;
  height: 96rpx;
  background: linear-gradient(90deg, #ff6b35, #ff9500);
  border-radius: 48rpx;
  color: #fff;
  font-size: 32rpx;
  font-weight: bold;
  border: none;
  margin-top: 16rpx;
}
.btn-login[disabled] {
  opacity: 0.6;
}
.footer-links {
  margin-top: 40rpx;
}
.link-text {
  color: rgba(255,255,255,0.9);
  font-size: 28rpx;
}
</style>
