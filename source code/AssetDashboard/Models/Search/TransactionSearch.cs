using System;
using System.Collections.Generic;

namespace StarTrack.Dashboard.Models.Search
{
    public class TransactionSearch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Location { get; set; }
        public string Direction { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
    public class TransactionResult
    {
        public TransactionResult()
        {
            AssetTransactions = new List<AssetTransaction>();
        }
        public List<AssetTransaction> AssetTransactions { get; set; }
        public int RowsCount { get; set; }
    }
    public class AssetTransaction
    {
        public string AssetId { get; set; }
        public string AssetName { get; set; }
        public int AssetStatus { get; set; }
        public string Location { get; set; }
        public DateTime Date_and_Time { get; set; }
        public string Direction { get; set; }
    }
}