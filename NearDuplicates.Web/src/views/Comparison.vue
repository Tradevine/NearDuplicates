<template>
  <div class="comparison text-left">
    <v-btn text color="primary light" icon @click="goBack()" title="Back">
      <v-icon medium>fa-arrow-circle-left</v-icon>
    </v-btn>
    <h3>Title</h3>
    <div class="float-left">
      <span class="id caption">{{ comparison.baseListing.id }}</span>
      <span>{{ comparison.baseListing.title }}</span>
    </div>
    <div class="float-left">
      <span class="id caption">{{ comparison.closestDuplicateByTitle.id }}</span>
      <span>{{ comparison.closestDuplicateByTitle.title }}</span>
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

<style lang="scss" scoped>
.id {
  min-width: 160px;
  max-width: 160px;
}
</style>
