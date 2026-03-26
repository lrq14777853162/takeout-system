import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { auth } from '../utils/auth'

export const useUserStore = defineStore('user', () => {
  const token = ref(auth.getToken() || '')
  const userInfo = ref(auth.getUserInfo() || null)
  const role = ref('')

  const isLoggedIn = computed(() => !!token.value)
  const userName = computed(() => userInfo.value?.name || userInfo.value?.userName || '')

  function setToken(t, refreshToken) {
    token.value = t
    auth.setToken(t, refreshToken)
  }

  function setUserInfo(info) {
    userInfo.value = info
    auth.setUserInfo(info)
    if (info?.roles?.includes('admin')) role.value = 'admin'
    else if (info?.roles?.includes('merchant')) role.value = 'merchant'
    else if (info?.roles?.includes('rider')) role.value = 'rider'
    else role.value = 'customer'
  }

  function logout() {
    token.value = ''
    userInfo.value = null
    role.value = ''
    auth.clearAll()
  }

  return { token, userInfo, role, isLoggedIn, userName, setToken, setUserInfo, logout }
})
