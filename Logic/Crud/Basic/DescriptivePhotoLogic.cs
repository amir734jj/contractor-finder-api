using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces.Basic;
using Models.Entities.Common;

namespace Logic.Crud.Basic
{
    public class DescriptivePhotoLogic : BasicCrudLogicAbstract<DescriptivePhoto>, IDescriptivePhotoLogic
    {
        private readonly IDescriptivePhotoDal _descriptivePhotoDal;

        public DescriptivePhotoLogic(IDescriptivePhotoDal descriptivePhotoDal)
        {
            _descriptivePhotoDal = descriptivePhotoDal;
        }
        
        protected override IBasicCrudDal<DescriptivePhoto> GetBasicCrudDal()
        {
            return _descriptivePhotoDal;
        }
    }
}