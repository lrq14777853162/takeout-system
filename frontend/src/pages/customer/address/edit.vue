<template>
  <view class="container">
    <view class="form">
      <view class="form-item">
        <text class="label">联系人</text>
        <input v-model="form.contactName" class="input" placeholder="请输入联系人姓名" />
      </view>
      <view class="form-item">
        <text class="label">手机号</text>
        <input v-model="form.contactPhone" class="input" placeholder="请输入手机号" type="number" />
      </view>
      <view class="form-item">
        <text class="label">详细地址</text>
        <input v-model="form.address" class="input" placeholder="请输入详细地址" />
      </view>
      <view class="form-item">
        <text class="label">设为默认</text>
        <switch :checked="form.isDefault" @change="e => form.isDefault = e.detail.value" color="#ff6b35" />
      </view>
    </view>
    <button class="btn-save" :disabled="saving" @click="save">{{ saving ? '保存中...' : '保存地址' }}</button>
  </view>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { userApi } from '../../../api/user'

const pages = getCurrentPages()
const query = pages[pages.length - 1]?.options || {}

const saving = ref(false)
const form = ref({ contactName: '', contactPhone: '', address: '', isDefault: false })

async function save() {
  if (!form.value.contactName) return uni.showToast({ title: '请输入联系人', icon: 'none' })
  if (!form.value.contactPhone) return uni.showToast({ title: '请输入手机号', icon: 'none' })
  if (!form.value.address) return uni.showToast({ title: '请输入地址', icon: 'none' })
  saving.value = true
  try {
    if (query.id) {
      await userApi.updateAddress(query.id, form.value)
    } else {
      await userApi.createAddress(form.value)
    }
    uni.showToast({ title: '保存成功', icon: 'success' })
    setTimeout(() => uni.navigateBack(), 1500)
  } finally {
    saving.value = false
  }
}

onMounted(async () => {
  // Could load existing address if query.id provided
})
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; padding: 16rpx; }
.form { background: #fff; border-radius: 16rpx; overflow: hidden; }
.form-item { display: flex; align-items: center; padding: 28rpx 30rpx; border-bottom: 1rpx solid #f5f5f5; }
.label { width: 160rpx; font-size: 28rpx; color: #333; flex-shrink: 0; }
.input { flex: 1; font-size: 28rpx; color: #333; }
.btn-save { background: #ff6b35; color: #fff; font-size: 30rpx; border-radius: 50rpx; border: none; width: 100%; margin-top: 40rpx; height: 96rpx; }
</style>
