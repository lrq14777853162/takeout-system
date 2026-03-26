const TOKEN_KEY = 'access_token'
const REFRESH_TOKEN_KEY = 'refresh_token'
const USER_INFO_KEY = 'user_info'

export const auth = {
  setToken(token, refreshToken) {
    uni.setStorageSync(TOKEN_KEY, token)
    if (refreshToken) {
      uni.setStorageSync(REFRESH_TOKEN_KEY, refreshToken)
    }
  },
  getToken() {
    return uni.getStorageSync(TOKEN_KEY)
  },
  getRefreshToken() {
    return uni.getStorageSync(REFRESH_TOKEN_KEY)
  },
  clearToken() {
    uni.removeStorageSync(TOKEN_KEY)
    uni.removeStorageSync(REFRESH_TOKEN_KEY)
  },
  isLoggedIn() {
    return !!this.getToken()
  },
  setUserInfo(info) {
    uni.setStorageSync(USER_INFO_KEY, JSON.stringify(info))
  },
  getUserInfo() {
    const str = uni.getStorageSync(USER_INFO_KEY)
    return str ? JSON.parse(str) : null
  },
  clearAll() {
    this.clearToken()
    uni.removeStorageSync(USER_INFO_KEY)
  }
}
