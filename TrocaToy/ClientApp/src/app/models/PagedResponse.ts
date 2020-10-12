export class PagedResponse {
  public pageNumber: number;
  public pageSize: number;
  public firstPage: string;
  public lastPage: string;
  public totalPages: number;
  public totalRecords: number;
  public nextPage: string;
  public previousPage: string;
  public data: any;
  public succeeded: boolean;
  public errors: string[];
  public message: string;
}
