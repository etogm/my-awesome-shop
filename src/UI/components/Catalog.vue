<script setup>
import { ref, onMounted } from 'vue'

import { useBasketStore } from '@/stores/basket'
import { useCatalogStore } from '@/stores/catalog'

const basket = useBasketStore()
const catalog = useCatalogStore()

const products = ref([])
onMounted(async () => {
  await catalog.fetch()
  products.value = catalog.products.map(p => ({...p, quantity: 1}))
})
</script>

<template>
  <div class="box">
    <h1>Каталог</h1>

    <table class="min-w-full text-left text-sm font-light">
      <thead class="border-b font-medium dark:border-neutral-500">
        <tr>
          <th>Id</th>
          <th>Название</th>
          <th>Описание</th>
          <th>Стоимость</th>
          <th>Кол-во</th>
          <th>Действие</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="product in products">
          <td>{{ product.id }}</td>
          <td>{{ product.name }}</td>
          <td>{{ product.description }}</td>
          <td>{{ product.price.amount }} RUB</td>
          <td><input type="number" v-model="product.quantity" /></td>
          <td>
            <button @click="basket.addToBasket(product)">
              Добавить в корзину
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
@/services/catalogService
