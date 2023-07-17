<script setup>
import CreateProduct from './components/CreateProduct.vue'
import Basket from './components/Basket.vue'
import Catalog from './components/Catalog.vue'
import { useUserStore } from './stores/user'
import Notifications from './components/Notifications.vue'

const user = useUserStore()
</script>

<template>
  <div class="container mx-auto">
    <div class="box">
      <h2>Пользователь</h2>
      <input class="forms-input" type="text" v-model="user.userId" />
    </div>
    <CreateProduct />
    <Notifications />
    <Basket />
    <Catalog />
  </div>
</template>

<style scoped></style>
./services/catalogService
