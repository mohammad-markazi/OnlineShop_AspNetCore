using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Slide
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide command);
        OperationResult Edit(EditSlide command);

        EditSlide GetDetail(long id);

        List<SlideViewModel> GetAll();

        void Remove(long id);
        void Restore(long id);
    }
}
