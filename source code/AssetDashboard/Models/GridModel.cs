using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarTrack.Dashboard.Models
{
    public class GridModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Index of columns that are used in sorting
        /// </summary>
        public int iSortCol_0 { get; set; }

        /// <summary>
        /// Sorting direction (asc or desc).
        /// </summary>
        public string sSortDir_0 { get; set; }
        public SortDirection SortingDirection
        {
            get
            {
                if (string.IsNullOrEmpty(sSortDir_0))
                    return SortDirection.Undefiened;
                switch (sSortDir_0)
                {
                    case "asc":
                        return SortDirection.Ascending;
                    case "desc":
                        return SortDirection.Descending;
                    default:
                        return SortDirection.Undefiened;
                }
            }
        }
        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string guid { get; set; }


        public string Settings { get; set; }
    }

    public class GridSetting
    {
        public int Key { get; set; }
    }
    public class GridSetting<T> : GridSetting
    {
        public T ExternalData { get; set; }
    }
    public class GridReponseBase
    {
        public ErrorCode ErrorCode { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public string sEcho { get; set; }
    }
    public class GridReponseFail : GridReponseBase
    {
        public string aaData { get; set; }
    }
    public class GridReponseSuccess : GridReponseBase
    {
        public IEnumerable<object[]> aaData { get; set; }
    }
    public enum SortDirection
    {
        Undefiened = 0,
        Ascending = 1,
        Descending = 2
    }

    public class GridDataReponse<T>
    {
        public int RowsCount { get; set; }
        public T Data { get; set; }
    }
}