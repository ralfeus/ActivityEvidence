using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityEvidence.EvidenceService
{
    public partial class ClaimCode
    {
        public bool OnCallStandby;

        public override bool Equals(object obj)
        {
            ClaimCode tmpObj = (ClaimCode)obj;
            return
                (tmpObj.Account == this.Account) &&
                (tmpObj.WorkItem == this.WorkItem) &&
                (tmpObj.CalledIn == this.CalledIn) &&
                (tmpObj.Overtime == this.Overtime) &&
                (tmpObj.OnCallStandby == this.OnCallStandby);
        }

        public override int GetHashCode()
        {
            return
                this.Account.GetHashCode() ^
                this.WorkItem.GetHashCode() ^
                this.CalledIn.GetHashCode() ^
                this.Overtime.GetHashCode() ^
                this.OnCallStandby.GetHashCode();
        }

        public override string ToString()
        {
            return this.Account + " " + this.WorkItem + (CalledIn ? " Called in" : "") + (Overtime ? " Overtime" : "") + (OnCallStandby ? " SB" : "");
        }

    }
}
