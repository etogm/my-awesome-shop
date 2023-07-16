<script setup>
import { onMounted, ref } from 'vue'

import basketService from './services/basketService'

const products = ref([])

onMounted(async () => {
  var response = await basketService.getProducts(1, 50)

  console.log(response.data)

  products.value = response.data.items
})
</script>

<template>
  <ul>
    <li v-for="product in products">{{ product.id }} {{ product.name }} {{ product.description }}</li>
  </ul>
</template>

<style scoped></style>
