import { api } from './api'

export default {
  getProducts(currentPage, perPage) {
    return api.get('/products', {
      params: {
        currentPage, perPage
      }
    })
  }
}