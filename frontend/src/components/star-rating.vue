<template>
  <view class="stars">
    <text v-for="i in 5" :key="i" class="star" :class="{ active: i <= modelValue }" @click="readonly || $emit('update:modelValue', i)">★</text>
  </view>
</template>
<script setup>
defineProps({ modelValue: { type: Number, default: 5 }, readonly: { type: Boolean, default: false } })
defineEmits(['update:modelValue'])
</script>
<style scoped>
.stars { display:flex; gap:8rpx; }
.star { font-size:48rpx; color:#ddd; }
.star.active { color:#ff9500; }
</style>
