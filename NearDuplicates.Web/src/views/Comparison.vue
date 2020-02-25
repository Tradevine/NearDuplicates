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
        <h3>Category</h3>
      </v-col>
      <v-col>
        <h3>Title</h3>
      </v-col>
      <v-col>
        <h3>Price</h3>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-container class="py-0">
          <v-row>
            <v-col class="flex-grow-0 pl-0">
              <span class="caption">{{ comparison.baseListing.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">{{ comparison.baseListing.mcat_path }}</span>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="flex-grow-0 pl-0">
              <span class="caption">{{ comparison.closestDuplicateByTitle.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">{{ comparison.closestDuplicateByTitle.mcat_path }}</span>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
      <v-col>
        <v-container class="py-0">
          <v-row>
            <v-col class="pl-0 id-fixed">
              <span class="caption">{{ comparison.baseListing.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">{{ comparison.baseListing.title }}</span>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="pl-0 id-fixed">
              <span class="caption">{{ comparison.closestDuplicateByTitle.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">{{ comparison.closestDuplicateByTitle.title }}</span>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="pl-0 id-fixed">
              <span class="caption">Diff</span>
            </v-col>
            <v-col>
              <div class="subtitle-2" id="titleDiff"></div>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
      <v-col>
        <v-container class="py-0">
          <v-row>
            <v-col class="flex-grow-0 pl-0">
              <span class="caption">{{ comparison.baseListing.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">${{ comparison.baseListing.buy_now_price }}</span>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="flex-grow-0 pl-0">
              <span class="caption">{{ comparison.closestDuplicateByTitle.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">${{ comparison.closestDuplicateByTitle.buy_now_price }}</span>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
    </v-row>

    <v-row class="mt-4">
      <v-col>
        <h3>Description</h3>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <span class="caption">{{ comparison.baseListing.id }}</span>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <span class="body-2">{{ comparison.baseListing.description }}</span>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <span class="caption">{{ comparison.closestDuplicateByTitle.id }}</span>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <span class="body-2">{{ comparison.closestDuplicateByTitle.description }}</span>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <span class="caption">Diff</span>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <div class="body-2" id="descDiff"></div>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
const Diff = require('diff')

export default {
  name: 'Comparison',
  data() {
    return {
      id: '',
      titleDiff: '',
      descriptionDiff: ''
    }
  },
  beforeMount() {
    this.id = this.$route.params.id
    this.$store.dispatch('getComparison', {
      id: this.id,
      callback: () => {
        this.displayTitleDiff()
        this.displayDescriptionDiff()
      }
    })
  },
  computed: {
    comparison() {
      return this.$store.getters.comparison
    }
  },
  methods: {
    goBack() {
      this.$router.go(-1)
    },
    displayTitleDiff() {
      var diff = Diff.diffChars(this.comparison.baseListing.title, this.comparison.closestDuplicateByTitle.title)
      this.displayDiff(diff, 'titleDiff')
    },
    displayDescriptionDiff() {
      var diff = Diff.diffChars(this.comparison.baseListing.description, this.comparison.closestDuplicateByTitle.description)
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
  }
}
</script>

<style lang="scss" scoped>
.id-fixed {
  min-width: 100px;
  max-width: 100px;
}
</style>
