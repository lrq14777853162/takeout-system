import { get, post } from './request'

export const adminApi = {
  getDashboard: () => get('/api/app/admin/dashboard'),
  getPendingMerchants: (params) => get('/api/app/merchant', { ...params, status: 0 }),
  getPendingRiders: (params) => get('/api/app/rider', { ...params, status: 0 }),
  getUsers: (params) => get('/api/identity/users', params),
  getFinanceStats: (params) => get('/api/app/admin/finance', params),
  getRefunds: (params) => get('/api/app/payment/refunds', params),
  processRefund: (id, approve) => post(`/api/app/payment/refund/${id}/process`, { approve })
}
