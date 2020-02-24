export default {
  checkResponse(data) {
    try {
      return JSON.parse(data)
    } catch (e) {
      console.log('Response is not JSON')
    }
  },

  getErrorText(e) {
    try {
      return e.response.data.Error
    } catch (ex) {
      return e
    }
  }
}
