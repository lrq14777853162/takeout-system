/**
 * 获取当前位置（Promise封装）
 */
export const getCurrentLocation = () => {
  return new Promise((resolve, reject) => {
    uni.getLocation({
      type: 'gcj02',
      success: (res) => {
        resolve({
          latitude: res.latitude,
          longitude: res.longitude,
          speed: res.speed,
          accuracy: res.accuracy
        })
      },
      fail: (err) => {
        console.error('获取位置失败:', err)
        reject(err)
      }
    })
  })
}

/**
 * 计算两点之间的距离（千米）
 */
export const calcDistance = (lat1, lng1, lat2, lng2) => {
  const R = 6371
  const dLat = ((lat2 - lat1) * Math.PI) / 180
  const dLng = ((lng2 - lng1) * Math.PI) / 180
  const a =
    Math.sin(dLat / 2) * Math.sin(dLat / 2) +
    Math.cos((lat1 * Math.PI) / 180) *
      Math.cos((lat2 * Math.PI) / 180) *
      Math.sin(dLng / 2) *
      Math.sin(dLng / 2)
  const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a))
  return R * c
}
