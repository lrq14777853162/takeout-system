<template>
  <view class="container">
    <view v-if="addresses.length === 0" class="empty">
      <text class="empty-text">暂无收货地址</text>
    </view>
    <view v-else class="addr-list">
      <view v-for="addr in addresses" :key="addr.id" class="addr-card" @click="selectAddr(addr)">
        <view class="addr-main">
          <text class="addr-name">{{ addr.contactName }} {{ addr.contactPhone }}</text>
          <view v-if="addr.isDefault" class="default-tag"><text>默认</text></view>
        </view>
        <text class="addr-detail">{{ addr.address }}</text>
        <view class="addr-actions">
          <text class="action-edit" @click.stop="editAddr(addr.id)">编辑</text>
          <text class="action-delete" @click.stop="deleteAddr(addr.id)">删除</text>
        </view>
      </view>
    </view>
    <view class="add-btn-wrap">
      <button class="btn-add" @click="editAddr()">+ 添加新地址</button>
    </view>
  </view>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { userApi } from '../../../api/user'

const pages = getCurrentPages()
const query = pages[pages.length - 1]?.options || {}
const isSelectMode = query.mode === 'select'

const addresses = ref([])

async function loadAddresses() {
  try {
    const res = await userApi.getAddresses()
    addresses.value = res.items || res || []
  } catch (e) { addresses.value = [] }
}

function selectAddr(addr) {
  if (isSelectMode) {
    const pages = getCurrentPages()
    const prevPage = pages[pages.length - 2]
    if (prevPage) prevPage.$vm.selectedAddress = addr
    uni.navigateBack()
  }
}

function editAddr(id) {
  uni.navigateTo({ url: `/pages/customer/address/edit?id=${id || ''}` })
}

async function deleteAddr(id) {
  const { confirm } = await new Promise(r => uni.showModal({ title: '提示', content: '确认删除该地址？', success: r }))
  if (!confirm) return
  await userApi.deleteAddress(id)
  loadAddresses()
}

onMounted(loadAddresses)
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; padding-bottom: 120rpx; }
.addr-list { padding: 16rpx; }
.addr-card { background: #fff; border-radius: 16rpx; padding: 24rpx; margin-bottom: 16rpx; }
.addr-main { display: flex; align-items: center; margin-bottom: 8rpx; }
.addr-name { font-size: 28rpx; font-weight: bold; color: #333; }
.default-tag { background: #ff6b35; color: #fff; font-size: 20rpx; padding: 4rpx 12rpx; border-radius: 20rpx; margin-left: 12rpx; }
.addr-detail { font-size: 26rpx; color: #666; display: block; margin-bottom: 16rpx; }
.addr-actions { display: flex; gap: 24rpx; }
.action-edit { font-size: 26rpx; color: #007aff; }
.action-delete { font-size: 26rpx; color: #ff3b30; }
.empty { text-align: center; padding: 100rpx 0; }
.empty-text { font-size: 28rpx; color: #999; }
.add-btn-wrap { position: fixed; bottom: 0; left: 0; right: 0; padding: 20rpx; background: #fff; }
.btn-add { background: #ff6b35; color: #fff; font-size: 30rpx; border-radius: 50rpx; border: none; width: 100%; }
</style>
