import { get, post } from './request'

export const riderApi = {
  register: (data) => post('/api/app/rider/register', data),
  getById: (id) => get(`/api/app/rider/${id}`),
  getMyInfo: () => get('/api/app/rider/my-info'),
  getList: (params) => get('/api/app/rider', params),
  approve: (id) => post(`/api/app/rider/${id}/approve`),
  reject: (id, reason) => post(`/api/app/rider/${id}/reject`, { reason }),
  updateLocation: (lat, lng) => post('/api/app/rider/update-location', { lat, lng }),
  goOnline: () => post('/api/app/rider/go-online'),
  goOffline: () => post('/api/app/rider/go-offline')
}
