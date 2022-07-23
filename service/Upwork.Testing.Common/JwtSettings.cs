﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upwork.Testing.Common
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Secret { get; set; }

        public int ExpirationInDays { get; set; }
    }
}
