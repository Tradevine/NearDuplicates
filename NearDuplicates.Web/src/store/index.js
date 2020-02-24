import Vue from 'vue'
import Vuex from 'vuex'
import { set } from './vuex'
import api from '../api/api'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    listings: [],
    listing: {},
    showSpinner: false,
    showSnackbar: false,
    snackbarText: '',
    snackbarIsError: false
  },
  getters: {
    listings: state => {
      return state.listings
    }
  },
  actions: {
    getListings({ commit, dispatch }) {
      api
        .getListings()
        .then(response => {
          commit('setListings', response.data)
        })
        .catch(e => {
          dispatch('handleError', e, { root: true })
        })
    },
    handleSuccess(context, successMessage) {
      context.commit('showSnackbar', true)
      context.commit('snackbarText', successMessage)
      context.commit('snackbarIsError', false)
      context.commit('showSpinner', false)
    },

    handleError(context, error) {
      if (!error.response || error.response.status !== 400) {
        console.error(error)
      }
      context.commit('showSnackbar', true)
      context.commit('snackbarText', error.response ? error.response.data.Message : 'An error occurred')
      context.commit('snackbarIsError', true)
      context.commit('showSpinner', false)
    }
  },
  mutations: {
    setListings(state, listings) {
      state.listings = listings
    },
    showSpinner: set('showSpinner'),
    showSnackbar: set('showSnackbar'),
    snackbarText: set('snackbarText'),
    snackbarIsError: set('snackbarIsError')
  }
})
