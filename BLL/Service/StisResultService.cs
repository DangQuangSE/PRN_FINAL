using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class StisResultService
    {
        private readonly DAL.Repository.StisResultRepository _stisResultRepository;
        public StisResultService()
        {
            _stisResultRepository = new DAL.Repository.StisResultRepository();
        }

        public void AddResult(DAL.Entities.StisResult result)
        {
            _stisResultRepository.AddResult(result);
        }
    }
}
