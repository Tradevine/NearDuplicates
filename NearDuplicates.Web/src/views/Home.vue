<template>
  <v-container>
    <v-row>
      <v-col>
        <h2>Near duplicates search</h2>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <duplicates-list />
      </v-col>
    </v-row>
  </v-container>
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
