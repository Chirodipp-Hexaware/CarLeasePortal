using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class TaxBillEngine
    {
        #region Public Methods and Operators

        public static async Task<FileRecord> GetAttachmentAsync(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var a = await e.TaxBillAttachments.FindAsync(id);
                    if (a?.DocumentFileStoreStreamId == null) return null;
                    var g = a.DocumentFileStoreStreamId.Value;

                    var d = await e.ViewDocumentFileStores.SingleOrDefaultAsync(v => v.stream_id.Equals(g));
                    if (d == null) return null;
                    return new FileRecord
                    {
                        Bytes = d.file_stream,
                        FileName = d.name
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<TaxBillListData> GetSearchCollectionAsync(int take, int skip, string ln, string tan,
            string bn, string vin, bool includePaid)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.TaxBills.Where(t => true);

                    if (!includePaid) query = query.Where(taxBill => !taxBill.TaxPaymentId.HasValue);
                    if (!ln.IsNullOrWhiteSpace())
                        query = query.Where(taxBill =>
                            taxBill.TaxBillCarLeases.Any(taxBillCarLease =>
                                taxBillCarLease.CarLease.LeaseNumber.Contains(ln)));
                    if (!tan.IsNullOrWhiteSpace())
                        query = query.Where(taxBill => taxBill.CollectorAccountNumber.Contains(tan));
                    if (!bn.IsNullOrWhiteSpace()) query = query.Where(taxBill => taxBill.BillNumber.Contains(bn));
                    if (!vin.IsNullOrWhiteSpace())
                        query = query.Where(taxBill =>
                            taxBill.TaxBillCarLeases.Any(taxBillCarLease =>
                                taxBillCarLease.Vin.Contains(vin)));
                    query = query.OrderBy(taxBill => taxBill.BillNumber);

                    var count = await query.CountAsync();
                    var r = await query.Skip(skip).Take(take).ToListAsync();

                    var totalResultsCount = await e.TaxBills.CountAsync();

                    var taxBillListRecords = r.Select(q => new TaxBillListRecord
                    {
                        BillNumber = q.BillNumber ?? "No Bill Number",
                        CollectorAccountNumber = q.CollectorAccountNumber,
                        CompleteDate = q.DateComplete,
                        DueDate = q.DueDate,
                        IsComplete = q.DateComplete.HasValue,
                        PaymentDate = q.TaxPayment?.PaymentDate,
                        TaxAssessorState = q.TaxCollector.TaxAssessor.StateAbbreviation,
                        TaxBillId = q.TaxBillId,
                        TaxCollectorName = q.TaxCollector.TaxCollectorName,
                        TaxYear = q.TaxYear,
                        TotalAmountOwed = q.TotalAmountOwed
                    }).ToList();

                    return new TaxBillListData
                    {
                        data = taxBillListRecords,
                        recordsFiltered = count,
                        recordsTotal = totalResultsCount
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new TaxBillListData
                {
                    data = new List<TaxBillListRecord>(),
                    recordsFiltered = 0,
                    recordsTotal = 0
                };
            }
        }

        public static async Task<TaxBillAssetSummaryRecord> GetTaxBillAssetSummaryRecord(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var x = await e.TaxBillCarLeases.FindAsync(id);
                    // Return nothing if the TaxBillCarLease record isn't directly related to an existing lease
                    if (x?.CarLeaseId == null) return null;

                    var retRec = new TaxBillAssetSummaryRecord
                    {
                        AssessedValue = x.AssessedValue,
                        BaseTax = x.BaseTax,
                        BillNumber = x.TaxBill.BillNumber ?? "No Bill Number",
                        CollectorAccountNumber = x.TaxBill.CollectorAccountNumber,
                        CompletedDate = x.TaxBill.DateComplete,
                        DecalRegistrationFee = x.DecalRegistrationFee,
                        Discount = x.Discount,
                        Interest = x.Interest,
                        LeaseNumber = x.CarLease?.LeaseNumber ?? string.Empty,
                        LesseeFirstName = x.CarLease?.LesseeFirstName ?? string.Empty,
                        LesseeLastName = x.CarLease?.LesseeLastName ?? string.Empty,
                        PaidDate = x.TaxBill?.TaxPayment?.PaymentDate,
                        Penalty = x.Penalty,
                        PptraCredit = x.PPTRACredit,
                        TaxAssessorName = x.TaxBill.TaxCollector.TaxAssessor.TaxAssessorName,
                        TaxCollectorName = x.TaxBill.TaxCollector.TaxCollectorName,
                        TaxYear = x.TaxBill.TaxYear,
                        TotalAmountOwed = x.TotalAmountOwed,
                        Vin = x.Vin,
                        CarMake = x.CarLease?.CarMaster?.CarMake ?? string.Empty,
                        ModelYear = x.CarLease?.CarMaster?.ModelYear ?? string.Empty,
                        CarModel = x.CarLease?.CarMaster?.CarModel ?? string.Empty
                    };

                    var customerAddress = x.CarLease?.CustomerAddresses.OrderByDescending(d => d.EffectiveDate)
                        .FirstOrDefault();

                    if (customerAddress == null) return retRec;
                    retRec.CustomerAddress = customerAddress.StreetAddress;
                    retRec.CustomerCity = customerAddress.City;
                    retRec.CustomerState = customerAddress.State;
                    retRec.CustomerPostalCode = customerAddress.PostalCode;

                    return retRec;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<TaxBillRecord> GetTaxBillAsync(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var record = await e.TaxBills.FindAsync(id);
                    if (record == null) return null;
                    var t = new TaxBillRecord
                    {
                        TaxBillId = record.TaxBillId,
                        TaxYear = record.TaxYear,
                        TaxBillCarLeaseRecords = record.TaxBillCarLeases.Select(v =>
                            new TaxBillCarLeaseRecord
                            {
                                AssessedValue = v.AssessedValue,
                                CarLeaseId = v.CarLeaseId,
                                Vin = v.Vin,
                                TaxBillId = v.TaxBillId,
                                BaseTax = v.BaseTax,
                                DecalRegistrationFee = v.DecalRegistrationFee,
                                Discount = v.Discount,
                                Interest = v.Interest,
                                PptraCredit = v.PPTRACredit,
                                Penalty = v.Penalty,
                                TaxBillCarLeaseId = v.TaxBillCarLeaseId,
                                TaxableValue = v.TaxableValue,
                                TotalAmountOwed = v.TotalAmountOwed
                            }).ToList(),
                        TotalAmountOwed = record.TaxBillCarLeases.Sum(v => v.TotalAmountOwed),
                        ApExportDate = record.APExportDate,
                        BillNumber = record.BillNumber ?? "No Bill Number",
                        CollectorAccountNumber = record.CollectorAccountNumber,
                        CompletedDate = record.DateComplete,
                        DiscountDueDate = record.DiscountDueDate,
                        DueDate = record.DueDate,
                        InvoiceDate = record.InvoiceDate,
                        MonthsTaxed = record.MonthsTaxed,
                        PaymentDate = record.TaxPayment?.PaymentDate,
                        ReceivedDate = record.ReceivedDate,
                        TaxCollectorId = record.TaxCollectorId,
                        TaxPaymentId = record.TaxPaymentId,
                        TaxPeriodEnd = record.TaxPeriodEnd,
                        TaxPeriodStart = record.TaxPeriodStart,
                        TotalTaxableValue = record.TaxBillCarLeases.Sum(v => v.TaxableValue),
                        TaxAssessorName = record.TaxCollector.TaxAssessor.TaxAssessorName,
                        TaxAssessorState = record.TaxCollector.TaxAssessor.StateAbbreviation,
                        TaxCollectorName = record.TaxCollector.TaxCollectorName,
                        TaxBillAttachments = record.TaxBillAttachments.Select(v => new TaxBillAttachmentRecord
                        {
                            AttachmentName = v.AttachmentName,
                            CreatedByPersonFullName = v.PersonCreatedBy?.FullName ?? string.Empty,
                            CreatedOnDateTime = v.CreatedOnDateTime,
                            TaxBillAttachmentId = v.TaxBillAttachmentId,
                            TaxBillAttachmentTypeName = v.TaxBillAttachmentType.TaxBillAttachmentName
                        }).ToList()
                    };
                    return t;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<TaxAllocationRecord> GetTaxBillCarLeaseRecord(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var x = await e.TaxBillCarLeases.FindAsync(id);
                    // Return nothing if the TaxBillCarLease record isn't directly related to an existing lease
                    if (x?.CarLeaseId == null) return null;

                    var retRec = new TaxAllocationRecord
                    {
                        AccountNumber = x.TaxBill.CollectorAccountNumber,
                        ApExportDate = x.TaxBill.APExportDate,
                        AssessedValue = x.AssessedValue,
                        BaseTax = x.BaseTax,
                        BillNumber = x.TaxBill.BillNumber ?? "No Bill Number",
                        CollectorAccountNumber = x.TaxBill.CollectorAccountNumber,
                        CompletedDate = x.TaxBill.DateComplete,
                        DecalRegistrationFee = x.DecalRegistrationFee,
                        Discount = x.Discount,
                        DiscountDueDate = x.TaxBill.DiscountDueDate,
                        DueDate = x.TaxBill.DueDate,
                        Interest = x.Interest,
                        InvoiceDate = x.TaxBill.InvoiceDate,
                        LeaseNumber = x.CarLease?.LeaseNumber ?? string.Empty,
                        LesseeFirstName = x.CarLease?.LesseeFirstName ?? string.Empty,
                        LesseeLastName = x.CarLease?.LesseeLastName ?? string.Empty,
                        MonthsTaxed = x.TaxBill.MonthsTaxed,
                        PaidDate = x.TaxBill?.TaxPayment?.PaymentDate,
                        Penalty = x.Penalty,
                        PlateNumber = x.CarLease?.PlateNumber ?? string.Empty,
                        PptraCredit = x.PPTRACredit,
                        ReceivedDate = x.TaxBill.ReceivedDate,
                        TaxableValue = x.TaxableValue,
                        TaxAssessorName = x.TaxBill.TaxCollector.TaxAssessor.TaxAssessorName,
                        TaxAssessorState = x.TaxBill.TaxCollector.TaxAssessor.StateAbbreviation,
                        TaxBillId = x.TaxBillId,
                        TaxBillCarLeaseId = x.TaxBillCarLeaseId,
                        TaxCollectorId = x.TaxBill.TaxCollectorId,
                        TaxCollectorName = x.TaxBill.TaxCollector.TaxCollectorName,
                        TaxPaymentId = x.TaxBill.TaxPaymentId,
                        TaxPeriodEnd = x.TaxBill.TaxPeriodEnd,
                        TaxPeriodStart = x.TaxBill.TaxPeriodStart,
                        TaxYear = x.TaxBill.TaxYear,
                        TotalAmountOwed = x.TotalAmountOwed,
                        CarLeaseId = x.CarLeaseId,
                        Vin = x.Vin,
                        CarMake = x.CarLease?.CarMaster?.CarMake ?? string.Empty,
                        ModelYear = x.CarLease?.CarMaster?.ModelYear ?? string.Empty,
                        CarModel = x.CarLease?.CarMaster?.CarModel ?? string.Empty
                    };
                    return retRec;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        #endregion
    }
}