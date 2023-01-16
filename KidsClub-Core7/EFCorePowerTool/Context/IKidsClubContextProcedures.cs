﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using KidsClub.EFCorePowerTool.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace KidsClub.EFCorePowerTool.Context
{
    public partial interface IKidsClubContextProcedures
    {
        Task<int> usp_Content_DeleteAsync(int? ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> usp_Content_InsertAsync(int? ParentID, int? CategoryID, string FromID, string ToID, string Title, string ShortDescription, string LongDescription, string URL, string URLSlug, string Picture, string Icon, byte? DisplayOrder, byte? Qty, decimal? Price, decimal? DiscountPrice, bool? IsActive, bool? IsDefault, bool? IsArchived, DateTime? StartDate, DateTime? EndDate, DateTime? DateUpdated, DateTime? DateEntered, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> usp_Content_UpdateAsync(int? ID, int? ParentID, int? CategoryID, string FromID, string ToID, string Title, string ShortDescription, string LongDescription, string URL, string URLSlug, string Picture, string Icon, byte? DisplayOrder, byte? Qty, decimal? Price, decimal? DiscountPrice, bool? IsActive, bool? IsDefault, bool? IsArchived, DateTime? StartDate, DateTime? EndDate, DateTime? DateUpdated, DateTime? DateEntered, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_EventAttendResult>> usp_EventAttendAsync(int? ID, string FromID, int? ParentID, string Title, string Description, decimal? Price, DateTime? Arrived, DateTime? Left, byte? Guests, bool? Like, bool? Dislike, string Category, string Icon, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> usp_Events_DeleteAsync(int? ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> usp_Events_InsertAsync(int? CategoryID, string FromID, string Title, string ShortDescription, string LongDescription, string URL, string Picture, string Icon, decimal? Price, decimal? DiscountPrice, bool? IsActive, bool? IsDefault, bool? IsArchived, DateTime? StartDate, DateTime? EndDate, DateTime? DateEntered, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_Events_SelectResult>> usp_Events_SelectAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> usp_Events_UpdateAsync(int? ID, int? ParentID, int? CategoryID, string FromID, string Title, string ShortDescription, string LongDescription, string URL, string Picture, string Icon, decimal? Price, decimal? DiscountPrice, bool? IsActive, bool? IsDefault, bool? IsArchived, DateTime? StartDate, DateTime? EndDate, DateTime? DateEntered, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_Messages_DeletedResult>> usp_Messages_DeletedAsync(string ReceiverID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_Messages_InboxResult>> usp_Messages_InboxAsync(string ReceiverID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_Messages_SendResult>> usp_Messages_SendAsync(string SenderID, string ReceiverID, string Message, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_Messages_SentResult>> usp_Messages_SentAsync(string SenderID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_Messages_TrashResult>> usp_Messages_TrashAsync(int? MessageID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_TestimonialsResult>> usp_TestimonialsAsync(int? ID, int? ParentID, string FromID, string ToID, string Title, string Description, string LongDescription, int? Rating, int? Ranking, string Category, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> usp_User_UPDATEAsync(string Id, string UserName, string FirstName, string LastName, bool? EmailConfirmed, string PhoneNumber, string Picture, DateTime? DOB, bool? IsActive, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}