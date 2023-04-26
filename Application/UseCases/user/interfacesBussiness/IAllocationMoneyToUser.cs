using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.user.interfacesBussiness
{
    public interface IAllocationMoneyToUser
    {
        decimal CalculateAllocationToUser(decimal money);
    }
}
