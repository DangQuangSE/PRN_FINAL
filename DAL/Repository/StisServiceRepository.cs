using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class StisServiceRepository
    {
        private readonly GenderHealthCareSystemContext _context;

        public StisServiceRepository()
        {
            _context = new GenderHealthCareSystemContext();
        }

        public List<StisService> GetAllServices()
        {
            return _context.StisServices
                .Where(s => s.Status == "active")
                .OrderBy(s => s.ServiceName)
                .ToList();
        }

        public StisService GetServiceById(int serviceId)
        {
            return _context.StisServices.FirstOrDefault(s => s.ServiceId == serviceId);
        }

        public List<StisService> GetServicesByType(string type)
        {
            return _context.StisServices
                .Where(s => s.Type == type && s.Status == "active")
                .OrderBy(s => s.ServiceName)
                .ToList();
        }
    }
}
