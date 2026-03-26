<template>
  <view class="container">
    <!-- 搜索栏 -->
    <view class="search-bar" @click="goSearch">
      <text class="search-icon">🔍</text>
      <text class="search-placeholder">搜索商家、菜品</text>
    </view>

    <!-- 分类导航 -->
    <scroll-view class="category-scroll" scroll-x>
      <view class="category-list">
        <view
          v-for="cat in categories"
          :key="cat.id"
          class="category-item"
          :class="{ active: activeCat === cat.id }"
          @click="selectCategory(cat.id)"
        >
          <text class="cat-icon">{{ cat.icon }}</text>
          <text class="cat-name">{{ cat.name }}</text>
        </view>
      </view>
    </scroll-view>

    <!-- 商家列表 -->
    <view class="merchant-list">
      <view v-if="loading" class="skeleton-list">
        <view v-for="i in 4" :key="i" class="skeleton-card">
          <view class="skeleton-img"></view>
          <view class="skeleton-content">
            <view class="skeleton-line w80"></view>
            <view class="skeleton-line w60"></view>
            <view class="skeleton-line w40"></view>
          </view>
        </view>
      </view>

      <view
        v-else
        v-for="m in merchants"
        :key="m.id"
        class="merchant-card"
        @click="goMerchant(m.id)"
      >
        <image class="merchant-img" :src="m.logoUrl || '/static/default-shop.png'" mode="aspectFill" />
        <view class="merchant-info">
          <text class="merchant-name">{{ m.name }}</text>
          <view class="merchant-meta">
            <text class="rating">⭐ {{ m.rating?.toFixed(1) || '5.0' }}</text>
            <text class="separator">·</text>
            <text class="delivery-time">{{ m.estimatedDeliveryTime || 30 }}分钟</text>
            <text class="separator">·</text>
            <text class="delivery-fee">配送费¥{{ m.deliveryFee?.toFixed(0) || 0 }}</text>
          </view>
          <text class="merchant-desc">{{ m.description || '新鲜食材，用心烹制' }}</text>
          <view class="merchant-tags">
            <text v-if="m.minOrderAmount" class="tag">满{{ m.minOrderAmount }}起送</text>
            <text class="tag tag-new">品质保证</text>
          </view>
        </view>
      </view>

      <view v-if="!loading && merchants.length === 0" class="empty">
        <text class="empty-text">附近暂无商家</text>
      </view>
    </view>
  </view>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { merchantApi } from '../../../api/merchant'
import { useLocationStore } from '../../../store/location'

const locationStore = useLocationStore()
const loading = ref(true)
const merchants = ref([])
const activeCat = ref(0)

const categories = ref([
  { id: 0, name: '全部', icon: '🍽️' },
  { id: 1, name: '快餐', icon: '🍔' },
  { id: 2, name: '中餐', icon: '🥢' },
  { id: 3, name: '西餐', icon: '🍝' },
  { id: 4, name: '甜品', icon: '🍰' },
  { id: 5, name: '饮品', icon: '🧋' },
  { id: 6, name: '烧烤', icon: '🔥' }
])

async function loadMerchants() {
  loading.value = true
  try {
    const params = {
      maxResultCount: 20,
      skipCount: 0,
      status: 1
    }
    if (activeCat.value > 0) params.categoryId = activeCat.value
    if (locationStore.latitude) {
      params.latitude = locationStore.latitude
      params.longitude = locationStore.longitude
    }
    const res = await merchantApi.getList(params)
    merchants.value = res.items || []
  } catch (e) {
    merchants.value = []
  } finally {
    loading.value = false
  }
}

function selectCategory(id) {
  activeCat.value = id
  loadMerchants()
}

function goSearch() {
  uni.navigateTo({ url: '/pages/customer/search/index' })
}

function goMerchant(id) {
  uni.navigateTo({ url: `/pages/customer/merchant-detail/index?id=${id}` })
}

onMounted(async () => {
  try {
    await locationStore.locate()
  } catch (e) {
    // proceed without location
  }
  loadMerchants()
})
</script>

<style scoped>
.container {
  background: #f5f5f5;
  min-height: 100vh;
}
.search-bar {
  display: flex;
  align-items: center;
  background: rgba(255,255,255,0.25);
  margin: 20rpx 24rpx 0;
  padding: 18rpx 28rpx;
  border-radius: 50rpx;
  border: 2rpx solid rgba(255,255,255,0.5);
}
.search-icon { font-size: 30rpx; margin-right: 12rpx; }
.search-placeholder { color: rgba(255,255,255,0.85); font-size: 28rpx; }
.category-scroll {
  background: #fff;
  margin-top: 20rpx;
  white-space: nowrap;
}
.category-list {
  display: flex;
  padding: 16rpx 20rpx;
}
.category-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 12rpx 20rpx;
  margin-right: 8rpx;
  border-radius: 50rpx;
  min-width: 100rpx;
}
.category-item.active {
  background: #fff3ee;
}
.cat-icon { font-size: 36rpx; }
.cat-name { font-size: 22rpx; color: #666; margin-top: 4rpx; }
.category-item.active .cat-name { color: #ff6b35; font-weight: bold; }
.merchant-list { padding: 16rpx; }
.merchant-card {
  display: flex;
  background: #fff;
  border-radius: 16rpx;
  margin-bottom: 16rpx;
  overflow: hidden;
  box-shadow: 0 2rpx 12rpx rgba(0,0,0,0.06);
}
.merchant-img { width: 180rpx; height: 180rpx; flex-shrink: 0; }
.merchant-info { flex: 1; padding: 20rpx; overflow: hidden; }
.merchant-name { font-size: 30rpx; font-weight: bold; color: #333; }
.merchant-meta { display: flex; align-items: center; margin: 8rpx 0; flex-wrap: wrap; }
.rating { font-size: 24rpx; color: #ff9500; }
.separator { color: #ccc; margin: 0 8rpx; font-size: 22rpx; }
.delivery-time, .delivery-fee { font-size: 24rpx; color: #666; }
.merchant-desc { font-size: 24rpx; color: #999; margin-bottom: 8rpx; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }
.merchant-tags { display: flex; flex-wrap: wrap; gap: 8rpx; }
.tag { font-size: 20rpx; color: #ff6b35; background: #fff3ee; padding: 4rpx 12rpx; border-radius: 20rpx; }
.tag-new { color: #4cd964; background: #f0fff4; }
.skeleton-card { display: flex; background: #fff; border-radius: 16rpx; margin-bottom: 16rpx; overflow: hidden; padding: 20rpx; }
.skeleton-img { width: 160rpx; height: 160rpx; background: #eee; border-radius: 12rpx; flex-shrink: 0; }
.skeleton-content { flex: 1; padding-left: 20rpx; display: flex; flex-direction: column; gap: 16rpx; justify-content: center; }
.skeleton-line { height: 24rpx; background: linear-gradient(90deg, #eee 25%, #f5f5f5 50%, #eee 75%); border-radius: 4rpx; animation: shimmer 1.5s infinite; }
.w80 { width: 80%; }
.w60 { width: 60%; }
.w40 { width: 40%; }
@keyframes shimmer {
  0% { background-position: -200% 0; }
  100% { background-position: 200% 0; }
}
.empty { text-align: center; padding: 80rpx 0; }
.empty-text { color: #999; font-size: 28rpx; }
</style>
