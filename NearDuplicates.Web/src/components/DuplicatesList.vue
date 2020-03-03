<template>
  <v-container v-resize="onResize">
    <v-row v-show="!showDuplicate">
      <v-col class="py-0 flex-grow-0">
        <v-btn color="primary light" @click="goBack()" title="Back">
          <v-icon left medium>fa-arrow-circle-left</v-icon>
          Back to sellers
        </v-btn>
      </v-col>
      <v-col class="pt-1">
        <h3>{{ seller_name }} ({{ seller_id }})</h3>
      </v-col>
    </v-row>
    <v-row v-show="!showDuplicate">
      <v-col>
        <ag-grid-vue style="width: 100%; height: 85vh" class="ag-theme-material" :components="components" :grid-options="gridOptions" :row-data="listings" />
      </v-col>
    </v-row>
    <v-row v-show="showDuplicate">
      <v-col>
        <comparison
          :id.sync="listing_id"
          @close="closeDuplicate()"
          @next="nextListing()"
          @previous="previousListing()"
          :start.sync="startOfListings"
          :end.sync="endOfListings"
        />
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
  props: {
    seller_id: {
      type: Number,
      default: 0
    },
    seller_name: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      components: {},
      showDuplicate: false,
      listing_id: 0,
      selectedParams: {},
      startOfListings: true,
      endOfListings: false,
      gridOptions: {
        animateRows: true,
        enableCellTextSelection: true,
        rowSelection: 'multiple',
        onRowSelected: this.rowSelected
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
      },
      {
        headerName: 'Title Rank',
        field: 'similarity_title',
        cellClass: 'text-xs-right',
        headerClass: 'text-xs-right',
        resizable: true,
        sortable: true,
        filter: 'agNumberColumnFilter',
        minWidth: 140,
        maxWidth: 140
      },
      {
        headerName: 'Description Rank',
        field: 'similarity_description',
        cellClass: 'text-xs-right',
        headerClass: 'text-xs-right',
        resizable: true,
        sortable: true,
        filter: 'agNumberColumnFilter',
        minWidth: 140,
        maxWidth: 140
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
      this.startOfListings = true
      this.endOfListings = false
      this.$emit('close')
    },
    closeDuplicate() {
      this.showDuplicate = false
    },
    rowSelected(params) {
      this.selectedParams = params
      if (params.node.selected === false) return
      this.startOfListings = params.node.childIndex === 0
      this.endOfListings = params.node.childIndex === this.listings.length - 1
      this.setListing(params.data.id)
    },
    setListing(id) {
      this.showDuplicate = true
      this.listing_id = id
    },
    nextListing() {
      this.searchForIndex(1)
      this.startOfListings = false
      this.endOfListings = this.gridOptions.api.getSelectedRows().length === 0
    },
    previousListing() {
      this.searchForIndex(-1)
      this.endOfListings = false
      this.startOfListings = this.gridOptions.api.getSelectedRows().length === 0
    },
    searchForIndex(delta) {
      this.gridOptions.api.deselectAll()

      this.gridOptions.api.forEachNode(node => {
        if (node.childIndex === this.selectedParams.node.childIndex + delta) {
          node.setSelected(true)
          return
        }
      })
    }
  }
}
</script>

<style scoped lang="scss"></style>
