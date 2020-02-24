<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn text color="primary light" icon @click="goBack()" title="Back">
          <v-icon medium>fa-arrow-circle-left</v-icon>
        </v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <h3>Title</h3>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <span class="id caption">{{ comparison.baseListing.id }}</span>
        <span class="headline">{{ comparison.baseListing.title }}</span>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <span class="id caption">{{ comparison.closestDuplicateByTitle.id }}</span>
        <span>{{ comparison.closestDuplicateByTitle.title }}</span>
      </v-col>
    </v-row>
  </v-container>
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
