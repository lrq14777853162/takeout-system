import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export const useCartStore = defineStore('cart', () => {
  const stored = uni.getStorageSync('cart_items')
  const items = ref(stored ? JSON.parse(stored) : [])
  const merchantId = ref(uni.getStorageSync('cart_merchant_id') || '')

  const totalCount = computed(() => items.value.reduce((sum, i) => sum + i.quantity, 0))
  const totalPrice = computed(() =>
    items.value.reduce((sum, i) => sum + i.price * i.quantity, 0)
  )

  function save() {
    uni.setStorageSync('cart_items', JSON.stringify(items.value))
    uni.setStorageSync('cart_merchant_id', merchantId.value)
  }

  function addItem(item) {
    if (merchantId.value && merchantId.value !== item.merchantId) {
      uni.showModal({
        title: '提示',
        content: '您的购物车中有其他商家的商品，是否清空后继续？',
        success: (res) => {
          if (res.confirm) {
            items.value = []
            merchantId.value = item.merchantId
            doAddItem(item)
          }
        }
      })
      return
    }
    merchantId.value = item.merchantId
    doAddItem(item)
  }

  function doAddItem(item) {
    const existing = items.value.find((i) => i.productId === item.productId && i.specId === item.specId)
    if (existing) {
      existing.quantity += item.quantity || 1
    } else {
      items.value.push({ ...item, quantity: item.quantity || 1 })
    }
    save()
  }

  function updateQuantity(productId, specId, quantity) {
    const item = items.value.find((i) => i.productId === productId && i.specId === specId)
    if (item) {
      item.quantity = quantity
      if (quantity <= 0) {
        removeItem(productId, specId)
        return
      }
    }
    save()
  }

  function removeItem(productId, specId) {
    items.value = items.value.filter((i) => !(i.productId === productId && i.specId === specId))
    if (items.value.length === 0) merchantId.value = ''
    save()
  }

  function clearCart() {
    items.value = []
    merchantId.value = ''
    save()
  }

  return { items, merchantId, totalCount, totalPrice, addItem, updateQuantity, removeItem, clearCart }
})
