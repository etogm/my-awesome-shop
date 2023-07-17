<script setup>
import { onMounted, ref } from 'vue'
import { connection } from '@/hubs/productHub'

const notifications = ref([])

onMounted(() => {
  connection.on('productUpdated', (message) => {
    notifications.value.push(message)
    console.log(message)
  })
})
</script>

<template>
  <div class="box">
    <h1>Уведомления</h1>
    <table class="min-w-full text-left text-sm font-light">
      <thead class="border-b font-medium dark:border-neutral-500">
        <tr>
          <th>Id продукта</th>
          <th>Новое название</th>
          <th>Новая цена</th>
          <th>Дата изменения</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="notification in notifications">
          <td>{{ notification.productId }}</td>
          <td>{{ notification.name }}</td>
          <td>{{ notification.price.amount }} RUB</td>
          <td>{{ notification.createdAt }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
