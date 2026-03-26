<template>
  <view class="product-item">
    <image class="prod-img" :src="product.imageUrl || '/static/default-food.png'" mode="aspectFill" />
    <view class="prod-info">
      <text class="prod-name">{{ product.name }}</text>
      <text class="prod-desc">{{ product.description }}</text>
      <view class="prod-footer">
        <text class="prod-price">¥{{ product.price?.toFixed(2) }}</text>
        <text class="add-btn" @click="$emit('add', product)">＋</text>
      </view>
    </view>
  </view>
</template>
<script setup>
defineProps({ product: { type: Object, required: true } })
defineEmits(['add'])
</script>
<style scoped>
.product-item { display:flex; padding:20rpx; border-bottom:1rpx solid #f5f5f5; background:#fff; }
.prod-img { width:140rpx; height:140rpx; border-radius:12rpx; flex-shrink:0; }
.prod-info { flex:1; padding-left:16rpx; display:flex; flex-direction:column; justify-content:space-between; }
.prod-name { font-size:28rpx; color:#333; font-weight:500; }
.prod-desc { font-size:22rpx; color:#999; }
.prod-footer { display:flex; justify-content:space-between; align-items:center; }
.prod-price { font-size:30rpx; color:#ff6b35; font-weight:bold; }
.add-btn { width:56rpx; height:56rpx; background:#ff6b35; color:#fff; border-radius:50%; text-align:center; line-height:56rpx; font-size:36rpx; }
</style>
