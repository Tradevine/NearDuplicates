<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn color="primary light" @click="goBack()" title="Back">
          <v-icon left medium>fa-arrow-circle-left</v-icon>
          Back to grid
        </v-btn>
      </v-col>
      <v-col>
        <v-btn color="primary light" @click="nextListing()" title="Next" class="float-right" v-show="!end">
          <v-icon left medium>fa-arrow-circle-right</v-icon>
          Next listing
        </v-btn>
      </v-col>
    </v-row>

    <v-row>
      <v-col class="d-flex justify-center">
        <h3>Base Listing (A)</h3>
      </v-col>
      <v-col class="d-flex justify-center">
        <h3>Compared With (B)</h3>
      </v-col>
      <v-col class="d-flex justify-center">
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
  props: {
    id: {
      type: Number,
      default: 0
    },
    end: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {}
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
  watch: {
    id(newVal) {
      if (!newVal || newVal === 0) return

      this.$store.dispatch('getComparison', {
        id: newVal,
        callback: () => {}
      })
    }
  },
  methods: {
    goBack() {
      this.$emit('close')
    },
    nextListing() {
      this.$emit('next')
    }
  }
}
</script>

<style lang="scss" scoped></style>
