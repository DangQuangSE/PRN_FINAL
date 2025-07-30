using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class StisServiceService
    {
        private readonly DAL.Repository.StisServiceRepository _stisServiceRepository;
        public StisServiceService()
        {
            _stisServiceRepository = new DAL.Repository.StisServiceRepository();
        }
        public List<DAL.Entities.StisService> GetAllServices()
        {
            return _stisServiceRepository.GetAllServices();
        }
        public DAL.Entities.StisService GetServiceById(int serviceId)
        {
            return _stisServiceRepository.GetServiceById(serviceId);
        }
        public List<DAL.Entities.StisService> GetServicesByType(string type)
        {
            return _stisServiceRepository.GetServicesByType(type);
        }
        public void AddService(DAL.Entities.StisService service)
        {
            _stisServiceRepository.AddService(service);
        }

        public void UpdateService(DAL.Entities.StisService updatedService)
        {
            _stisServiceRepository.UpdateService(updatedService);
        }

        public void DeleteService(int serviceId)
        {
            _stisServiceRepository.DeleteService(serviceId);
        }
    }
}
