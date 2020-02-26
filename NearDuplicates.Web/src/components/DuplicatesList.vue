<template>
  <v-container>
    <v-row v-show="!showDuplicate">
      <v-col>
        <v-btn color="primary light" icon @click="goBack()" title="Back">
          <v-icon left medium>fa-arrow-circle-left</v-icon>
          Back to sellers
        </v-btn>
      </v-col>
    </v-row>
    <v-row v-show="!showDuplicate">
      <v-col>
        <ag-grid-vue style="width: 100%; height: 90vh" class="ag-theme-material" :components="components" :grid-options="gridOptions" :row-data="listings" />
      </v-col>
    </v-row>
    <v-row v-show="showDuplicate">
      <v-col>
        <comparison :id.sync="listing_id" @close="closeDuplicate()" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { AgGridVue } from 'ag-grid-vue'
import Comparison from '@/components/Comparison'

export default {
  name: 'DuplicatesList',
  components: {
    AgGridVue,
    Comparison
  },
  data() {
    return {
      components: {},
      showDuplicate: false,
      listing_id: 0,
      gridOptions: {
        animateRows: true,
        enableCellTextSelection: true,
        rowSelection: 'multiple',
        onRowDoubleClicked: this.selectListing
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
    goBack() {
      this.$emit('close')
    },
    closeDuplicate() {
      this.showDuplicate = false
    },
    selectListing(params) {
      if (params.node.selected === false) return

      this.showDuplicate = true
      this.listing_id = params.data.id
    }
  }
}
</script>

<style scoped lang="scss"></style>
