import { ref } from 'vue'
import { defineStore } from 'pinia'
import basketService from '@/services/basketService'
import { useUserStore } from './user'
import * as productHub from '@/hubs/productHub'

export const useBasketStore = defineStore('basket', () => {
  const user = useUserStore()

  const products = ref([])

  async function addToBasket(product) {
    if (user.userId)
      await basketService.addToBasket(
        user.userId,
        product.id,
        product.quantity
      )

    productHub.subscribeToProduct(product.id)
    
    var productFromLocalBasket = products.value.find(f => f.id == product.id)

    if (productFromLocalBasket)
        productFromLocalBasket.quantity += product.quantity
    else
      products.value.push(product)
  }

  async function deleteFromBasket(productId) {
    if (user.userId) {
      await basketService.deleteFromBasket(user.userId, productId)
    }

    productHub.unsubscribeToProduct(productId)
    products.value = products.value.filter(p => p.id != productId)
  }

  async function fetch() {
    if (!user.userId) return

    products.value = (await basketService.getBasket(user.userId)).data

    products.value.forEach((p) => productHub.subscribeToProduct(p.id))
  }

  return { products, addToBasket, fetch, deleteFromBasket }
})
