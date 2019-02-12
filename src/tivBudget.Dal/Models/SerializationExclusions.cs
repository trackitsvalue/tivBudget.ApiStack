using System;
using System.Collections.Generic;
using System.Text;

namespace tivBudget.Dal.Models
{
    public partial class Account
    {
        public bool ShouldSerializeOwner { get; } = false;
        public bool ShouldSerializeAccountActualRecurrences { get; } = false;
        public bool ShouldSerializeAccountTemplate { get; } = false;
        public bool ShouldSerializeAccountType { get; } = false;
    }

    public partial class BudgetCategory
    {
        public bool ShouldSerializeBudget { get; } = false;
        public bool ShouldSerializeCategoryTemplate { get; } = false;
    }
}
