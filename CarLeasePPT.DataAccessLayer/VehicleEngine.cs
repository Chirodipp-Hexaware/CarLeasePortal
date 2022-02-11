using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class CarEngine
    {
        #region Public Methods and Operators

        public static async Task<CarListData> GetCollectionAsync(string searchBy, int take, int skip, string sortBy,
            bool sortDir)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = PredicateWhere(searchBy);

                    var query = e.CarMasters.AsExpandable().Where(p);

                    switch (sortBy)
                    {
                        case "Vin":
                            query = sortDir
                                ? query.OrderBy(v => v.Vin)
                                : query.OrderByDescending(v => v.Vin);
                            break;
                        default:
                            query = query.OrderBy(v => v.Vin);
                            break;
                    }

                    var count = await query.CountAsync();
                    var recordList = await query.Skip(() => skip).Take(() => take).ToListAsync();

                    var totalResultsCount = e.CarMasters.Count();

                    var carMasterRecords = recordList.Select(vmr => new CarMasterRecord
                    {
                        Vin = vmr.Vin,
                        ModelYear = vmr.ModelYear,
                        CarMake = vmr.CarMake,
                        CarMasterId = vmr.CarMasterId,
                        CarModel = vmr.CarModel,
                        CarLeaseRecords = vmr.CarLeases.Select(vl => new CarLeaseRecord
                        {
                            LeaseNumber = vl.LeaseNumber,
                            LesseeFirstName = vl.LesseeFirstName,
                            LesseeLastName = vl.LesseeLastName,
                            CarLeaseId = vl.CarLeaseId,
                            CarMasterId = vl.CarMasterId
                        }).ToList()
                    }).ToList();

                    return new CarListData
                    {
                        data = carMasterRecords,
                        recordsFiltered = count,
                        recordsTotal = totalResultsCount
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new CarListData
                {
                    data = new List<CarMasterRecord>(),
                    recordsFiltered = 0,
                    recordsTotal = 0
                };
            }
        }

        public static async Task<Select2Data> GetSelect2CollectionAsync(string term, int page)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    const int take = 20;
                    var skip = (page - 1) * take;
                    var baseQuery = e.CarMasters.Where(v => v.Vin.Contains(term)).OrderBy(v => v.Vin);
                    var query = baseQuery.Skip(skip).Take(take);
                    var count = await baseQuery.CountAsync();
                    var records = await query.ToListAsync();

                    var select2Data = records.Select(r => new Select2Record
                    {
                        id = r.CarMasterId,
                        text = r.Vin
                    }).ToList();

                    return new Select2Data
                    {
                        results = select2Data,
                        pagination = new Select2Pagination
                        {
                            more = page * take < count
                        }
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new Select2Data();
            }
        }

        #endregion

        #region Methods

        private static Expression<Func<CarMaster, bool>> PredicateWhere(string searchValue)
        {
            var p = PredicateBuilder.New<CarMaster>(true);
            if (string.IsNullOrWhiteSpace(searchValue)) return p;
            var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
            p = p.Or(s => searchTerms.Any(value => s.Vin.ToLower().Contains(value)));
            return p;
        }

        #endregion
    }
}