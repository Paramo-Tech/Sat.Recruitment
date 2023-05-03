using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public interface ICalculateMoney
    {
        decimal CalculateAllocationToUser(decimal money);

    }
}

