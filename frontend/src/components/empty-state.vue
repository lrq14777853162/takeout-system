<template>
  <view class="empty-state">
    <text class="empty-icon">{{ icon }}</text>
    <text class="empty-title">{{ title }}</text>
    <text v-if="desc" class="empty-desc">{{ desc }}</text>
    <button v-if="btnText" class="empty-btn" @click="$emit('action')">{{ btnText }}</button>
  </view>
</template>
<script setup>
defineProps({ icon: { type: String, default: '📭' }, title: { type: String, default: '暂无数据' }, desc: String, btnText: String })
defineEmits(['action'])
</script>
<style scoped>
.empty-state { display:flex; flex-direction:column; align-items:center; padding:100rpx 40rpx; }
.empty-icon { font-size:100rpx; }
.empty-title { font-size:30rpx; color:#333; margin-top:24rpx; }
.empty-desc { font-size:26rpx; color:#999; margin-top:12rpx; text-align:center; }
.empty-btn { background:#ff6b35; color:#fff; font-size:28rpx; padding:16rpx 60rpx; border-radius:50rpx; border:none; margin-top:32rpx; }
</style>
