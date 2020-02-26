<template>
  <div>
    <div v-show="propertiesIdentical">Identical {{ propertyName }}s</div>
    <div v-show="!propertiesIdentical">
      <span>A: </span>
      <v-chip small label color="blue lighten-1" class="mr-1 mb-1" v-for="(part, i) in A_not_B" :key="i">
        {{ part.value }}
      </v-chip>
    </div>
    <div v-show="!propertiesIdentical" class="mt-2">
      <span>B: </span>
      <v-chip small label color="green lighten-1" class="mr-1 mb-1" v-for="(part, i) in B_not_A" :key="i">
        {{ part.value }}
      </v-chip>
    </div>
  </div>
</template>

<script>
const Diff = require('diff')
import _ from 'lodash'

export default {
  name: 'DiffDisplay',
  props: {
    baseString: {
      type: String,
      default: ''
    },
    compareString: {
      type: String,
      default: ''
    },
    propertyName: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      diffs: []
    }
  },
  computed: {
    propertiesIdentical() {
      return this.baseString === this.compareString
    },
    A_not_B() {
      return _.filter(this.diffs, part => {
        return part.removed
      })
    },
    B_not_A() {
      return _.filter(this.diffs, part => {
        return part.added
      })
    }
  },
  methods: {
    calculateDiffs() {
      this.diffs = Diff.diffWords(this.baseString, this.compareString)
    }
  },
  watch: {
    compareString() {
      this.calculateDiffs()
    }
  }
}
</script>

<style lang="scss" scoped></style>
