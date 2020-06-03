using StarTrack.Dashboard.Models;
using StarTrack.Dashboard.Models.Search;
using StarTrack.Dashboard.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Web.Mvc;

namespace StarTrack.Dashboard.Controllers
{
    [AuthorizationFilter]
    public class BaseController : Controller
    {
        protected RFIDEntities db = new RFIDEntities();
        private static readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString;
        private void ExecuteReader(string storedProcedure, Action<SqlCommand> FillCommand, Action<SqlDataReader> FetchReader)
        {
            try
            {
                using (var command = new SqlCommand(storedProcedure, new SqlConnection(CONNECTION_STRING)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    FillCommand(command);
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        FetchReader(reader);
                        command.Connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected TransactionResult ListMovements(GridModel model, TransactionSearch search)
        {
            var response = new TransactionResult();
            ExecuteReader("r_AssetTransaction_List",
                delegate (SqlCommand sqlCommand)
                {
                    if (search != null)
                    {
                        if (!string.IsNullOrEmpty(search.Id))
                        {
                            sqlCommand.Parameters.AddWithValue("@Id", search.Id);
                        }
                        if (search.Location != 0)
                        {
                            sqlCommand.Parameters.AddWithValue("@location", search.Location);
                        }
                        if (search.Status != 0)
                        {
                            sqlCommand.Parameters.AddWithValue("@status", search.Status);
                        }
                        if (!string.IsNullOrEmpty(search.Name))
                        {
                            sqlCommand.Parameters.AddWithValue("@name", search.Name);
                        }
                        if (!string.IsNullOrEmpty(search.From))
                        {
                            sqlCommand.Parameters.AddWithValue("@from", DateTime.Parse(search.From));
                        }
                        if (!string.IsNullOrEmpty(search.To))
                        {
                            sqlCommand.Parameters.AddWithValue("@to", DateTime.Parse(search.To).AddHours(23).AddMinutes(59));
                        }
                        if (!string.IsNullOrEmpty(search.Direction))
                        {
                            sqlCommand.Parameters.AddWithValue("@Direction", search.Direction);
                        }
                        if (model.iSortCol_0 != 0)
                        {
                            model.iSortCol_0--;
                        }
                        switch (model.SortingDirection)
                        {
                            case SortDirection.Undefiened:
                                sqlCommand.Parameters.AddWithValue("@sortColumn", 4);
                                sqlCommand.Parameters.AddWithValue("@SortDirection", 1);
                                break;
                            case SortDirection.Ascending:
                                sqlCommand.Parameters.AddWithValue("@sortColumn", model.iSortCol_0);
                                sqlCommand.Parameters.AddWithValue("@SortDirection", 0);
                                break;
                            case SortDirection.Descending:
                                sqlCommand.Parameters.AddWithValue("@sortColumn", model.iSortCol_0);
                                sqlCommand.Parameters.AddWithValue("@SortDirection", 1);
                                break;
                            default:
                                break;
                        }
                     }
                    var pageSize = model == null ? 10 : ((model.iDisplayLength == 0) ? 10 : model.iDisplayLength);
                    var PageNumber = model == null ? 1 : ((model.iDisplayStart == 0) ? 1 : model.iDisplayStart) / pageSize + 1;
                    sqlCommand.Parameters.AddWithValue("@pageSize", pageSize);
                    sqlCommand.Parameters.AddWithValue("@PageNumber", PageNumber);
                },
                delegate (SqlDataReader reader)
                {

                    while (reader.Read())
                        response.AssetTransactions.Add(new AssetTransaction
                        {
                            AssetId = reader.GetString(0),
                            AssetName = reader.GetString(1),
                            AssetStatus = reader.GetInt32(2),
                            Location = reader.GetString(3),
                            Date_and_Time = reader.GetDateTime(4),
                            Direction = reader.GetString(5)
                        });
                    if(reader.NextResult() && reader.Read())
                    {
                        response.RowsCount = reader.GetInt32(0);
                    }
                });
            return response;
        }
        protected AssetResult ListAssets(GridModel model, AssetSearch search)
        {
            var response = new AssetResult();
            ExecuteReader("r_Asset_List",
                delegate (SqlCommand sqlCommand)
                {
                    if (search != null)
                    {
                        if (!string.IsNullOrEmpty(search.Id))
                        {
                            sqlCommand.Parameters.AddWithValue("@Id", search.Id);
                        }
                        
                        if (search.Status != 0)
                        {
                            sqlCommand.Parameters.AddWithValue("@status", search.Status);
                        }
                        if (!string.IsNullOrEmpty(search.Name))
                        {
                            sqlCommand.Parameters.AddWithValue("@name", search.Name);
                        }
                        if (!string.IsNullOrEmpty(search.CreateDate))
                        {
                            sqlCommand.Parameters.AddWithValue("@From", DateTime.Parse(search.CreateDate));
                            sqlCommand.Parameters.AddWithValue("@To", DateTime.Parse(search.CreateDate).AddHours(23).AddMinutes(59));
                        }
                        if (!string.IsNullOrEmpty(search.TagId))
                        {
                            sqlCommand.Parameters.AddWithValue("@TagId", search.TagId);
                        }
                        if (model.iSortCol_0 != 0)
                        {
                            model.iSortCol_0--;
                        }
                        switch (model.SortingDirection)
                        {
                            case SortDirection.Undefiened:
                                sqlCommand.Parameters.AddWithValue("@sortColumn", 4);
                                sqlCommand.Parameters.AddWithValue("@SortDirection", 1);
                                break;
                            case SortDirection.Ascending:
                                sqlCommand.Parameters.AddWithValue("@sortColumn", model.iSortCol_0);
                                sqlCommand.Parameters.AddWithValue("@SortDirection", 0);
                                break;
                            case SortDirection.Descending:
                                sqlCommand.Parameters.AddWithValue("@sortColumn", model.iSortCol_0);
                                sqlCommand.Parameters.AddWithValue("@SortDirection", 1);
                                break;
                            default:
                                break;
                        }
                    }
                    var pageSize = model == null ? 10 : ((model.iDisplayLength == 0) ? 10 : model.iDisplayLength);
                    var PageNumber = model == null ? 1 : ((model.iDisplayStart == 0) ? 1 : model.iDisplayStart) / pageSize + 1;
                    sqlCommand.Parameters.AddWithValue("@pageSize", pageSize);
                    sqlCommand.Parameters.AddWithValue("@PageNumber", PageNumber);
                },
                delegate (SqlDataReader reader)
                {

                    while (reader.Read())
                        response.Assets.Add(new AssetListModel
                        {
                            Id = reader.GetInt32(0),
                            AID = reader.GetString(1),
                            Name = reader.GetString(2),
                            Status = (AssetStatus)reader.GetInt32(3),
                            TagId = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                            CreateDate = reader.GetDateTime(5),
                            ImagePath =reader.IsDBNull(6)?string.Empty: reader.GetString(6),
                            Category=reader.GetString(7),
                            Department= reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                            Type =reader.GetString(9),
                            Cost=reader.GetDecimal(10),
                            PurchaseDate = reader.IsDBNull(11) ? DateTime.MinValue : reader.GetDateTime(11),
                            Location= reader.IsDBNull(12) ? string.Empty : reader.GetString(12)
                        });
                    if (reader.NextResult() && reader.Read())
                    {
                        response.RowsCount = reader.GetInt32(0);
                    }
                });
            return response;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}