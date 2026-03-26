<template>
  <view class="container">
    <!-- 收货地址 -->
    <view class="section address-section" @click="selectAddress">
      <view v-if="selectedAddress" class="address-info">
        <text class="addr-name">{{ selectedAddress.contactName }} {{ selectedAddress.contactPhone }}</text>
        <text class="addr-detail">{{ selectedAddress.address }}</text>
      </view>
      <view v-else class="no-address">
        <text class="no-addr-text">请选择收货地址</text>
      </view>
      <text class="arrow">›</text>
    </view>

    <!-- 商品列表 -->
    <view class="section">
      <view class="section-title"><text>订单商品</text></view>
      <view v-for="item in cartStore.items" :key="item.productId" class="order-item">
        <text class="item-name">{{ item.productName }}</text>
        <text class="item-qty">×{{ item.quantity }}</text>
        <text class="item-price">¥{{ (item.price * item.quantity).toFixed(2) }}</text>
      </view>
    </view>

    <!-- 备注 -->
    <view class="section">
      <view class="section-title"><text>备注</text></view>
      <textarea
        v-model="remark"
        class="remark-input"
        placeholder="口味偏好、特殊要求等（选填）"
        maxlength="100"
      />
    </view>

    <!-- 优惠券 -->
    <view class="section coupon-row" @click="selectCoupon">
      <text class="row-label">优惠券</text>
      <view class="row-right">
        <text class="coupon-text" :class="{ 'has-coupon': selectedCoupon }">
          {{ selectedCoupon ? `-¥${selectedCoupon.discountAmount?.toFixed(2)}` : '暂无可用' }}
        </text>
        <text class="arrow">›</text>
      </view>
    </view>

    <!-- 费用明细 -->
    <view class="section fee-section">
      <view class="fee-row">
        <text>商品合计</text>
        <text>¥{{ cartStore.totalPrice.toFixed(2) }}</text>
      </view>
      <view class="fee-row">
        <text>配送费</text>
        <text>¥{{ deliveryFee.toFixed(2) }}</text>
      </view>
      <view v-if="selectedCoupon" class="fee-row discount">
        <text>优惠券</text>
        <text>-¥{{ selectedCoupon.discountAmount?.toFixed(2) }}</text>
      </view>
      <view class="fee-total">
        <text>实付款</text>
        <text class="total-price">¥{{ totalAmount.toFixed(2) }}</text>
      </view>
    </view>

    <!-- 提交按钮 -->
    <view class="submit-bar">
      <view class="submit-price">
        <text class="price-label">合计</text>
        <text class="price-val">¥{{ totalAmount.toFixed(2) }}</text>
      </view>
      <button class="btn-submit" :disabled="submitting" @click="submitOrder">
        {{ submitting ? '提交中...' : '提交订单' }}
      </button>
    </view>
  </view>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useCartStore } from '../../../store/cart'
import { orderApi } from '../../../api/order'
import { userApi } from '../../../api/user'

const cartStore = useCartStore()
const selectedAddress = ref(null)
const selectedCoupon = ref(null)
const remark = ref('')
const submitting = ref(false)
const deliveryFee = ref(3)

const totalAmount = computed(() => {
  let t = cartStore.totalPrice + deliveryFee.value
  if (selectedCoupon.value) t -= selectedCoupon.value.discountAmount || 0
  return Math.max(t, 0)
})

function selectAddress() {
  uni.navigateTo({ url: '/pages/customer/address/index?mode=select' })
}

function selectCoupon() {
  uni.navigateTo({ url: '/pages/customer/coupons/index?mode=select' })
}

async function submitOrder() {
  if (!selectedAddress.value) {
    uni.showToast({ title: '请选择收货地址', icon: 'none' })
    return
  }
  if (cartStore.items.length === 0) {
    uni.showToast({ title: '购物车为空', icon: 'none' })
    return
  }
  submitting.value = true
  try {
    const order = await orderApi.create({
      merchantId: cartStore.merchantId,
      addressId: selectedAddress.value.id,
      couponId: selectedCoupon.value?.id,
      remark: remark.value,
      items: cartStore.items.map(i => ({
        productId: i.productId,
        specId: i.specId,
        quantity: i.quantity,
        price: i.price
      }))
    })
    cartStore.clearCart()
    uni.redirectTo({ url: `/pages/customer/order-detail/index?id=${order.id}` })
  } finally {
    submitting.value = false
  }
}

onMounted(async () => {
  try {
    const addrs = await userApi.getAddresses()
    const def = (addrs.items || addrs || []).find(a => a.isDefault) || addrs.items?.[0]
    if (def) selectedAddress.value = def
  } catch (e) {
    // silent
  }
})
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; padding-bottom: 120rpx; }
.section { background: #fff; margin-bottom: 16rpx; padding: 24rpx 30rpx; }
.section-title { font-size: 28rpx; font-weight: bold; color: #333; margin-bottom: 16rpx; }
.address-section { display: flex; align-items: center; }
.address-info { flex: 1; }
.addr-name { font-size: 28rpx; color: #333; font-weight: bold; display: block; }
.addr-detail { font-size: 26rpx; color: #666; margin-top: 6rpx; display: block; }
.no-address { flex: 1; }
.no-addr-text { font-size: 28rpx; color: #999; }
.arrow { font-size: 36rpx; color: #ccc; margin-left: 16rpx; }
.order-item { display: flex; align-items: center; padding: 12rpx 0; border-bottom: 1rpx solid #f5f5f5; }
.item-name { flex: 1; font-size: 28rpx; color: #333; }
.item-qty { font-size: 26rpx; color: #999; margin: 0 16rpx; }
.item-price { font-size: 28rpx; color: #333; }
.remark-input { width: 100%; height: 120rpx; font-size: 26rpx; color: #333; background: #f7f7f7; border-radius: 8rpx; padding: 16rpx; box-sizing: border-box; }
.coupon-row { display: flex; align-items: center; }
.row-label { font-size: 28rpx; color: #333; flex: 1; }
.row-right { display: flex; align-items: center; }
.coupon-text { font-size: 26rpx; color: #999; }
.has-coupon { color: #ff6b35; font-weight: bold; }
.fee-row { display: flex; justify-content: space-between; padding: 12rpx 0; font-size: 26rpx; color: #666; }
.discount { color: #ff6b35; }
.fee-total { display: flex; justify-content: space-between; padding-top: 16rpx; margin-top: 8rpx; border-top: 1rpx solid #eee; font-size: 28rpx; color: #333; font-weight: bold; }
.total-price { color: #ff6b35; font-size: 34rpx; }
.submit-bar { position: fixed; bottom: 0; left: 0; right: 0; background: #fff; padding: 16rpx 30rpx; display: flex; align-items: center; box-shadow: 0 -2rpx 12rpx rgba(0,0,0,0.08); padding-bottom: calc(16rpx + env(safe-area-inset-bottom)); }
.submit-price { flex: 1; }
.price-label { font-size: 24rpx; color: #999; }
.price-val { font-size: 36rpx; color: #ff6b35; font-weight: bold; margin-left: 8rpx; }
.btn-submit { background: #ff6b35; color: #fff; font-size: 30rpx; padding: 18rpx 60rpx; border-radius: 50rpx; border: none; }
.btn-submit[disabled] { opacity: 0.6; }
</style>
