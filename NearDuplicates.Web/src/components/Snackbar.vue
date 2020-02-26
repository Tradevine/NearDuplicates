<template>
  <v-snackbar v-model="show" :color="colour" :timeout="timeout" multi-line top>
    <ul :class="isError ? 'red' : 'green'">
      <li v-for="(line, index) in lines" :key="index" class="line" color="white">
        {{ line }}
      </li>
    </ul>
    <v-btn color="white" @click="close()">
      Close
    </v-btn>
  </v-snackbar>
</template>

<script>
export default {
  name: 'Snackbar',
  data: () => ({}),
  computed: {
    show: {
      get() {
        return this.$store.state.showSnackbar
      },
      set(val) {
        this.$store.commit('showSnackbar', val)
      }
    },
    text: {
      get() {
        return this.$store.state.snackbarText
      },
      set(val) {
        this.$store.commit('snackbarText', val)
      }
    },
    isError: {
      get() {
        return this.$store.state.snackbarIsError
      },
      set(val) {
        this.$store.commit('snackbarIsError', val)
      }
    },
    colour() {
      return this.isError ? 'red' : 'green'
    },
    timeout() {
      return this.isError ? 0 : 6000
    },
    lines() {
      return this.text ? this.text.split('\n') : ''
    }
  },
  methods: {
    close() {
      this.text = ''
      this.show = false
    }
  }
}
</script>

<style lang="scss">
ul {
  &.success {
    list-style-type: none;
  }
  &.error {
    list-style-type: disc;
  }
}
</style>
