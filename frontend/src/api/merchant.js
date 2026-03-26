import { get, post, put, del } from './request'

export const merchantApi = {
  getList: (params) => get('/api/app/merchant', params),
  getById: (id) => get(`/api/app/merchant/${id}`),
  create: (data) => post('/api/app/merchant', data),
  update: (id, data) => put(`/api/app/merchant/${id}`, data),
  delete: (id) => del(`/api/app/merchant/${id}`),
  approve: (id) => post(`/api/app/merchant/${id}/approve`),
  reject: (id, reason) => post(`/api/app/merchant/${id}/reject`, { reason }),
  getMyMerchant: () => get('/api/app/merchant/my-merchant'),
  getCategories: () => get('/api/app/merchant-category')
}
