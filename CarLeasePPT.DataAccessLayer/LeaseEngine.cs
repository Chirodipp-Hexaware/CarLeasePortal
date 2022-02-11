using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class LeaseEngine
    {
        #region Public Methods and Operators

        public static async Task<FileRecord> GetAttachmentAsync(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var a = await e.LeaseAttachments.FindAsync(id);
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

        public static async Task<LeaseSearchListData> GetSearchCollectionAsync(int take, int skip, string pn, string ln,
            string tan, string bn, string vin, string lln, string lfn)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.CarLeases.Where(p => true);

                    if (!string.IsNullOrWhiteSpace(pn)) query = query.Where(v => v.PlateNumber.Contains(pn));
                    if (!string.IsNullOrWhiteSpace(ln)) query = query.Where(v => v.LeaseNumber.Contains(ln));
                    if (!string.IsNullOrWhiteSpace(vin)) query = query.Where(v => v.Vin.Contains(vin));
                    if (!string.IsNullOrWhiteSpace(lln)) query = query.Where(v => v.LesseeLastName.Contains(lln));
                    if (!string.IsNullOrWhiteSpace(lfn)) query = query.Where(v => v.LesseeFirstName.Contains(lfn));

                    if (!bn.IsNullOrWhiteSpace())
                        query = query.Where(v => v.TaxBillCarLeases.Any(b => b.TaxBill.BillNumber.Contains(bn)));

                    if (!tan.IsNullOrWhiteSpace())
                        query = query.Where(v =>
                            v.TaxBillCarLeases.Any(b => b.TaxBill.CollectorAccountNumber.Contains(tan)));

                    query = query.OrderBy(v => v.Vin).ThenBy(v => v.LeaseNumber);

                    var count = await query.CountAsync();
                    var recordList = await query.Skip(skip).Take(take).ToListAsync();

                    var totalResultsCount = e.CarLeases.Count();

                    var leaseSearchRecords = recordList.Select(lease => new LeaseSearchRecord
                    {
                        VIN = lease.CarMaster.Vin,
                        ModelYear = lease.CarMaster.ModelYear,
                        CarMake = lease.CarMaster.CarMake,
                        CarMasterId = lease.CarMasterId,
                        CarModel = lease.CarMaster.CarModel,
                        LeaseNumber = lease.LeaseNumber,
                        LesseeFirstName = lease.LesseeFirstName,
                        LesseeLastName = lease.LesseeLastName,
                        CarLeaseId = lease.CarLeaseId
                    }).ToList();

                    return new LeaseSearchListData
                    {
                        data = leaseSearchRecords,
                        recordsFiltered = count,
                        recordsTotal = totalResultsCount
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new LeaseSearchListData
                {
                    data = new List<LeaseSearchRecord>(),
                    recordsFiltered = 0,
                    recordsTotal = 0
                };
            }
        }

        public static async Task<CarLeaseRecord> GetCarLeaseAsync(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var record = await e.CarLeases.FindAsync(id);
                    if (record == null) return null;

                    var vlr = new CarLeaseRecord
                    {
                        ActualTerminationDate = record.ActualTerminationDate,
                        AssetNumber = record.AssetNumber,
                        AssetStatusName = record.AssetStatus.AssetStatusName,
                        BranchCode = record.BranchCode,
                        CommencementDate = record.CommencementDate,
                        CompanyCode = record.CompanyCode,
                        CompanyName = record.Company.CompanyName,
                        ExemptionCode = record.ExemptionCode,
                        LeaseNumber = record.LeaseNumber,
                        LeaseStatusName = record.LeaseStatus.LeaseStatusName,
                        LesseeFirstName = record.LesseeFirstName,
                        LesseeLastName = record.LesseeLastName,
                        OriginalCost = record.OriginalCost,
                        PayoffSource = record.PayoffSource,
                        PlateNumber = record.PlateNumber,
                        ProductUseCode = record.ProductUseCode,
                        RegistrationState = record.RegistrationState,
                        SaleDate = record.SaleDate,
                        SalesPrice = record.SalesPrice,
                        ScheduledTerminationDate = record.ScheduledTerminationDate,
                        TaxRecoveryCode = record.TaxRecoveryCode,
                        TerminationCode = record.TerminationCode,
                        CarLeaseId = record.CarLeaseId,
                        CarMasterId = record.CarMasterId,
                        Vin = record.CarMaster.Vin,
                        CarMake = record.CarMaster.CarMake,
                        CarModel = record.CarMaster.CarModel,
                        ModelYear = record.CarMaster.ModelYear,
                        LeaseAttachmentRecords = record.LeaseAttachments.Select(a => new LeaseAttachmentRecord
                        {
                            CreatedOnDateTime = a.CreatedOnDateTime,
                            AttachmentName = a.AttachmentName,
                            LeaseAttachmentId = a.LeaseAttachmentId,
                            LeaseAttachmentTypeName = a.LeaseAttachmentType.LeaseAttachmentName
                        }).ToList(),
                        PhysicalAddressRecords = record.PhysicalAddresses.OrderByDescending(a => a.EffectiveDate)
                            .Select(a => new PhysicalAddressRecord
                            {
                                EffectiveDateTime = a.EffectiveDate,
                                City = a.City,
                                County = a.County,
                                EndDate = a.EndDate,
                                PostalCode = a.PostalCode,
                                State = a.State,
                                StreetAddress = a.StreetAddress
                            }).ToList(),
                        CustomerAddressRecords = record.CustomerAddresses.OrderByDescending(a => a.EffectiveDate)
                            .Select(a => new CustomerAddressRecord
                            {
                                EffectiveDateTime = a.EffectiveDate,
                                City = a.City,
                                EndDate = a.EndDate,
                                PostalCode = a.PostalCode,
                                State = a.State,
                                StreetAddress = a.StreetAddress
                            }).ToList(),
                        TaxBillCarLeaseRecords = record.TaxBillCarLeases
                            .OrderByDescending(t => t.TaxBill.TaxYear).ThenBy(t => t.TaxBill.BillNumber).Select(t =>
                                new TaxBillCarLeaseRecord
                                {
                                    AccountNumber = t.TaxBill.CollectorAccountNumber,
                                    AssessedValue = t.AssessedValue,
                                    BaseTax = t.BaseTax,
                                    BillNumber = t.TaxBill.BillNumber ?? "No Bill Number",
                                    DecalRegistrationFee = t.DecalRegistrationFee,
                                    Discount = t.Discount,
                                    DueDate = t.TaxBill.DueDate,
                                    Interest = t.Interest,
                                    PaidDate = t.TaxBill?.TaxPayment?.PaymentDate,
                                    Penalty = t.Penalty,
                                    PptraCredit = t.PPTRACredit,
                                    TaxableValue = t.TaxableValue,
                                    TaxBillId = t.TaxBillId,
                                    TaxBillCarLeaseId = t.TaxBillCarLeaseId,
                                    TaxCollector = t.TaxBill.TaxCollector.TaxCollectorName,
                                    TaxYear = t.TaxBill.TaxYear,
                                    TotalAmountOwed = t.TotalAmountOwed,
                                    CarLeaseId = t.CarLeaseId,
                                    Vin = t.Vin
                                }).ToList()
                    };
                    return vlr;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        #endregion


        //private static Expression<Func<CarLease, bool>> PredicateWhere(string searchValue)
        //{
        //    var p = PredicateBuilder.New<CarLease>(true);
        //    if (string.IsNullOrWhiteSpace(searchValue)) return p;
        //    var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
        //    p = p.Or(s => searchTerms.Any(value => s.CarMaster.VIN.ToLower().Contains(value)));
        //    p = p.Or(s => searchTerms.Any(value => s.CarMaster.CarMake.ToLower().Contains(value)));
        //    p = p.Or(s => searchTerms.Any(value => s.CarMaster.CarModel.Contains(value)));
        //    p = p.Or(s => searchTerms.Any(value => s.LeaseNumber.Contains(value)));
        //    p = p.Or(s => searchTerms.Any(value => s.LesseeFirstName.Contains(value)));
        //    p = p.Or(s => searchTerms.Any(value => s.LesseeLastName.Contains(value)));
        //    return p;
        //}
    }
}