<template>
  <view class="container">
    <view v-if="coupons.length === 0" class="empty">
      <text class="empty-icon">🎫</text>
      <text class="empty-text">暂无优惠券</text>
    </view>
    <view v-else class="coupon-list">
      <view v-for="c in coupons" :key="c.id" class="coupon-card" @click="selectCoupon(c)">
        <view class="coupon-left">
          <text class="discount">¥{{ c.discountAmount }}</text>
          <text class="condition">满{{ c.minOrderAmount }}可用</text>
        </view>
        <view class="coupon-right">
          <text class="coupon-name">{{ c.name }}</text>
          <text class="coupon-expire">有效期至 {{ c.expiryDate }}</text>
          <view v-if="c.isUsed" class="used-tag"><text>已使用</text></view>
        </view>
      </view>
    </view>
  </view>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { userApi } from '../../../api/user'

const pages = getCurrentPages()
const query = pages[pages.length - 1]?.options || {}
const isSelectMode = query.mode === 'select'

const coupons = ref([])

function selectCoupon(c) {
  if (isSelectMode && !c.isUsed) {
    const pages = getCurrentPages()
    const prevPage = pages[pages.length - 2]
    if (prevPage) prevPage.$vm.selectedCoupon = c
    uni.navigateBack()
  }
}

onMounted(async () => {
  try {
    const res = await userApi.getCoupons()
    coupons.value = res.items || res || []
  } catch (e) { coupons.value = [] }
})
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; padding: 16rpx; }
.empty { display: flex; flex-direction: column; align-items: center; padding: 100rpx 0; }
.empty-icon { font-size: 100rpx; }
.empty-text { font-size: 28rpx; color: #999; margin-top: 16rpx; }
.coupon-card { display: flex; background: #fff; border-radius: 16rpx; margin-bottom: 16rpx; overflow: hidden; }
.coupon-left { background: #ff6b35; width: 180rpx; display: flex; flex-direction: column; align-items: center; justify-content: center; padding: 30rpx 0; flex-shrink: 0; }
.discount { font-size: 52rpx; font-weight: bold; color: #fff; }
.condition { font-size: 20rpx; color: rgba(255,255,255,0.9); margin-top: 4rpx; }
.coupon-right { flex: 1; padding: 24rpx; position: relative; }
.coupon-name { font-size: 28rpx; color: #333; font-weight: bold; display: block; }
.coupon-expire { font-size: 22rpx; color: #999; margin-top: 8rpx; display: block; }
.used-tag { position: absolute; top: 16rpx; right: 16rpx; background: #ccc; color: #fff; font-size: 20rpx; padding: 4rpx 12rpx; border-radius: 20rpx; }
</style>
