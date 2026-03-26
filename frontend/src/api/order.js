import { get, post } from './request'

export const orderApi = {
  create: (data) => post('/api/app/order', data),
  getById: (id) => get(`/api/app/order/${id}`),
  getList: (params) => get('/api/app/order', params),
  accept: (id) => post(`/api/app/order/${id}/accept`),
  assignRider: (id, riderId) => post(`/api/app/order/${id}/assign-rider`, { riderId }),
  pickUp: (id) => post(`/api/app/order/${id}/pick-up`),
  deliver: (id) => post(`/api/app/order/${id}/deliver`),
  complete: (id) => post(`/api/app/order/${id}/complete`),
  cancel: (id, reason) => post(`/api/app/order/${id}/cancel`, { reason }),
  getMerchantOrders: (merchantId, params) => get(`/api/app/order/merchant-orders/${merchantId}`, params),
  getRiderOrders: (riderId, params) => get(`/api/app/order/rider-orders/${riderId}`, params)
}
