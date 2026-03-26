<template>
  <view class="container">
    <!-- 状态标签栏 -->
    <scroll-view class="tab-bar" scroll-x>
      <view class="tabs">
        <view
          v-for="tab in tabs"
          :key="tab.value"
          class="tab"
          :class="{ active: activeTab === tab.value }"
          @click="switchTab(tab.value)"
        >
          <text>{{ tab.label }}</text>
          <view v-if="activeTab === tab.value" class="tab-line"></view>
        </view>
      </view>
    </scroll-view>

    <scroll-view
      class="order-scroll"
      scroll-y
      @scrolltolower="loadMore"
      refresher-enabled
      :refresher-triggered="refreshing"
      @refresherrefresh="onRefresh"
    >
      <view v-if="loading && orders.length === 0" class="loading-wrap">
        <text class="loading-text">加载中...</text>
      </view>

      <view v-else-if="orders.length === 0" class="empty-wrap">
        <text class="empty-icon">📦</text>
        <text class="empty-text">暂无订单</text>
      </view>

      <view v-else class="order-list">
        <view
          v-for="order in orders"
          :key="order.id"
          class="order-card"
          @click="goDetail(order.id)"
        >
          <view class="order-header">
            <text class="shop-name">{{ order.merchantName }}</text>
            <text class="order-status" :class="statusClass(order.status)">{{ formatStatus(order.status) }}</text>
          </view>
          <view class="order-items">
            <text class="item-names">{{ getItemNames(order) }}</text>
            <text class="item-count">共{{ order.totalItemCount || 1 }}件</text>
          </view>
          <view class="order-footer">
            <text class="order-time">{{ formatTime(order.creationTime) }}</text>
            <text class="order-price">合计 <text class="price-num">¥{{ order.totalAmount?.toFixed(2) }}</text></text>
          </view>
          <view class="order-actions">
            <button
              v-if="order.status === 0"
              class="btn-action btn-pay"
              @click.stop="payOrder(order.id)"
            >去支付</button>
            <button
              v-if="order.status === 7"
              class="btn-action btn-review"
              @click.stop="goReview(order.id)"
            >评价</button>
            <button
              v-if="[0,1].includes(order.status)"
              class="btn-action btn-cancel"
              @click.stop="cancelOrder(order.id)"
            >取消订单</button>
          </view>
        </view>
      </view>

      <view v-if="noMore" class="no-more"><text>没有更多了</text></view>
    </scroll-view>
  </view>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { orderApi } from '../../../api/order'
import { formatOrderStatus, formatDate } from '../../../utils/format'

const tabs = [
  { label: '全部', value: -1 },
  { label: '待支付', value: 0 },
  { label: '进行中', value: 2 },
  { label: '已完成', value: 7 },
  { label: '已取消', value: 8 }
]

const activeTab = ref(-1)
const orders = ref([])
const loading = ref(false)
const refreshing = ref(false)
const noMore = ref(false)
const skipCount = ref(0)
const pageSize = 10

function formatStatus(s) { return formatOrderStatus(s) }
function formatTime(t) { return formatDate(t, 'MM-DD HH:mm') }

function statusClass(s) {
  if ([0].includes(s)) return 'status-pending'
  if ([1,2,3,4,5,6].includes(s)) return 'status-active'
  if ([7].includes(s)) return 'status-done'
  return 'status-cancel'
}

function getItemNames(order) {
  if (!order.items?.length) return '订单商品'
  return order.items.slice(0, 2).map(i => i.productName).join('、') + (order.items.length > 2 ? '...' : '')
}

async function loadOrders(reset = false) {
  if (loading.value) return
  loading.value = true
  if (reset) {
    skipCount.value = 0
    noMore.value = false
  }
  try {
    const params = { skipCount: skipCount.value, maxResultCount: pageSize }
    if (activeTab.value >= 0) params.status = activeTab.value
    const res = await orderApi.getList(params)
    const list = res.items || []
    if (reset) {
      orders.value = list
    } else {
      orders.value.push(...list)
    }
    skipCount.value += list.length
    noMore.value = list.length < pageSize
  } finally {
    loading.value = false
    refreshing.value = false
  }
}

function switchTab(val) {
  activeTab.value = val
  loadOrders(true)
}

function loadMore() {
  if (!noMore.value) loadOrders()
}

function onRefresh() {
  refreshing.value = true
  loadOrders(true)
}

function goDetail(id) {
  uni.navigateTo({ url: `/pages/customer/order-detail/index?id=${id}` })
}

function goReview(id) {
  uni.navigateTo({ url: `/pages/customer/review/index?orderId=${id}` })
}

async function cancelOrder(id) {
  const { confirm } = await new Promise(r => uni.showModal({ title: '提示', content: '确认取消订单？', success: r }))
  if (!confirm) return
  await orderApi.cancel(id, '用户主动取消')
  loadOrders(true)
}

function payOrder(id) {
  uni.showToast({ title: '跳转支付...', icon: 'none' })
}

onMounted(() => loadOrders(true))
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; display: flex; flex-direction: column; }
.tab-bar { background: #fff; white-space: nowrap; }
.tabs { display: flex; }
.tab { flex: 1; min-width: 140rpx; padding: 28rpx 0 22rpx; text-align: center; font-size: 28rpx; color: #666; position: relative; }
.tab.active { color: #ff6b35; font-weight: bold; }
.tab-line { position: absolute; bottom: 0; left: 50%; transform: translateX(-50%); width: 48rpx; height: 6rpx; background: #ff6b35; border-radius: 3rpx; }
.order-scroll { flex: 1; }
.order-list { padding: 16rpx; }
.order-card { background: #fff; border-radius: 16rpx; margin-bottom: 16rpx; overflow: hidden; }
.order-header { display: flex; justify-content: space-between; align-items: center; padding: 24rpx 24rpx 16rpx; border-bottom: 1rpx solid #f5f5f5; }
.shop-name { font-size: 30rpx; font-weight: bold; color: #333; }
.order-status { font-size: 26rpx; }
.status-pending { color: #ff6b35; }
.status-active { color: #007aff; }
.status-done { color: #4cd964; }
.status-cancel { color: #999; }
.order-items { display: flex; justify-content: space-between; align-items: center; padding: 16rpx 24rpx; }
.item-names { font-size: 26rpx; color: #666; flex: 1; margin-right: 20rpx; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }
.item-count { font-size: 24rpx; color: #999; }
.order-footer { display: flex; justify-content: space-between; align-items: center; padding: 0 24rpx 16rpx; }
.order-time { font-size: 24rpx; color: #999; }
.order-price { font-size: 26rpx; color: #666; }
.price-num { color: #ff6b35; font-weight: bold; font-size: 30rpx; }
.order-actions { display: flex; justify-content: flex-end; gap: 16rpx; padding: 16rpx 24rpx; border-top: 1rpx solid #f5f5f5; }
.btn-action { font-size: 26rpx; padding: 12rpx 32rpx; border-radius: 40rpx; background: transparent; margin: 0; }
.btn-pay { color: #fff; background: #ff6b35; }
.btn-review { color: #ff6b35; border: 2rpx solid #ff6b35; }
.btn-cancel { color: #666; border: 2rpx solid #ddd; }
.empty-wrap { display: flex; flex-direction: column; align-items: center; padding: 120rpx 0; }
.empty-icon { font-size: 100rpx; }
.empty-text { font-size: 28rpx; color: #999; margin-top: 20rpx; }
.loading-wrap { text-align: center; padding: 60rpx; }
.loading-text { color: #999; font-size: 28rpx; }
.no-more { text-align: center; padding: 30rpx; font-size: 24rpx; color: #ccc; }
</style>
