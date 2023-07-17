<script setup>
import { useBasketStore } from '@/stores/basket'
import { onMounted } from 'vue'

const basket = useBasketStore()

async function updateBasket() {
  await basket.fetch()
}

onMounted(async () =>{
  await updateBasket()
})
</script>

<template>
  <div class="box">
    <h1>Корзина</h1>
    <button @click="updateBasket">Обновить корзину</button>
    <table class="min-w-full text-left text-sm font-light">
      <thead class="border-b font-medium dark:border-neutral-500">
        <tr>
          <th>Id</th>
          <th>Название</th>
          <th>Стоимость</th>
          <th>Кол-во</th>
          <th>Действие</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="product in basket.products">
          <td>{{ product.id }}</td>
          <td>{{ product.name }}</td>
          <td>{{ product.price.amount }} RUB</td>
          <td>{{ product.quantity }}</td>
          <td><button @click="basket.deleteFromBasket(product.id)">Удалить из корзины</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
