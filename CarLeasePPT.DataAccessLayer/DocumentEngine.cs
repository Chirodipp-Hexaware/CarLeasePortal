using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class DocumentEngine
    {
        #region Public Methods and Operators

        public static Guid CreateFileStore(string relativePath, string fileName, byte[] streamBytes)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var streamId = new ObjectParameter("stream_id", Guid.NewGuid());
                    e.Document_Add(relativePath, fileName, streamBytes, streamId);
                    if (streamId.Value == null) return Guid.Empty;
                    var returnGuid = (Guid) streamId.Value;
                    return returnGuid;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return Guid.Empty;
            }
        }

        public static async Task<FileRecord> GetFileStore(Guid streamId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var documentFileStore =
                        await e.ViewDocumentFileStores.SingleOrDefaultAsync(v => v.stream_id.Equals(streamId));
                    if (documentFileStore == null) return null;
                    return new FileRecord
                    {
                        Bytes = documentFileStore.file_stream,
                        FileName = documentFileStore.name
                    };
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