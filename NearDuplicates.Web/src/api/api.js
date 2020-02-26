import axios from 'axios'
import apiUtils from './apiUtils'
import _ from 'lodash'

const apiParams = {
  baseURL: 'http://local.nearduplicates.com/'
}

const noSpinnerUrls = ['']

apiParams.withCredentials = true
apiParams.transformResponse = [data => apiUtils.checkResponse(data)]

apiParams.validateStatus = function(status) {
  window.vm.showSpinner = false

  if (status >= 200 && status < 400) {
    return true
  }

  return false
}

const apiAxios = axios.create(apiParams)

apiAxios.interceptors.request.use(config => {
  window.vm.showSnackbar = false

  if (
    !_.some(noSpinnerUrls, item => {
      return config.url.includes(item)
    })
  ) {
    window.vm.showSpinner = true
  }

  return config
})

apiAxios.interceptors.response.use(response => {
  window.vm.showSpinner = false

  return response
})

export default {
  getApiParams() {
    return apiParams
  },
  getCategories() {
    return apiAxios.get('/Listings/GetCategories')
  },
  analyzeCategory(mcat_path) {
    return apiAxios.post(`/Actions/AnalyzeCategory?mcat_path=${mcat_path}`)
  },
  getSellersForCategory(mcat_path) {
    return apiAxios.get(`/Listings/GetSellersForCategory?mcat_path=${mcat_path}`)
  },
  getListingsForSeller(seller_id, mcat_path) {
    return apiAxios.get(`/Listings/GetListingsForSeller?=seller_id=${seller_id}&mcat_path=${mcat_path}`)
  },
  getComparison(listing_id) {
    return apiAxios.get(`/Listings/GetComparison?listing_id=${listing_id}`)
  }
}
