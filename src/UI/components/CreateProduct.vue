<script setup>
import { ref } from 'vue'
import catalogService from '@/services/catalogService'
import { useCatalogStore } from '@/stores/catalog'

const catalog = useCatalogStore()
const product = ref({
  id: '',
  name: '',
  description: '',
  price: {
    amount: 0,
    currency: 0,
  },
})

async function createOrUpdate() {
  if (product.value.id === '') {
    await catalogService.createProduct(product.value)
  } else {
    await catalogService.updateProduct(product.value)
  }

  await catalog.fetch()
}
</script>
<template>
  <div class="box">
    <h2>Добавить/обновить продукт</h2>

    <div class="mb-4">
      <label class="forms-label">Id</label>
      <input class="forms-input forms-focus" type="text" v-model="product.id" />
    </div>

    <div class="mb-4">
      <label class="forms-label">Название</label>
      <input
        class="forms-input forms-focus"
        type="text"
        v-model="product.name"
      />
    </div>

    <div class="mb-4">
      <label class="forms-label">Описание</label>
      <input
        class="forms-input forms-focus"
        type="text"
        v-model="product.description"
      />
    </div>

    <div class="mb-4">
      <label class="forms-label">Цена</label>
      <input
        class="forms-input forms-focus"
        type="text"
        v-model="product.price.amount"
      />
    </div>

    <div class="mb-4">
      <label class="forms-label">Валюта</label>
      <input
        class="forms-input forms-focus"
        type="text"
        v-model="product.price.currency"
      />
    </div>

    <button @click="createOrUpdate" class="btn btn-blue">
      Добавить/обновить
    </button>
  </div>
</template>
@/services/catalogService
