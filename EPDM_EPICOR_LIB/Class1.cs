using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPDM_EPICOR_LIB
{

    public class BillItem : IComparable<BillItem>
    {
        public string PartNumber { get; set; }

        public string Qty { get; set; }

        public override string ToString()
        {
            return PartNumber;
        }

        public int CompareTo(BillItem other)
        {
            return this.PartNumber.CompareTo(other.PartNumber);
        }
    }
}
