<template>
  <router-link :to="navigateTo">
    {{ linkText }}
  </router-link>
</template>

<script>
export default {
  name: 'LinkCellRenderer',
  computed: {
    navigateTo() {
      if (this.params.name === 'ProductName') {
        return `/products/${this.params.value}`
      }

      return '/'
    },
    linkText() {
      return this.params.value
    }
  },
  methods: {}
}
</script>

<style scoped></style>
