import { get, post, put, del } from './request'

export const productApi = {
  getList: (params) => get('/api/app/product', params),
  getById: (id) => get(`/api/app/product/${id}`),
  getMerchantProducts: (merchantId) => get(`/api/app/product/merchant-products/${merchantId}`),
  create: (data) => post('/api/app/product', data),
  update: (id, data) => put(`/api/app/product/${id}`, data),
  delete: (id) => del(`/api/app/product/${id}`),
  setAvailable: (id, available) => post(`/api/app/product/${id}/set-available`, { available }),
  getCategories: (merchantId) => get('/api/app/product-category', { merchantId })
}
