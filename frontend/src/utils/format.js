import dayjs from 'dayjs'

export const formatPrice = (price) => {
  if (price === null || price === undefined) return '¥0.00'
  return `¥${Number(price).toFixed(2)}`
}

export const formatDate = (date, format = 'YYYY-MM-DD HH:mm') => {
  if (!date) return ''
  return dayjs(date).format(format)
}

export const formatRelativeTime = (date) => {
  if (!date) return ''
  const now = dayjs()
  const target = dayjs(date)
  const diff = now.diff(target, 'minute')

  if (diff < 1) return '刚刚'
  if (diff < 60) return `${diff}分钟前`
  if (diff < 1440) return `${Math.floor(diff / 60)}小时前`
  return target.format('MM-DD HH:mm')
}

export const formatOrderStatus = (status) => {
  const map = {
    0: '待支付',
    1: '已支付',
    2: '商家已接单',
    3: '待取餐',
    4: '骑手取餐中',
    5: '配送中',
    6: '已送达',
    7: '已完成',
    8: '已取消',
    9: '退款中',
    10: '已退款'
  }
  return map[status] ?? '未知'
}

export const formatDistance = (km) => {
  if (km < 1) return `${Math.round(km * 1000)}m`
  return `${km.toFixed(1)}km`
}
