import { get, post, put, del } from './request'

export const userApi = {
  getAddresses: () => get('/api/app/user-address/my-addresses'),
  createAddress: (data) => post('/api/app/user-address', data),
  updateAddress: (id, data) => put(`/api/app/user-address/${id}`, data),
  deleteAddress: (id) => del(`/api/app/user-address/${id}`),
  setDefaultAddress: (id) => post(`/api/app/user-address/${id}/set-default`),
  getCoupons: () => get('/api/app/coupon/my-coupons'),
  claimCoupon: (templateId) => post('/api/app/coupon/claim', { couponTemplateId: templateId })
}
