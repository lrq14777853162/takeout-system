import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useLocationStore = defineStore('location', () => {
  const latitude = ref(0)
  const longitude = ref(0)
  const city = ref('')
  const address = ref('')
  const isLocated = ref(false)

  async function locate() {
    return new Promise((resolve, reject) => {
      uni.getLocation({
        type: 'gcj02',
        success: (res) => {
          latitude.value = res.latitude
          longitude.value = res.longitude
          isLocated.value = true
          uni.setStorageSync('location', JSON.stringify({
            latitude: res.latitude,
            longitude: res.longitude
          }))
          resolve(res)
        },
        fail: (err) => {
          const cached = uni.getStorageSync('location')
          if (cached) {
            const loc = JSON.parse(cached)
            latitude.value = loc.latitude
            longitude.value = loc.longitude
          }
          reject(err)
        }
      })
    })
  }

  function setCity(c) {
    city.value = c
  }

  return { latitude, longitude, city, address, isLocated, locate, setCity }
})
