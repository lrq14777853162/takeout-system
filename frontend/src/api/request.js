// API请求封装 - 自动注入Token，处理401跳转，统一错误提示
const BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000'

/**
 * 封装uni.request，支持Promise、Token注入、错误处理
 */
export const request = (options) => {
  return new Promise((resolve, reject) => {
    const token = uni.getStorageSync('access_token')
    const header = {
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...options.header
    }

    uni.request({
      url: `${BASE_URL}${options.url}`,
      method: options.method || 'GET',
      data: options.data,
      header,
      success: (res) => {
        if (res.statusCode === 401) {
          uni.removeStorageSync('access_token')
          uni.reLaunch({ url: '/pages/common/login/index' })
          reject(new Error('未授权，请重新登录'))
          return
        }
        if (res.statusCode === 403) {
          uni.showToast({ title: '权限不足', icon: 'none' })
          reject(new Error('权限不足'))
          return
        }
        if (res.statusCode >= 400) {
          const errMsg = res.data?.error?.message || res.data?.message || '请求失败'
          uni.showToast({ title: errMsg, icon: 'none' })
          reject(new Error(errMsg))
          return
        }
        resolve(res.data)
      },
      fail: (err) => {
        uni.showToast({ title: '网络错误，请重试', icon: 'none' })
        reject(err)
      }
    })
  })
}

export const get = (url, data) => request({ url, method: 'GET', data })
export const post = (url, data) => request({ url, method: 'POST', data })
export const put = (url, data) => request({ url, method: 'PUT', data })
export const del = (url) => request({ url, method: 'DELETE' })
