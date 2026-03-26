import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAppStore = defineStore('app', () => {
  const loading = ref(false)
  const networkConnected = ref(true)

  function showLoading(title = '加载中...') {
    loading.value = true
    uni.showLoading({ title, mask: true })
  }

  function hideLoading() {
    loading.value = false
    uni.hideLoading()
  }

  return { loading, networkConnected, showLoading, hideLoading }
})
