using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Validation
{
    public class ValidationModel
    {
        public bool IsValid { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
