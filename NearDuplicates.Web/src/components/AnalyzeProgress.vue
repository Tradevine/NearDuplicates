<template>
  <div class="text-center" v-show="showProgress">
    <v-progress-circular :rotate="360" :size="400" :width="40" :value="jobPercent" color="teal"> {{ jobPercent }} % </v-progress-circular>
  </div>
</template>

<script>
export default {
  name: 'AnalyzeProgress',
  props: {
    jobid: {
      type: String,
      default: function() {
        return ''
      }
    }
  },
  data() {
    return {
      timer: null
    }
  },
  beforeDestroy() {
    this.cancelTimer()
  },
  computed: {
    jobPercent() {
      return this.$store.getters.jobPercent
    },
    showProgress() {
      return this.jobPercent > 0
    }
  },
  watch: {
    jobid() {
      this.cancelTimer()
      this.startNewTimer()
    },
    jobPercent(newVal) {
      if (newVal < 100) return
      this.cancelTimer()
    }
  },
  methods: {
    startNewTimer() {
      this.timer = setInterval(() => {
        this.$store.dispatch('getJobPercent', {
          jobid: this.jobid,
          error: () => {
            this.cancelTimer()
          }
        })
      }, 500)
    },
    cancelTimer() {
      if (this.timer === null) return
      clearInterval(this.timer)
      this.$store.dispatch('clearJobPercent')
    }
  }
}
</script>

<style lang="scss" scoped></style>
