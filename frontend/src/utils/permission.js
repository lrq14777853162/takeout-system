import { auth } from './auth'

const authPages = [
  '/pages/customer/order-list/index',
  '/pages/customer/submit-order/index',
  '/pages/merchant/dashboard/index',
  '/pages/rider/home/index',
  '/pages/admin/dashboard/index'
]

export const setupPermissionGuard = () => {
  const originalNavigateTo = uni.navigateTo
  const originalReLaunch = uni.reLaunch
  const originalSwitchTab = uni.switchTab

  const checkAuth = (url) => {
    const path = url.split('?')[0]
    if (authPages.some((p) => path.includes(p)) && !auth.isLoggedIn()) {
      uni.reLaunch({ url: '/pages/common/login/index' })
      return false
    }
    return true
  }

  uni.navigateTo = function (options) {
    if (checkAuth(options.url)) {
      originalNavigateTo.call(this, options)
    }
  }

  uni.reLaunch = function (options) {
    originalReLaunch.call(this, options)
  }

  uni.switchTab = function (options) {
    if (checkAuth(options.url)) {
      originalSwitchTab.call(this, options)
    }
  }
}
