<template>
  <view class="container">
    <view class="review-form">
      <view class="section-title"><text>整体评分</text></view>
      <view class="stars">
        <text
          v-for="i in 5"
          :key="i"
          class="star"
          :class="{ active: i <= rating }"
          @click="rating = i"
        >★</text>
      </view>
      <view class="section-title" style="margin-top: 32rpx;"><text>评价内容</text></view>
      <textarea v-model="content" class="review-input" placeholder="分享您的用餐体验..." maxlength="300" />
      <view class="char-count"><text>{{ content.length }}/300</text></view>
      <button class="btn-submit" :disabled="submitting" @click="submitReview">
        {{ submitting ? '提交中...' : '提交评价' }}
      </button>
    </view>
  </view>
</template>

<script setup>
import { ref } from 'vue'
import { post } from '../../../api/request'

const pages = getCurrentPages()
const query = pages[pages.length - 1]?.options || {}

const rating = ref(5)
const content = ref('')
const submitting = ref(false)

async function submitReview() {
  if (!content.value.trim()) return uni.showToast({ title: '请输入评价内容', icon: 'none' })
  submitting.value = true
  try {
    await post('/api/app/review', { orderId: query.orderId, rating: rating.value, content: content.value })
    uni.showToast({ title: '评价成功', icon: 'success' })
    setTimeout(() => uni.navigateBack(), 1500)
  } finally {
    submitting.value = false
  }
}
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; padding: 20rpx; }
.review-form { background: #fff; border-radius: 16rpx; padding: 32rpx; }
.section-title { font-size: 28rpx; font-weight: bold; color: #333; margin-bottom: 16rpx; }
.stars { display: flex; gap: 16rpx; margin-bottom: 8rpx; }
.star { font-size: 60rpx; color: #ddd; }
.star.active { color: #ff9500; }
.review-input { width: 100%; height: 200rpx; background: #f7f7f7; border-radius: 12rpx; padding: 20rpx; font-size: 28rpx; color: #333; box-sizing: border-box; }
.char-count { text-align: right; font-size: 22rpx; color: #999; margin-top: 8rpx; }
.btn-submit { background: #ff6b35; color: #fff; font-size: 30rpx; border-radius: 50rpx; border: none; margin-top: 32rpx; height: 96rpx; }
</style>
