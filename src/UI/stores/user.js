import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', () => {
  const userId = ref('356f310f-eff4-4ffa-9391-5bc9b0341560')

  return { userId }
})
