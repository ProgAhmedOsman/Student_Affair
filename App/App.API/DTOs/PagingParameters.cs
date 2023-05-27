using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App.API.DTOs
{
    public class PagingParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
