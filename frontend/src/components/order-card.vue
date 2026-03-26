<template>
  <view class="order-card" @click="$emit('click', order)">
    <view class="order-header">
      <text class="shop-name">{{ order.merchantName }}</text>
      <text class="status">{{ formatOrderStatus(order.status) }}</text>
    </view>
    <text class="items">{{ order.items?.map(i => i.productName).join('、') }}</text>
    <view class="order-footer">
      <text class="time">{{ formatDate(order.creationTime, 'MM-DD HH:mm') }}</text>
      <text class="price">¥{{ order.totalAmount?.toFixed(2) }}</text>
    </view>
  </view>
</template>
<script setup>
import { formatOrderStatus, formatDate } from '../utils/format'
defineProps({ order: { type: Object, required: true } })
defineEmits(['click'])
</script>
<style scoped>
.order-card { background:#fff; border-radius:16rpx; padding:24rpx; margin-bottom:16rpx; }
.order-header { display:flex; justify-content:space-between; margin-bottom:12rpx; }
.shop-name { font-size:30rpx; font-weight:bold; color:#333; }
.status { font-size:26rpx; color:#ff6b35; }
.items { font-size:26rpx; color:#666; display:block; margin-bottom:12rpx; }
.order-footer { display:flex; justify-content:space-between; }
.time { font-size:24rpx; color:#999; }
.price { font-size:28rpx; color:#ff6b35; font-weight:bold; }
</style>
