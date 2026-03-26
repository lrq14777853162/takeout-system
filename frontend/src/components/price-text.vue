<template>
  <text class="price" :class="size">¥{{ Number(value).toFixed(2) }}</text>
</template>
<script setup>
defineProps({ value: { type: [Number, String], default: 0 }, size: { type: String, default: 'md' } })
</script>
<style scoped>
.price { color:#ff6b35; font-weight:bold; }
.sm { font-size:24rpx; }
.md { font-size:32rpx; }
.lg { font-size:40rpx; }
</style>
