<template>
  <view v-if="cartStore.totalCount > 0" class="cart-bar" @click="$emit('checkout')">
    <view class="cart-left">
      <text class="cart-icon">🛒</text>
      <view class="badge">{{ cartStore.totalCount }}</view>
    </view>
    <text class="cart-total">¥{{ cartStore.totalPrice.toFixed(2) }}</text>
    <text class="checkout-btn">去结算</text>
  </view>
</template>
<script setup>
import { useCartStore } from '../store/cart'
const cartStore = useCartStore()
defineEmits(['checkout'])
</script>
<style scoped>
.cart-bar { position:fixed; bottom:0; left:0; right:0; background:#333; padding:16rpx 30rpx; display:flex; align-items:center; padding-bottom:calc(16rpx + env(safe-area-inset-bottom)); }
.cart-left { position:relative; margin-right:16rpx; }
.cart-icon { font-size:44rpx; }
.badge { position:absolute; top:-8rpx; right:-8rpx; background:#ff3b30; color:#fff; font-size:18rpx; min-width:28rpx; height:28rpx; border-radius:14rpx; display:flex; align-items:center; justify-content:center; }
.cart-total { flex:1; font-size:30rpx; color:#fff; font-weight:bold; }
.checkout-btn { background:#ff6b35; color:#fff; font-size:28rpx; padding:14rpx 36rpx; border-radius:40rpx; }
</style>
