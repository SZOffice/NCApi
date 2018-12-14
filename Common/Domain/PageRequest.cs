
using System.ComponentModel.DataAnnotations;

namespace NCApi.Common.Domain
{
    public class PageRequest
    {
        private int page = 1;
        private int pageSize = 20;

        [Required]
        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        [Required]
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
    }
}