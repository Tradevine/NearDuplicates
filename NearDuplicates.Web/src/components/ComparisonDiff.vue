<template>
  <v-container v-if="!!duplicate" class="diff-container">
    <v-row>
      <v-col>
        <div v-show="titlesIdentical">Identical titles</div>
        <div v-show="!titlesIdentical">
          <span>A: </span>
          <v-chip small label color="blue lighten-1" class="mr-1 mb-1" v-for="(part, i) in title_A_not_B" :key="i">
            {{ part.value }}
          </v-chip>
        </div>
        <div v-show="!titlesIdentical" class="mt-2">
          <span>B: </span>
          <v-chip small label color="green lighten-1" class="mr-1 mb-1" v-for="(part, i) in title_B_not_A" :key="i">
            {{ part.value }}
          </v-chip>
        </div>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <div class="caption">Category:</div>
        <span class="subtitle-2" v-show="categoriesIdentical">Same category</span>
        <span class="subtitle-2" v-show="!categoriesIdentical">Different category</span>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <div class="caption">Price:</div>
        <span class="subtitle-2">{{ priceDiffPercent }}%</span>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <div v-show="descriptionsIdentical">Identical descriptions</div>
        <div v-show="!descriptionsIdentical">
          <span>A: </span>
          <v-chip small label color="blue lighten-1" class="mr-1 mb-1" v-for="(part, i) in description_A_not_B" :key="i">
            {{ part.value }}
          </v-chip>
        </div>
        <div v-show="!descriptionsIdentical" class="mt-2">
          <span>B: </span>
          <v-chip small label color="green lighten-1" class="mr-1 mb-1" v-for="(part, i) in description_B_not_A" :key="i">
            {{ part.value }}
          </v-chip>
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
const Diff = require('diff')
import _ from 'lodash'

export default {
  name: 'ComparisonDiff',
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
    return {
      titleDiffs: [],
      descDiffs: []
    }
  },
  computed: {
    titlesIdentical() {
      return this.baselisting.title === this.duplicate.title
    },
    descriptionsIdentical() {
      return this.baselisting.description === this.duplicate.description
    },
    categoriesIdentical() {
      return this.baselisting.mcath_path === this.duplicate.mcat_path
    },
    priceDiffPercent() {
      return ((this.baselisting.buy_now_price / this.duplicate.buy_now_price) * 100).toFixed(2)
    },
    title_A_not_B() {
      return _.filter(this.titleDiffs, part => {
        return part.removed
      })
    },
    title_B_not_A() {
      return _.filter(this.titleDiffs, part => {
        return part.added
      })
    },
    description_A_not_B() {
      return _.filter(this.descDiffs, part => {
        return part.removed
      })
    },
    description_B_not_A() {
      return _.filter(this.descDiffs, part => {
        return part.added
      })
    }
  },
  methods: {
    calculateDiffs() {
      this.titleDiffs = Diff.diffWords(this.baselisting.title, this.duplicate.title)
      this.descDiffs = Diff.diffWords(this.baselisting.description, this.duplicate.description)
    }
  },
  watch: {
    duplicate() {
      this.calculateDiffs()
    }
  }
}
</script>

<style lang="scss" scoped>
.diff-container {
  margin-top: 210px;
}
</style>
