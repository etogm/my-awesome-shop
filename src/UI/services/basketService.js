import { api } from "./api";

export default {
  async addToBasket(userId, productId, quantity) {
    await api.post(`basket/users/${userId}/products?productId=${productId}&quantity=${quantity}`)
  },

  async deleteFromBasket(userId, productId) {
    await api.delete(`basket/users/${userId}/products`, {
      params: {
        productId
      }
    })
  },

  getBasket(userId) {
    return api.get(`basket/users/${userId}/products`)
  }
}