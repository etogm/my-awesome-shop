<script setup>
import CreateProduct from './components/CreateProduct.vue'
import Basket from './components/Basket.vue'
import Catalog from './components/Catalog.vue'
import { useUserStore } from './stores/user'
import Notifications from './components/Notifications.vue'
import { ref } from 'vue'

const user = useUserStore()
const creatorMode = ref(false)
</script>

<template>
  <div class="container mx-auto">
    <label class="relative inline-flex items-center mb-5 cursor-pointer">
      <input type="checkbox" value="" class="sr-only peer" v-model="creatorMode" />
      <div
        class="w-9 h-5 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-30 rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-4 after:w-4 after:transition-all dark:border-gray-600"
      ></div>
      <span class="ml-3 text-sm font-medium text-gray-900 dark:text-gray-300"
        >Creator mode</span
      >
    </label>
    <div v-if="creatorMode">
      <CreateProduct />
    </div>
    <div v-else>
      <div class="box">
        <h2>Пользователь</h2>
        <input class="forms-input" type="text" v-model="user.userId" />
      </div>
      <Notifications />
      <Basket />
      <Catalog />
    </div>
  </div>
</template>

<style scoped></style>
./services/catalogService
