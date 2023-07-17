import { api } from './api'

export default {
  getProducts(currentPage, perPage) {
    return api.get('catalog/products', {
      params: {
        currentPage, perPage
      }
    })
  },
  
  getProduct(id) {
    return api.get('catalog/products', {
      params: {
        id
      }
    })
  },
  
  createProduct(product) {
    return api.post('catalog/products', product)
  },
  
  updateProduct(product) {
    return api.put('catalog/products', product)
  },
  
  deleteProduct(id) {
    return api.delete('catalog/products', {
      params: {
        id
      }
    })
  }
}