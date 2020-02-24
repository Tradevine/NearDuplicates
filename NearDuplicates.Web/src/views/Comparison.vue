<template>
  <div class="comparison">
    <v-btn class="d-inline-flex" text color="primary light" icon @click="goBack()" title="Back">
      <v-icon medium>fa-arrow-circle-left</v-icon>
    </v-btn>
    <div>
      <h3>Title</h3>
      <span class="caption">{{ comparison.baseListing.id }}</span>
      <h4>{{ comparison.baseListing.title }}</h4>
      <span class="caption mt-2">{{ comparison.closestDuplicateByTitle.id }}</span>
      <h4>{{ comparison.closestDuplicateByTitle.title }}</h4>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Comparison',
  data() {
    return {
      id: ''
    }
  },
  beforeMount() {
    this.id = this.$route.params.id
    this.$store.dispatch('getComparison', this.id)
  },
  computed: {
    comparison() {
      return this.$store.getters.comparison
    }
  },
  methods: {
    goBack() {
      this.$router.go(-1)
    }
  }
}
</script>
