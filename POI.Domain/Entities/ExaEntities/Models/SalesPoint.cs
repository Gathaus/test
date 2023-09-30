using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using POI.Domain.Entities;

namespace POI.Domain.Entities.ExaEntities.Models
{
    public class SalesPoint 
    {
        public SalesPoint()
        {
            RowGuid = Guid.NewGuid();
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int ChannelId { get; set; }

        public int CityId { get; set; }

        public int CountyId { get; set; }

        public int? BrickId { get; set; }

        public int? CustomerTypeId { get; set; }

        public int? DistributionTypeId { get; set; }

        public int? LocationSiteId { get; set; }

        public int? OutletTypeId { get; set; }

        public int? OutletFormatForMobileId { get; set; }

        public int? PartnershipSegmentId { get; set; }

        public int? RegionId { get; set; }

        public int? RetailerId { get; set; }

        public int? UserId_ReportedBy { get; set; }

        public int? SesGroupId { get; set; }

        public int? StoreSizeId { get; set; }

        public int? ValueSegmentId { get; set; }

        public int? WarehouseId { get; set; }

        [StringLength(1000)]
        public string? SearchText { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string? NameOnBoard { get; set; }

        [StringLength(100)]
        public string? CompanySalesPointNumber { get; set; }

        [StringLength(100)]
        public string? CompanySalesPointNumber2 { get; set; }

        [StringLength(100)]
        public string? CompanySalesPointNumber3 { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public string? TimeZone { get; set; }

        public string? AdminNotes { get; set; }

        //[JsonConverter(typeof(DbGeometryConverter))]
        public Point? Geography { get; set; }

        public int? CheckDurationInMinutes { get; set; }

        public string? SendEmailTo_Audit { get; set; }

        public bool IsIgnoredByAutoCancel { get; set; }

        public bool IsReported { get; set; }

        public bool IsPassive { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public Guid? RowGuid { get; set; }
        public int? POIId { get; set; }
        public virtual City City { get; set; }
        public virtual County County { get; set; }

        #region IAuditInfo Members

        public int? UserId_CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public int? UserId_LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDateTime { get; set; }

        public DateTime? ReportedDateTime { get; set; }

        public DateTimeOffset? ReportedDateTimeLocal { get; set; }


        #endregion IAuditInfo Members

 

    }
}