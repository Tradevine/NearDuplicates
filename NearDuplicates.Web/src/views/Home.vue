<template>
  <div class="home">
    <div>
      <h2>No more duplicates...</h2>
    </div>

    <duplicates-list class="mt-2 pa-3" />
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
