﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Core.DomainObject
{
   public class DomainException: Exception
    {
        public DomainException(string message):base(message) {}

        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}