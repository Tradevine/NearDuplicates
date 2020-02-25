<template>
  <v-container v-if="!!duplicate">
    <v-row> </v-row>

    <v-row>
      <v-col>
        <span class="heading-1" id="titleDiff"></span>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <div class="caption">Category:</div>
        <span class="subtitle-2">(Cat diff here)</span>
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
        <span class="body-2" id="descDiff"></span>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
const Diff = require('diff')

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
      descDiffs: [],
      priceDiff
    }
  },
  computed: {
    titlesIdentical() {
      return this.baselisting.title === this.duplicate.title
    },
    descriptionsIdentical() {
      return this.baselisting.description === this.duplicate.description
    },
    priceDiffPercent() {
      return (this.baselisting.buy_now_price / this.duplicate.buy_now_price) * 100
    }
  },
  methods: {
    displayTitleDiff() {
      var diff = Diff.diffChars(this.baselisting.title, this.duplicate.title)
      this.displayDiff(diff, 'titleDiff')
    },
    displayDescriptionDiff() {
      var diff = Diff.diffWords(this.baselisting.description, this.duplicate.description)
      this.displayDiff(diff, 'descDiff')
    },
    displayDiff(diff, targetId) {
      var target = document.getElementById(targetId)
      var fragment = document.createDocumentFragment()

      diff.forEach(function(part) {
        // green for additions, red for deletions, grey for common parts
        var color = part.added ? 'green' : part.removed ? 'red' : 'grey'
        var span = document.createElement('span')
        span.style.color = color
        span.appendChild(document.createTextNode(part.value))
        fragment.appendChild(span)
      })

      target.appendChild(fragment)
    }
  },
  watch: {
    duplicate() {
      this.displayTitleDiff()
      this.displayDescriptionDiff()
    }
  }
}
</script>

<style lang="scss" scoped></style>
