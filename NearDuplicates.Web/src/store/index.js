import Vue from 'vue'
import Vuex from 'vuex'
import { set } from './vuex'
import api from '../api/api'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    listings: [],
    comparison: {
      baseListing: {},
      closestDuplicateByTitle: {},
      closestDuplicateByDescription: {}
    },
    showSpinner: false,
    showSnackbar: false,
    snackbarText: '',
    snackbarIsError: false
  },
  getters: {
    listings: state => {
      return state.listings
    },
    comparison: state => {
      return state.comparison
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
    getComparison({ commit, dispatch }, args) {
      api
        .getComparison(args.id)
        .then(response => {
          commit('setComparison', response.data)
          args.callback()
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
    setComparison(state, comparison) {
      state.comparison = comparison
    },
    showSpinner: set('showSpinner'),
    showSnackbar: set('showSnackbar'),
    snackbarText: set('snackbarText'),
    snackbarIsError: set('snackbarIsError')
  }
})
