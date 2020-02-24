<template>
  <div class="list">
    <ag-grid-vue style="width: 100%; height: 75vh" class="ag-theme-material" :components="components" :grid-options="gridOptions" :row-data="listings" />
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
        rowSelection: 'multiple'
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
        filter: 'agTextColumnFilter'
      },
      {
        headerName: 'Seller Name',
        field: 'seller_name',
        cellClass: 'text-xs-left',
        headerClass: 'text-xs-left',
        resizable: true,
        sortable: true,
        filter: 'agTextColumnFilter'
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
        filter: 'agTextColumnFilter'
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
    }
  }
}
</script>

<style scoped lang="scss"></style>
