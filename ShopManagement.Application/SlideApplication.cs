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
        private readonly IFileUploader _fileUploader;
        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();

            var fileName = _fileUploader.Upload(command.Picture, "Slides");

            var slide = new Slide(fileName, command.PictureAlt, command.PictureTitle, command.Heading,
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
            
            _fileUploader.RemoveFile(slide.Picture);

            var fileName = _fileUploader.Upload(command.Picture, "Slides");

            slide.Edit(fileName, command.PictureAlt, command.PictureTitle, command.Heading,
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
