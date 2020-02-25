<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn color="primary light" icon @click="goBack()" title="Back">
          <v-icon left medium>fa-arrow-circle-left</v-icon>
          Back to grid
        </v-btn>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <h3>Base Listing</h3>
      </v-col>
      <v-col>
        <h3>Compared With</h3>
      </v-col>
      <v-col>
        <h3>Diff</h3>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <comparison-listing :listing.sync="baseListing" />
      </v-col>
      <v-col>
        <comparison-listing :listing.sync="duplicateListing" />
      </v-col>
      <v-col>
        <comparison-diff :baselisting.sync="baseListing" :duplicate.sync="duplicateListing" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import ComparisonListing from '../components/ComparisonListing'
import ComparisonDiff from '../components/ComparisonDiff'

export default {
  name: 'Comparison',
  components: {
    ComparisonListing,
    ComparisonDiff
  },
  data() {
    return {
      id: ''
    }
  },
  beforeMount() {
    this.id = this.$route.params.id
    this.$store.dispatch('getComparison', {
      id: this.id,
      callback: () => {}
    })
  },
  computed: {
    comparison() {
      return this.$store.getters.comparison
    },
    baseListing() {
      return this.comparison.baseListing
    },
    duplicateListing() {
      return this.comparison.duplicate
    }
  },
  methods: {
    goBack() {
      this.$router.go(-1)
    }
  }
}
</script>

<style lang="scss" scoped></style>
