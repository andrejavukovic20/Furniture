export interface Pagination<T> {
    page: number
    pageSize: number
    totalPages: number
    totalItems: number
    items: T
  }
  