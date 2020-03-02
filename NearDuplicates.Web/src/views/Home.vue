<template>
  <v-container>
    <v-row v-show="!showSeller">
      <v-col class="py-1">
        <h2>Near duplicates search</h2>
      </v-col>
    </v-row>
    <v-row v-show="!showSeller">
      <v-col class="d-inline-flex py-0">
        <v-autocomplete
          :items="categories"
          return-object
          autocomplete="off"
          v-model="selected_category_mcat"
          placeholder="Select a category..."
          solo-inverted
          clearable
          class="py-0"
        ></v-autocomplete>
        <v-btn color="primary light" class="ml-3 mt-2" v-if="showButtons" @click="searchCategory()">
          <v-icon left small>fa fa-search</v-icon>
          Search
        </v-btn>
        <v-btn color="secondary light" class="ml-3 mt-2" v-if="showButtons" @click="analyzeCategory()">
          <v-icon left small>fa fa-cog</v-icon>Download and Analyze
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="mt-2" v-show="showSearching || showAnalyzing">
      <v-col>
        <h3 v-show="showSearching">Searching for sellers in this category...</h3>
        <h3 v-show="showAnalyzing">Analyzing duplicates in this category...</h3>
        <analyze-progress v-show="showAnalyzing" :jobid.sync="job_id" />
      </v-col>
    </v-row>
    <v-row>
      <v-col class="py-0">
        <sellers-list v-show="showGrid" @showSeller="showSeller = true" @hideSeller="showSeller = false" :mcat_path.sync="selected_category_mcat" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import SellersList from '@/components/SellersList.vue'
import AnalyzeProgress from '@/components/AnalyzeProgress.vue'

export default {
  name: 'Home',
  components: {
    SellersList,
    AnalyzeProgress
  },
  data() {
    return {
      selected_category_mcat: '',
      showSearching: false,
      showAnalyzing: false,
      showGrid: false,
      showButtons: false,
      showSeller: false,
      job_id: ''
    }
  },
  computed: {
    categories() {
      return this.$store.getters.categories
    }
  },
  created() {
    this.$store.dispatch('getCategories')
  },
  watch: {
    selected_category_mcat(newVal) {
      this.showGrid = false
      this.showButtons = !!newVal && newVal.length > 0
      this.showSearching = false
      this.showAnalyzing = false
    }
  },
  methods: {
    searchCategory() {
      this.showGrid = false
      this.showSearching = true

      this.$store.dispatch('getSellersForCategory', {
        mcat_path: this.selected_category_mcat,
        callback: () => {
          this.showSearching = false
          this.showGrid = true
        }
      })
    },
    analyzeCategory() {
      this.showGrid = false
      this.job_id = this.uuidv4()
      this.showAnalyzing = true

      this.$store.dispatch('analyzeCategory', {
        mcat_path: this.selected_category_mcat,
        job_id: this.job_id,
        callback: () => {
          this.showAnalyzing = false
          this.searchCategory()
        }
      })
    },
    uuidv4() {
      return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = (Math.random() * 16) | 0,
          v = c == 'x' ? r : (r & 0x3) | 0x8
        return v.toString(16)
      })
    }
  }
}
</script>
