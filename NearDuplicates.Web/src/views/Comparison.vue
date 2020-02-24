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
      <v-col>
        <h3>Price</h3>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-container>
          <v-row>
            <v-col class="flex-grow-0">
              <span class="caption">{{ comparison.baseListing.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">{{ comparison.baseListing.title }}</span>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="flex-grow-0">
              <span class="caption">{{ comparison.closestDuplicateByTitle.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">{{ comparison.closestDuplicateByTitle.title }}</span>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
      <v-col>
        <v-container>
          <v-row>
            <v-col class="flex-grow-0">
              <span class="caption">{{ comparison.baseListing.id }}</span>
            </v-col>
            <v-col>
              <span class="subtitle-2">${{ comparison.baseListing.buy_now_price }}</span>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="flex-grow-0">
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

<style lang="scss" scoped></style>
