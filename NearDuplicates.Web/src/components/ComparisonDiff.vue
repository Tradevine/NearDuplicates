<template>
  <v-container v-if="!!duplicate">
    <v-row>
      <v-col class="d-flex justify-center">
        <div class="caption">Title:</div>
      </v-col>
    </v-row>

    <v-row>
      <v-col class="d-flex justify-center">
        <diff-display :baseString.sync="baselisting.title" :compareString.sync="duplicate.title" propertyName="title" />
      </v-col>
    </v-row>

    <v-row class="mt-2">
      <v-col class="d-flex justify-center">
        <div class="caption">Category:</div>
      </v-col>
    </v-row>

    <v-row>
      <v-col class="d-flex justify-center">
        <span class="subtitle-2" v-show="categoriesIdentical">Same category</span>
        <span class="subtitle-2" v-show="!categoriesIdentical">Different category</span>
      </v-col>
    </v-row>

    <v-row class="mt-2">
      <v-col class="d-flex justify-center">
        <div class="caption">Price:</div>
      </v-col>
    </v-row>

    <v-row>
      <v-col class="d-flex justify-center">
        <span class="subtitle-2">{{ priceDiffPercent }}%</span>
      </v-col>
    </v-row>

    <v-row class="mt-2">
      <v-col class="d-flex justify-center">
        <div class="caption">Description:</div>
      </v-col>
    </v-row>

    <v-row>
      <v-col class="d-flex justify-center">
        <diff-display :baseString.sync="baselisting.description" :compareString.sync="duplicate.description" propertyName="description" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import DiffDisplay from '@/components/DiffDisplay'

export default {
  name: 'ComparisonDiff',
  components: {
    DiffDisplay
  },
  props: {
    baselisting: {
      type: Object,
      default: function() {
        return {}
      }
    },
    duplicate: {
      type: Object,
      default: function() {
        return {}
      }
    }
  },
  data() {
    return {}
  },
  computed: {
    categoriesIdentical() {
      return this.baselisting.mcath_path === this.duplicate.mcat_path
    },
    priceDiffPercent() {
      return (100 - (this.baselisting.buy_now_price / this.duplicate.buy_now_price) * 100).toFixed(2)
    }
  },
  methods: {}
}
</script>

<style lang="scss" scoped></style>
