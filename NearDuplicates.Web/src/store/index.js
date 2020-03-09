import Vue from 'vue'
import Vuex from 'vuex'
import { set } from './vuex'
import api from '../api/api'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    categories: [],
    sellersForCategory: [],
    listings: [],
    comparison: {
      baseListing: {},
      duplicate: {}
    },
    showSpinner: false,
    showSnackbar: false,
    snackbarText: '',
    snackbarIsError: false,
    jobPercent: 0
  },
  getters: {
    categories: state => {
      return state.categories
    },
    sellersForCategory: state => {
      return state.sellersForCategory
    },
    listings: state => {
      return state.listings
    },
    comparison: state => {
      return state.comparison
    },
    jobPercent: state => {
      return state.jobPercent
    }
  },
  actions: {
    getCategories({ commit, dispatch }) {
      api
        .getCategories()
        .then(response => {
          commit('setCategories', response.data)
        })
        .catch(e => {
          dispatch('handleError', e, { root: true })
        })
    },
    getSellers({ commit, dispatch }, args) {
      api
        .getSellers(args.mcat_path, args.seller_id)
        .then(response => {
          commit('setSellers', response.data)
          args.callback()
        })
        .catch(e => {
          dispatch('handleError', e, { root: true })
        })
    },
    analyze({ dispatch }, args) {
      api
        .analyze(args.mcat_path, args.seller_id, args.job_id)
        .then(() => {
          args.callback()
        })
        .catch(e => {
          dispatch('handleError', e, { root: true })
        })
    },
    getListingsForSeller({ commit, dispatch }, args) {
      api
        .getListingsForSeller(args.seller_id, args.mcat_path)
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
    getJobPercent({ commit, dispatch }, args) {
      api
        .getJobPercent(args.jobid)
        .then(response => {
          commit('setJobPercent', response.data)
        })
        .catch(e => {
          dispatch('handleError', e, { root: true })
          args.error()
        })
    },
    clearJobPercent({ commit }) {
      commit('setJobPercent', 0)
    },
    handleSuccess(context, successMessage) {
      context.commit('showSnackbar', true)
      context.commit('snackbarText', successMessage)
      context.commit('snackbarIsError', false)
      context.commit('showSpinner', false)
    },

    handleError(context, error) {
      if (!error.response) {
        console.error(error)
      }
      context.commit('showSnackbar', true)
      context.commit('snackbarText', error.response.data ? error.response.data.error : 'An error occurred')
      context.commit('snackbarIsError', true)
      context.commit('showSpinner', false)
    }
  },
  mutations: {
    setCategories(state, categories) {
      state.categories = categories
    },
    setSellers(state, sellersForCategory) {
      state.sellersForCategory = sellersForCategory
    },
    setListings(state, listings) {
      state.listings = listings
    },
    setComparison(state, comparison) {
      state.comparison = comparison
    },
    setJobPercent(state, jobPercent) {
      state.jobPercent = jobPercent
    },
    showSpinner: set('showSpinner'),
    showSnackbar: set('showSnackbar'),
    snackbarText: set('snackbarText'),
    snackbarIsError: set('snackbarIsError')
  }
})
