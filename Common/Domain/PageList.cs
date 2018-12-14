
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NCApi.Common.Domain
{
    public class PageList<T>
    {
        public int Page
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public long Total
        {
            get;
            set;
        }

        public int PageCount
        {
            get
            {
                if (Total > 0 && PageSize > 0 && Total > PageSize)
                {
                    return (int)Math.Ceiling((decimal)Total / (decimal)PageSize);
                }
                return 1;
            }
        }

        public IList<T> Rows
        {
            get;
            set;
        }

        public PageList()
        {
            Rows = new List<T>();
        }
    }
}