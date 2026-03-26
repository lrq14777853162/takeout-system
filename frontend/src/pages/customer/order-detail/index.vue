<template>
  <view class="container">
    <view v-if="loading" class="loading"><text>加载中...</text></view>
    <view v-else-if="order">
      <!-- 状态卡 -->
      <view class="status-card">
        <text class="status-text">{{ formatOrderStatus(order.status) }}</text>
        <text class="status-sub">{{ statusDesc[order.status] || '' }}</text>
      </view>

      <!-- 配送信息 -->
      <view class="section">
        <view class="section-title"><text>📍 收货信息</text></view>
        <text class="addr-name">{{ order.contactName }} {{ order.contactPhone }}</text>
        <text class="addr-detail">{{ order.deliveryAddress }}</text>
      </view>

      <!-- 商品列表 -->
      <view class="section">
        <view class="section-title"><text>🍱 {{ order.merchantName }}</text></view>
        <view v-for="item in order.items" :key="item.id" class="item-row">
          <text class="item-name">{{ item.productName }}</text>
          <text class="item-qty">×{{ item.quantity }}</text>
          <text class="item-price">¥{{ (item.price * item.quantity).toFixed(2) }}</text>
        </view>
        <view class="fee-row">
          <text>配送费</text>
          <text>¥{{ order.deliveryFee?.toFixed(2) || '0.00' }}</text>
        </view>
        <view class="fee-total">
          <text>实付款</text>
          <text class="total-price">¥{{ order.totalAmount?.toFixed(2) }}</text>
        </view>
      </view>

      <!-- 订单信息 -->
      <view class="section order-info">
        <view class="info-row">
          <text class="info-key">订单编号</text>
          <text class="info-val">{{ order.orderNo }}</text>
        </view>
        <view class="info-row">
          <text class="info-key">下单时间</text>
          <text class="info-val">{{ formatDate(order.creationTime) }}</text>
        </view>
        <view v-if="order.remark" class="info-row">
          <text class="info-key">备注</text>
          <text class="info-val">{{ order.remark }}</text>
        </view>
      </view>

      <!-- 操作按钮 -->
      <view class="action-bar">
        <button v-if="order.status === 0" class="btn-primary" @click="payOrder">立即支付</button>
        <button v-if="[0,1].includes(order.status)" class="btn-secondary" @click="cancelOrder">取消订单</button>
        <button v-if="order.status === 7" class="btn-primary" @click="goReview">评价订单</button>
      </view>
    </view>
  </view>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { orderApi } from '../../../api/order'
import { formatOrderStatus, formatDate } from '../../../utils/format'

const order = ref(null)
const loading = ref(true)

const statusDesc = {
  0: '请尽快完成支付',
  1: '等待商家接单',
  2: '商家已接单，准备中',
  3: '等待骑手取餐',
  4: '骑手正在取餐',
  5: '外卖正在配送中',
  6: '外卖已送达，请确认',
  7: '感谢您的光顾，欢迎再来！',
  8: '订单已取消',
  9: '退款处理中',
  10: '退款已完成'
}

const pages = getCurrentPages()
const query = pages[pages.length - 1]?.options || {}

async function loadOrder() {
  if (!query.id) return
  loading.value = true
  try {
    order.value = await orderApi.getById(query.id)
  } finally {
    loading.value = false
  }
}

async function cancelOrder() {
  const { confirm } = await new Promise(r => uni.showModal({ title: '提示', content: '确认取消订单？', success: r }))
  if (!confirm) return
  await orderApi.cancel(query.id, '用户主动取消')
  loadOrder()
}

function payOrder() {
  uni.showToast({ title: '跳转支付...', icon: 'none' })
}

function goReview() {
  uni.navigateTo({ url: `/pages/customer/review/index?orderId=${query.id}` })
}

onMounted(loadOrder)
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; padding-bottom: 40rpx; }
.loading { text-align: center; padding: 100rpx; color: #999; font-size: 28rpx; }
.status-card { background: linear-gradient(135deg, #ff6b35, #ff9500); padding: 40rpx 30rpx; }
.status-text { font-size: 40rpx; font-weight: bold; color: #fff; display: block; }
.status-sub { font-size: 26rpx; color: rgba(255,255,255,0.85); margin-top: 8rpx; display: block; }
.section { background: #fff; margin: 16rpx; border-radius: 16rpx; padding: 24rpx 30rpx; }
.section-title { font-size: 28rpx; font-weight: bold; color: #333; margin-bottom: 16rpx; display: block; }
.addr-name { font-size: 28rpx; color: #333; font-weight: bold; display: block; }
.addr-detail { font-size: 26rpx; color: #666; margin-top: 6rpx; display: block; }
.item-row { display: flex; align-items: center; padding: 12rpx 0; border-bottom: 1rpx solid #f5f5f5; }
.item-name { flex: 1; font-size: 28rpx; color: #333; }
.item-qty { font-size: 26rpx; color: #999; margin: 0 16rpx; }
.item-price { font-size: 28rpx; color: #333; }
.fee-row { display: flex; justify-content: space-between; padding: 12rpx 0; font-size: 26rpx; color: #666; }
.fee-total { display: flex; justify-content: space-between; padding-top: 16rpx; margin-top: 8rpx; border-top: 1rpx solid #eee; font-size: 28rpx; font-weight: bold; color: #333; }
.total-price { color: #ff6b35; }
.order-info {}
.info-row { display: flex; padding: 12rpx 0; border-bottom: 1rpx solid #f5f5f5; }
.info-key { font-size: 26rpx; color: #999; width: 160rpx; flex-shrink: 0; }
.info-val { font-size: 26rpx; color: #333; flex: 1; }
.action-bar { padding: 20rpx 30rpx; display: flex; gap: 20rpx; justify-content: flex-end; }
.btn-primary { background: #ff6b35; color: #fff; font-size: 28rpx; padding: 16rpx 48rpx; border-radius: 40rpx; border: none; }
.btn-secondary { background: transparent; color: #666; font-size: 28rpx; padding: 16rpx 48rpx; border-radius: 40rpx; border: 2rpx solid #ddd; }
</style>
