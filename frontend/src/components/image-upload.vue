<template>
  <view class="upload-wrap">
    <view v-for="(img, i) in images" :key="i" class="img-item">
      <image class="preview-img" :src="img" mode="aspectFill" />
      <text class="del-btn" @click="remove(i)">×</text>
    </view>
    <view v-if="images.length < max" class="add-btn" @click="choose">
      <text class="add-icon">+</text>
      <text class="add-text">上传图片</text>
    </view>
  </view>
</template>
<script setup>
import { ref } from 'vue'
const props = defineProps({ max: { type: Number, default: 3 }, modelValue: { type: Array, default: () => [] } })
const emit = defineEmits(['update:modelValue'])
const images = ref([...props.modelValue])
function choose() {
  uni.chooseImage({
    count: props.max - images.value.length,
    success: (res) => {
      images.value.push(...res.tempFilePaths)
      emit('update:modelValue', images.value)
    }
  })
}
function remove(i) {
  images.value.splice(i, 1)
  emit('update:modelValue', images.value)
}
</script>
<style scoped>
.upload-wrap { display:flex; flex-wrap:wrap; gap:16rpx; }
.img-item { position:relative; width:180rpx; height:180rpx; }
.preview-img { width:100%; height:100%; border-radius:12rpx; }
.del-btn { position:absolute; top:-12rpx; right:-12rpx; width:40rpx; height:40rpx; background:#ff3b30; color:#fff; border-radius:50%; text-align:center; line-height:40rpx; font-size:28rpx; }
.add-btn { width:180rpx; height:180rpx; background:#f7f7f7; border-radius:12rpx; border:2rpx dashed #ddd; display:flex; flex-direction:column; align-items:center; justify-content:center; }
.add-icon { font-size:60rpx; color:#ccc; }
.add-text { font-size:24rpx; color:#999; margin-top:8rpx; }
</style>
