<template>
  <div class="home">
    <div>
      <v-icon>fa fa-ban</v-icon>
      <v-icon>fa fa-clone</v-icon>
      <h2>No more duplicates...</h2>
    </div>

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
