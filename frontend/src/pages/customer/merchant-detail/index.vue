<template>
  <view class="container">
    <!-- 商家头图 -->
    <view class="hero">
      <image class="hero-img" :src="merchant.logoUrl || '/static/default-shop.png'" mode="aspectFill" />
      <view class="hero-overlay">
        <text class="merchant-name">{{ merchant.name }}</text>
        <view class="merchant-meta">
          <text class="rating">⭐ {{ merchant.rating?.toFixed(1) || '5.0' }}</text>
          <text class="sep">·</text>
          <text>{{ merchant.monthSales || 0 }}单/月</text>
          <text class="sep">·</text>
          <text>{{ merchant.estimatedDeliveryTime || 30 }}分钟</text>
        </view>
      </view>
    </view>

    <!-- 分类 + 商品 -->
    <view class="content">
      <view class="category-sidebar">
        <view
          v-for="cat in categories"
          :key="cat.id"
          class="cat-item"
          :class="{ active: activeCatId === cat.id }"
          @click="activeCatId = cat.id"
        >
          <text>{{ cat.name }}</text>
        </view>
      </view>
      <scroll-view class="product-list" scroll-y>
        <view v-for="product in filteredProducts" :key="product.id" class="product-item">
          <image class="prod-img" :src="product.imageUrl || '/static/default-food.png'" mode="aspectFill" />
          <view class="prod-info">
            <text class="prod-name">{{ product.name }}</text>
            <text class="prod-desc">{{ product.description }}</text>
            <view class="prod-footer">
              <text class="prod-price">¥{{ product.price?.toFixed(2) }}</text>
              <view class="quantity-ctrl">
                <text class="qty-btn" @click="decrease(product)">－</text>
                <text class="qty-num">{{ getQty(product) }}</text>
                <text class="qty-btn qty-add" @click="addToCart(product)">＋</text>
              </view>
            </view>
          </view>
        </view>
      </scroll-view>
    </view>

    <!-- 购物车栏 -->
    <view v-if="cartStore.totalCount > 0" class="cart-bar" @click="goCart">
      <view class="cart-icon-wrap">
        <text class="cart-icon">🛒</text>
        <view class="cart-badge">{{ cartStore.totalCount }}</view>
      </view>
      <text class="cart-total">¥{{ cartStore.totalPrice.toFixed(2) }}</text>
      <text class="cart-btn">去结算</text>
    </view>
  </view>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { merchantApi } from '../../../api/merchant'
import { productApi } from '../../../api/product'
import { useCartStore } from '../../../store/cart'

const cartStore = useCartStore()
const merchant = ref({})
const categories = ref([])
const products = ref([])
const activeCatId = ref(null)

const pages = getCurrentPages()
const query = pages[pages.length - 1]?.options || {}
const merchantId = query.id

const filteredProducts = computed(() =>
  activeCatId.value
    ? products.value.filter(p => p.categoryId === activeCatId.value)
    : products.value
)

function getQty(product) {
  const item = cartStore.items.find(i => i.productId === product.id)
  return item?.quantity || 0
}

function addToCart(product) {
  cartStore.addItem({
    productId: product.id,
    productName: product.name,
    price: product.price,
    imageUrl: product.imageUrl,
    merchantId: merchantId,
    specId: null
  })
}

function decrease(product) {
  const qty = getQty(product)
  if (qty > 0) cartStore.updateQuantity(product.id, null, qty - 1)
}

function goCart() {
  uni.navigateTo({ url: '/pages/customer/cart/index' })
}

onMounted(async () => {
  if (!merchantId) return
  try {
    merchant.value = await merchantApi.getById(merchantId)
    const res = await productApi.getMerchantProducts(merchantId)
    products.value = res.items || res || []
    const catRes = await productApi.getCategories(merchantId)
    categories.value = [{ id: null, name: '全部' }, ...(catRes.items || catRes || [])]
  } catch (e) {
    // silent
  }
})
</script>

<style scoped>
.container { background: #f5f5f5; min-height: 100vh; display: flex; flex-direction: column; }
.hero { position: relative; height: 360rpx; }
.hero-img { width: 100%; height: 100%; }
.hero-overlay { position: absolute; bottom: 0; left: 0; right: 0; padding: 30rpx; background: linear-gradient(transparent, rgba(0,0,0,0.6)); }
.merchant-name { font-size: 36rpx; font-weight: bold; color: #fff; display: block; }
.merchant-meta { display: flex; align-items: center; margin-top: 8rpx; }
.merchant-meta text { font-size: 24rpx; color: rgba(255,255,255,0.9); }
.sep { margin: 0 10rpx; color: rgba(255,255,255,0.5); }
.content { flex: 1; display: flex; overflow: hidden; background: #fff; }
.category-sidebar { width: 180rpx; background: #f7f7f7; flex-shrink: 0; }
.cat-item { padding: 28rpx 20rpx; font-size: 26rpx; color: #666; text-align: center; }
.cat-item.active { background: #fff; color: #ff6b35; font-weight: bold; border-left: 6rpx solid #ff6b35; }
.product-list { flex: 1; height: 60vh; }
.product-item { display: flex; padding: 20rpx; border-bottom: 1rpx solid #f5f5f5; }
.prod-img { width: 140rpx; height: 140rpx; border-radius: 12rpx; flex-shrink: 0; }
.prod-info { flex: 1; padding-left: 16rpx; display: flex; flex-direction: column; justify-content: space-between; }
.prod-name { font-size: 28rpx; color: #333; font-weight: 500; }
.prod-desc { font-size: 22rpx; color: #999; margin-top: 6rpx; }
.prod-footer { display: flex; justify-content: space-between; align-items: center; }
.prod-price { font-size: 30rpx; color: #ff6b35; font-weight: bold; }
.quantity-ctrl { display: flex; align-items: center; gap: 16rpx; }
.qty-btn { width: 44rpx; height: 44rpx; border-radius: 50%; background: #eee; display: flex; align-items: center; justify-content: center; font-size: 28rpx; color: #666; text-align: center; line-height: 44rpx; }
.qty-add { background: #ff6b35; color: #fff; }
.qty-num { font-size: 28rpx; color: #333; min-width: 30rpx; text-align: center; }
.cart-bar { position: fixed; bottom: 0; left: 0; right: 0; background: #333; padding: 20rpx 30rpx; display: flex; align-items: center; padding-bottom: calc(20rpx + env(safe-area-inset-bottom)); }
.cart-icon-wrap { position: relative; margin-right: 16rpx; }
.cart-icon { font-size: 44rpx; }
.cart-badge { position: absolute; top: -8rpx; right: -8rpx; background: #ff3b30; color: #fff; font-size: 18rpx; min-width: 28rpx; height: 28rpx; border-radius: 14rpx; display: flex; align-items: center; justify-content: center; }
.cart-total { flex: 1; font-size: 30rpx; color: #fff; font-weight: bold; }
.cart-btn { background: #ff6b35; color: #fff; font-size: 28rpx; padding: 14rpx 36rpx; border-radius: 40rpx; }
</style>
