import { post, get } from './request'

export const authApi = {
  login: (data) => post('/api/account/login', data),
  wechatLogin: (code) => post('/api/account/wechat-login', { code }),
  refreshToken: (refreshToken) => post('/api/account/refresh-token', { refreshToken }),
  getProfile: () => get('/api/account/my-profile'),
  changePassword: (data) => post('/api/account/change-password', data),
  logout: () => post('/api/account/logout')
}
