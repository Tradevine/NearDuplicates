import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import 'vuetify/dist/vuetify.css'
import '@fortawesome/fontawesome-free/css/all.css'
import 'ag-grid-community/dist/styles/ag-grid.css'
import 'ag-grid-community/dist/styles/ag-theme-material.css'
import vuetify from './plugins/vuetify'

Vue.config.productionTip = false

window.vm = new Vue({
  el: '#app',
  router,
  store,
  vuetify,
  render: h => h(App)
})
