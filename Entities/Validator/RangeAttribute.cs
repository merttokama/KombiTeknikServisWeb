using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Validator
{
    public class RangeAttribute : ValidationAttribute
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public override bool IsValid(object value)
        {
            this.EnsureLegalLengths();
            return (int)value <= MaxValue && (int)value >= MinValue;
        }
        /// <summary>
        /// Checks that MinimumLength and MaximumLength have legal values.  Throws InvalidOperationException if not.
        /// </summary>
        private void EnsureLegalLengths()
        {
            if (this.MaxValue < 0)
            {
                throw new InvalidOperationException("MaximumLength 0'dan büyük olmalı");
            }

            if (this.MaxValue < this.MinValue)
            {
                throw new InvalidOperationException("Max value Min value'dan büyük olmalıdır");
            }
        }
    }
}
