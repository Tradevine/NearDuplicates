<template>
  <div class="list">
    <ag-grid-vue style="width: 100%; height: 90vh" class="ag-theme-material" :components="components" :grid-options="gridOptions" :row-data="listings" />
  </div>
</template>

<script>
import { AgGridVue } from 'ag-grid-vue'

export default {
  name: 'DuplicatesList',
  components: {
    AgGridVue
  },
  data() {
    return {
      components: {},
      gridOptions: {
        animateRows: true,
        enableCellTextSelection: true,
        rowSelection: 'multiple',
        onRowDoubleClicked: this.selectListing
      }
    }
  },
  beforeMount() {
    this.$store.dispatch('getListings')

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
        headerName: 'Category',
        field: 'mcat_path',
        cellClass: 'text-xs-left',
        headerClass: 'text-xs-left',
        resizable: true,
        sortable: true,
        filter: 'agTextColumnFilter'
      },
      {
        headerName: 'Listing ID',
        field: 'id',
        cellClass: 'text-xs-left',
        headerClass: 'text-xs-left',
        resizable: true,
        sortable: true,
        filter: 'agNumberColumnFilter',
        minWidth: 140,
        maxWidth: 140
      },
      {
        headerName: 'Title',
        field: 'title',
        cellClass: 'text-xs-left',
        headerClass: 'text-xs-left',
        resizable: true,
        sortable: true,
        filter: 'agTextColumnFilter'
      }
    ]

    setTimeout(() => {
      this.onResize()
    }, 500)
  },
  computed: {
    listings() {
      return this.$store.getters['listings']
    }
  },
  methods: {
    onResize() {
      this.gridOptions.api.sizeColumnsToFit()
    },
    selectListing(params) {
      if (params.node.selected === false) return

      this.$router.push(`/comparison/${params.data.id}`)
    }
  }
}
</script>

<style scoped lang="scss"></style>
