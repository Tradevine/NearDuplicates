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
  analyze(mcat_path, seller_id, job_id) {
    return apiAxios.post('/Actions/Analyze', { mcat_path, seller_id, job_id })
  },
  getSellers(mcat_path, seller_id) {
    return apiAxios.get(`/Listings/GetSellers?mcat_path=${mcat_path}&seller_id=${seller_id}`)
  },
  getListingsForSeller(seller_id, mcat_path) {
    return apiAxios.get(`/Listings/GetListingsForSeller?seller_id=${seller_id}&mcat_path=${mcat_path}`)
  },
  getComparison(listing_id) {
    return apiAxios.get(`/Listings/GetComparison?listing_id=${listing_id}`)
  },
  getJobPercent(job_id) {
    return apiAxios.get(`/Actions/GetJobProgress?job_id=${job_id}`)
  }
}
