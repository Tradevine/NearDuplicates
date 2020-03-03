<template>
  <v-container>
    <v-row v-show="showSellers">
      <v-col class="border pa-0">
        <ag-grid-vue style="width: 100%; height: 80vh" class="ag-theme-material" :components="components" :grid-options="gridOptions" :row-data="sellers" />
      </v-col>
    </v-row>
    <v-row v-show="showSellerListings">
      <v-col v-if="selected_seller_id > 0">
        <duplicates-list @close="closeSellerListings" :seller_id.sync="selected_seller_id" :seller_name.sync="selected_seller_name" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { AgGridVue } from 'ag-grid-vue'
import DuplicatesList from '@/components/DuplicatesList.vue'

export default {
  name: 'SellersList',
  components: {
    AgGridVue,
    DuplicatesList
  },
  props: {
    mcat_path: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      components: {},
      selected_seller_id: 0,
      selected_seller_name: '',
      showSellers: true,
      showSellerListings: false,
      gridOptions: {
        animateRows: true,
        enableCellTextSelection: true,
        rowSelection: 'multiple',
        onRowSelected: this.selectSeller
      }
    }
  },
  beforeMount() {
    this.gridOptions.columnDefs = [
      {
        headerName: 'Seller ID',
        field: 'seller_id',
        cellClass: 'text-xs-left',
        headerClass: 'text-xs-left',
        resizable: true,
        sortable: true,
        filter: 'agNumberColumnFilter',
        minWidth: 140,
        maxWidth: 140
      },
      {
        headerName: 'Seller Name',
        field: 'seller_name',
        cellClass: 'text-xs-left',
        headerClass: 'text-xs-left',
        resizable: true,
        sortable: true,
        filter: 'agTextColumnFilter',
        minWidth: 180,
        maxWidth: 180
      },
      {
        headerName: 'Number Listings',
        field: 'listings_count',
        cellClass: 'text-xs-right',
        headerClass: 'text-xs-right',
        resizable: true,
        sortable: true,
        sort: 'desc',
        filter: 'agNumberColumnFilter'
      }
    ]

    setTimeout(() => {
      this.onResize()
    }, 500)
  },
  computed: {
    sellers() {
      return this.$store.getters.sellersForCategory
    }
  },
  methods: {
    onResize() {
      this.gridOptions.api.sizeColumnsToFit()
    },
    selectSeller(params) {
      if (params.node.selected === false) return

      this.selected_seller_id = params.data.seller_id
      this.selected_seller_name = params.data.seller_name
      this.showSellerListings = true
      this.showSellers = false
      this.$emit('showSeller')
    },
    closeSellerListings() {
      this.showSellerListings = false
      this.showSellers = true
      this.$emit('hideSeller')
    }
  },
  watch: {
    selected_seller_id(newVal) {
      this.$store.dispatch('getListingsForSeller', { seller_id: newVal, mcat_path: this.mcat_path })
    }
  }
}
</script>

<style scoped lang="scss"></style>
