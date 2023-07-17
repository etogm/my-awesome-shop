import catalogService from '@/services/catalogService'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useCatalogStore = defineStore('catalog', () => {
  const products = ref([])

  const currentPage = ref(1)
  const perPage = ref(50)

  async function fetch() {
    const result = (
      await catalogService.getProducts(currentPage.value, perPage.value)
    ).data

    products.value = result.items
  }

  return { products, currentPage, perPage, fetch }
})
