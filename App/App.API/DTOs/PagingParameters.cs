using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App.API.DTOs
{
    public class PagingParameters
    {
        const int maxPageSize = 50;
        private int _pageNumber = 1;
        /// <summary>
        ///         enter page number default =1  min=1
        /// </summary>
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = (value < 1) ? 1 : value;
            }


        }
        private int _pageSize = 10;
        /// <summary>
        ///         enter page number  default=10  max=50
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
