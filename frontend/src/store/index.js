import { createPinia } from 'pinia'

export const pinia = createPinia()
export { useUserStore } from './user'
export { useCartStore } from './cart'
export { useLocationStore } from './location'
export { useAppStore } from './app'
