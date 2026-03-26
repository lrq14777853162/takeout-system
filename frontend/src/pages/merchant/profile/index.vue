<template>
  <view class="container">
    <view class="placeholder">
      <text class="page-title">商家资料</text>
      <text class="page-hint">功能开发中</text>
    </view>
  </view>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const loading = ref(false)

onMounted(() => {
  // 初始化页面
})
</script>

<style scoped>
.container {
  padding: 20rpx;
  background: #f5f5f5;
  min-height: 100vh;
}
.placeholder {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 80rpx 40rpx;
  background: #fff;
  border-radius: 16rpx;
  margin-top: 40rpx;
}
.page-title {
  font-size: 36rpx;
  font-weight: bold;
  color: #333;
}
.page-hint {
  font-size: 26rpx;
  color: #999;
  margin-top: 16rpx;
}
</style>
