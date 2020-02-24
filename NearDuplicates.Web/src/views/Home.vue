<template>
  <div class="home">
    <h2>Near duplicates search</h2>

    <duplicates-list />
  </div>
</template>

<script>
import DuplicatesList from '@/components/DuplicatesList.vue'

export default {
  name: 'Home',
  components: {
    DuplicatesList
  }
}
</script>
