using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static partial class AuthenticationFailureEngine
    {
        #region Properties

        private static string PersonBlockSequence => ConfigurationManager.AppSettings["PersonBlockSequence"] ?? "";

        private static ReadOnlyCollection<BlockSequenceRecord> PersonBlockSequenceRecords =>
            GetBlockSequenceRecords(PersonBlockSequence);

        #endregion

        #region Public Methods and Operators

        public static async Task AddAuthenticationFailureRecordAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var failureRecord = await e.AuthenticationFailures.FindAsync(personId);

                    if (failureRecord != null)
                    {
                        failureRecord.FailureSequence++;
                        var fs = failureRecord.FailureSequence;
                        failureRecord.LastFailure = DateTime.Now;
                        e.Entry(failureRecord).State = EntityState.Modified;
                        await e.SaveChangesAsync();
                        var blockSequence = PersonBlockSequenceRecords;
                        var blockDuration = CheckFailureSequence(fs, blockSequence);
                        if (!blockDuration.Equals(0)) await CreateBlockRecordAsync(personId, blockDuration);
                    }
                    else
                    {
                        failureRecord = e.AuthenticationFailures.Create();
                        failureRecord.FailureSequence = 1;
                        failureRecord.LastFailure = DateTime.Now;
                        failureRecord.PersonId = personId;
                        e.AuthenticationFailures.Add(failureRecord);
                        await e.SaveChangesAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        public static async Task ClearAuthenticationFailureRecordAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var failureRecord = await e.AuthenticationFailures.FindAsync(personId);
                    if (failureRecord == null) return;
                    e.AuthenticationFailures.Remove(failureRecord);
                    await e.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        public static async Task ClearBlockRecordAsync(int personId, int overriddenByPersonId)
        {
            try
            {
                var now = DateTime.Now;
                using (var e = new HexaEntities())
                {
                    var pbr = await e.PersonBlocks.Where(p => p.PersonId.Equals(personId) && p.ExpirationDateTime > now)
                        .ToListAsync();
                    foreach (var p in pbr)
                    {
                        p.IsOverridden = true;
                        p.OverriddenOnDateTime = now;
                        p.OverriddenByPersonId = overriddenByPersonId;
                        e.Entry(p).State = EntityState.Modified;
                    }

                    await e.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        #endregion

        #region Methods

        private static int CheckFailureSequence(int failureSequence,
            IReadOnlyCollection<BlockSequenceRecord> sequenceRecords)
        {
            try
            {
                // Check to see if the sequence is populated. If not, return a non-blocking duration.
                if (!sequenceRecords.Any()) return 0;
                // Check to see if there's a sequence record that matches our failure count and return that block duration.
                foreach (var s in sequenceRecords)
                    if (failureSequence.Equals(s.FailureSequence))
                        return s.BlockMinutes;
                // Since no exact match, check to see if we have any failure sequences higher than the current number. If so, return a non-blocking duration.
                if (sequenceRecords.Any(t => t.FailureSequence > failureSequence)) return 0;
                // Since there are no sequences higher than our current sequence, determine the maximum sequence number and return the block duration for that record.
                var maxFailureSequence = sequenceRecords.Max(t => t.FailureSequence);
                return sequenceRecords.Single(t => t.FailureSequence.Equals(maxFailureSequence)).BlockMinutes;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return 0;
            }
        }


        private static async Task CreateBlockRecordAsync(int personId, int blockDuration)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var now = DateTime.Now;
                    e.PersonBlocks.Add(new PersonBlock
                    {
                        PersonId = personId,
                        EffectiveDateTime = now,
                        ExpirationDateTime = now.AddMinutes(blockDuration)
                    });
                    await e.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        private static ReadOnlyCollection<BlockSequenceRecord> GetBlockSequenceRecords(string blockSequence)
        {
            try
            {
                return
                    new ReadOnlyCollection<BlockSequenceRecord>(
                        blockSequence.Split('|').Select(p => p.Split(',')).Select(blockPair => new BlockSequenceRecord
                        {
                            FailureSequence = int.TryParse(blockPair[0], out var fs) ? fs : 5,
                            BlockMinutes = int.TryParse(blockPair[1], out var bm) ? bm : 30
                        }).ToList());
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<BlockSequenceRecord>
                {
                    new BlockSequenceRecord
                    {
                        FailureSequence = 5,
                        BlockMinutes = 30
                    }
                }.AsReadOnly();
            }
        }

        #endregion
    }
}