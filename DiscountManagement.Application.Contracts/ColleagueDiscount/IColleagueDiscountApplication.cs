using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);

        EditColleagueDiscount GetDetail(long id);

        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel model);

        void Remove(long id);

        void Restore(long id);
    }
}
