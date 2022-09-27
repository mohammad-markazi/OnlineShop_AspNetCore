using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();

            var slide = new Slide(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);

            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();

            var slide = _slideRepository.Get(command.Id);
            if (slide is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditSlide GetDetail(long id)
        {

            var slide = _slideRepository.Get(id);
            return new EditSlide()
            {
                BtnText = slide.BtnText,
                Heading = slide.Heading,
                Title = slide.Title,
                Text = slide.Text,
                Id = slide.Id,
                Picture = slide.Picture,
                PictureTitle = slide.PictureTitle,
                PictureAlt = slide.PictureAlt,
                Link = slide.Link
            };
        }

        public List<SlideViewModel> GetAll()
        {
           return _slideRepository.GetAll().Select(x=>new SlideViewModel()
           {
               Picture = x.Picture,
               Heading = x.Heading,
               Id = x.Id,
               Title = x.Title,
               IsRemoved = x.IsRemoved,
               CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture)
           }).ToList();
        }

        public void Remove(long id)
        {
            var slide = _slideRepository.Get(id);

            slide.Removed();

            _slideRepository.SaveChanges();
        }

        public void Restore(long id)
        {
            var slide = _slideRepository.Get(id);

            slide.Restore();

            _slideRepository.SaveChanges();
        }
    }
}
