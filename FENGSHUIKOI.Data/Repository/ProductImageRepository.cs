﻿using FENGSHUIKOI.Data.Base;
using FENGSHUIKOI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FENGSHUIKOI.Data.Repository
{
    public class ProductImageRepository : GenericRepository<ProductImage>
    {
        public ProductImageRepository() { }
        public ProductImageRepository(NET1720_231_2_FENGSHUIKOIContext context)
        {
            _context = context;
        }
    }
}
