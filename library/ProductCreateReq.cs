﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productlib
{
    public class ProductCreateReq
    {
        public string Code {  get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
