<template>
  <v-container v-if="!!listing">
    <v-row>
      <v-col class="d-flex justify-center">
        <a :href="`https://www.trademe.co.nz//Browse/Listing.aspx?id=${listing.id}`" target="blank">{{ listing.id }}</a>
      </v-col>
    </v-row>

    <v-row>
      <v-col class="d-flex justify-center">
        <v-img maxWidth="200" v-if="listing.photo_id" :src="`https://trademe.tmcdn.co.nz/photoserver/plusw/${listing.photo_id}.jpg`"></v-img>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <h3>{{ listing.title }}</h3>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <div class="caption">Category:</div>
        <span class="subtitle-2">{{ listing.mcat_path }}</span>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <div class="caption">Price:</div>
        <span class="subtitle-2">${{ listing.buy_now_price }}</span>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <span class="body-2">{{ listing.description }}</span>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: 'ComparisonListing',
  props: {
    listing: {
      type: Object,
      default: function() {
        return {}
      }
    }
  },
  data() {
    return {}
  },
  computed: {},
  methods: {}
}
</script>

<style lang="scss" scoped></style>
