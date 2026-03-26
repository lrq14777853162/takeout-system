<template>
  <view class="container">
    <view class="search-bar">
      <input
        v-model="keyword"
        class="search-input"
        placeholder="搜索商家、菜品"
        confirm-type="search"
        :focus="true"
        @confirm="doSearch"
      />
      <text class="search-btn" @click="doSearch">搜索</text>
    </view>

    <view v-if="results.length > 0" class="results">
      <view
        v-for="m in results"
        :key="m.id"
        class="result-item"
        @click="uni.navigateTo({ url: `/pages/customer/merchant-detail/index?id=${m.id}` })"
      >
        <image class="result-img" :src="m.logoUrl || '/static/default-shop.png'" mode="aspectFill" />
        <view class="result-info">
          <text class="result-name">{{ m.name }}</text>
          <text class="result-meta">⭐{{ m.rating?.toFixed(1) }} · {{ m.estimatedDeliveryTime }}分钟</text>
        </view>
      </view>
    </view>

    <view v-else-if="searched && keyword" class="empty">
      <text class="empty-text">未找到相关商家或菜品</text>
    </view>
  </view>
</template>

<script setup>
import { ref } from 'vue'
import { merchantApi } from '../../../api/merchant'

const keyword = ref('')
const results = ref([])
const searched = ref(false)

async function doSearch() {
  if (!keyword.value.trim()) return
  try {
    const res = await merchantApi.getList({ keyword: keyword.value, status: 1 })
    results.value = res.items || []
    searched.value = true
  } catch (e) {
    results.value = []
  }
}
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; }
.search-bar { display: flex; align-items: center; background: #fff; padding: 16rpx 24rpx; gap: 16rpx; }
.search-input { flex: 1; height: 72rpx; background: #f5f5f5; border-radius: 36rpx; padding: 0 24rpx; font-size: 28rpx; }
.search-btn { color: #ff6b35; font-size: 28rpx; font-weight: bold; padding: 10rpx; }
.results { padding: 16rpx; }
.result-item { display: flex; background: #fff; border-radius: 16rpx; padding: 20rpx; margin-bottom: 16rpx; }
.result-img { width: 100rpx; height: 100rpx; border-radius: 12rpx; flex-shrink: 0; }
.result-info { flex: 1; padding-left: 16rpx; display: flex; flex-direction: column; justify-content: center; }
.result-name { font-size: 30rpx; color: #333; font-weight: bold; }
.result-meta { font-size: 24rpx; color: #999; margin-top: 8rpx; }
.empty { text-align: center; padding: 100rpx 0; }
.empty-text { font-size: 28rpx; color: #999; }
</style>
