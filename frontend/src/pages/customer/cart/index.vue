<template>
  <view class="container">
    <view v-if="cartStore.items.length === 0" class="empty">
      <text class="empty-icon">🛒</text>
      <text class="empty-text">购物车是空的</text>
      <button class="btn-go-shop" @click="uni.navigateBack()">去点餐</button>
    </view>
    <view v-else>
      <view class="cart-list">
        <view v-for="item in cartStore.items" :key="`${item.productId}-${item.specId}`" class="cart-item">
          <image class="item-img" :src="item.imageUrl || '/static/default-food.png'" mode="aspectFill" />
          <view class="item-info">
            <text class="item-name">{{ item.productName }}</text>
            <view class="item-bottom">
              <text class="item-price">¥{{ item.price?.toFixed(2) }}</text>
              <view class="qty-ctrl">
                <text class="qty-btn" @click="decrease(item)">－</text>
                <text class="qty-num">{{ item.quantity }}</text>
                <text class="qty-btn qty-add" @click="increase(item)">＋</text>
              </view>
            </view>
          </view>
        </view>
      </view>

      <view class="submit-bar">
        <view class="cart-total">
          <text class="total-label">合计</text>
          <text class="total-price">¥{{ cartStore.totalPrice.toFixed(2) }}</text>
        </view>
        <button class="btn-checkout" @click="goCheckout">去结算 ({{ cartStore.totalCount }})</button>
      </view>
    </view>
  </view>
</template>

<script setup>
import { useCartStore } from '../../../store/cart'

const cartStore = useCartStore()

function decrease(item) {
  cartStore.updateQuantity(item.productId, item.specId, item.quantity - 1)
}

function increase(item) {
  cartStore.updateQuantity(item.productId, item.specId, item.quantity + 1)
}

function goCheckout() {
  uni.navigateTo({ url: '/pages/customer/submit-order/index' })
}
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; padding-bottom: 120rpx; }
.empty { display: flex; flex-direction: column; align-items: center; padding: 160rpx 0; }
.empty-icon { font-size: 120rpx; }
.empty-text { font-size: 28rpx; color: #999; margin-top: 20rpx; }
.btn-go-shop { background: #ff6b35; color: #fff; font-size: 28rpx; padding: 18rpx 60rpx; border-radius: 50rpx; border: none; margin-top: 40rpx; }
.cart-list { padding: 16rpx; }
.cart-item { display: flex; background: #fff; border-radius: 16rpx; padding: 20rpx; margin-bottom: 16rpx; }
.item-img { width: 120rpx; height: 120rpx; border-radius: 12rpx; flex-shrink: 0; }
.item-info { flex: 1; padding-left: 16rpx; display: flex; flex-direction: column; justify-content: space-between; }
.item-name { font-size: 28rpx; color: #333; }
.item-bottom { display: flex; justify-content: space-between; align-items: center; }
.item-price { font-size: 30rpx; color: #ff6b35; font-weight: bold; }
.qty-ctrl { display: flex; align-items: center; gap: 16rpx; }
.qty-btn { width: 44rpx; height: 44rpx; border-radius: 50%; background: #eee; text-align: center; line-height: 44rpx; font-size: 28rpx; color: #666; }
.qty-add { background: #ff6b35; color: #fff; }
.qty-num { font-size: 28rpx; color: #333; min-width: 30rpx; text-align: center; }
.submit-bar { position: fixed; bottom: 0; left: 0; right: 0; background: #fff; padding: 16rpx 30rpx; display: flex; align-items: center; box-shadow: 0 -2rpx 12rpx rgba(0,0,0,0.08); padding-bottom: calc(16rpx + env(safe-area-inset-bottom)); }
.cart-total { flex: 1; }
.total-label { font-size: 26rpx; color: #999; }
.total-price { font-size: 36rpx; color: #ff6b35; font-weight: bold; margin-left: 8rpx; }
.btn-checkout { background: #ff6b35; color: #fff; font-size: 28rpx; padding: 18rpx 48rpx; border-radius: 50rpx; border: none; }
</style>
