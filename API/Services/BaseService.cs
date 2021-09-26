using AutoMapper;
using Domaine.Interfaces;

namespace API.Services
{
    public class BaseService
    {
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        protected internal IMapper Mapper { get; set; }
        protected internal IUnitOfWork UnitOfWork { get; set; }
    }
}