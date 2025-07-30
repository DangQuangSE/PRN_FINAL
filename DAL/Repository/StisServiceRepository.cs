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
        public void AddService(StisService service)
        {
            service.CreatedAt = DateTime.Now;
            service.Status = "ACTIVE";
            _context.StisServices.Add(service);
            _context.SaveChanges();
        }

        // Update
        public void UpdateService(StisService updatedService)
        {
            var existingService = _context.StisServices.FirstOrDefault(s => s.ServiceId == updatedService.ServiceId);
            if (existingService != null)
            {
                existingService.Description = updatedService.Description;
                existingService.Duration = updatedService.Duration;
                existingService.Price = updatedService.Price;
                existingService.ServiceName = updatedService.ServiceName;
                existingService.Status = updatedService.Status;
                existingService.Tests = updatedService.Tests;
                existingService.Type = updatedService.Type;
                existingService.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
            }
        }

        // Delete (Soft delete)
        public void DeleteService(int serviceId)
        {
            var service = _context.StisServices.FirstOrDefault(s => s.ServiceId == serviceId);
            if (service != null)
            {
                service.Status = "INACTIVE";
                service.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
