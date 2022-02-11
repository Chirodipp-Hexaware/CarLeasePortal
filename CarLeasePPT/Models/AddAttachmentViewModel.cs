using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class AddAttachmentViewModel
    {
        #region Public Properties

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_UploadedFiles_DisplayName")]
        public List<HttpPostedFileBase> UploadedFiles { get; set; }

        public int WorkItemActivityId { get; set; }
        public int WorkItemId { get; set; }

        #endregion
    }
}