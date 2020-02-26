<template>
  <div id="app">
    <v-app>
      <v-content>
        <snackbar />
        <router-view />
      </v-content>
    </v-app>
  </div>
</template>

<script>
import Snackbar from '@/components/Snackbar.vue'

export default {
  name: 'App',
  components: {
    Snackbar
  }
}
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: left;
  color: #2c3e50;

  .border {
    border: 1px solid grey;
  }
}
</style>
