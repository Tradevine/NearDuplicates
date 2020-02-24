<template>
  <div class="home">
    <h2>Near duplicates list</h2>
    <duplicates-list />
  </div>
</template>

<script>
// @ is an alias to /src
import DuplicatesList from '@/components/DuplicatesList.vue'

export default {
  name: 'Home',
  components: {
    DuplicatesList
  }
}
</script>
