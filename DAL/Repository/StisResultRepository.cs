using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repository
{
    public class StisResultRepository
    {
        private readonly GenderHealthCareSystemContext _context;

        public StisResultRepository()
        {
            _context = new GenderHealthCareSystemContext();
        }

        
        public void AddResult(StisResult result)
        {
            result.ResultDate = DateTime.Now;
            _context.StisResults.Add(result);
            _context.SaveChanges();
        }

    }
}
