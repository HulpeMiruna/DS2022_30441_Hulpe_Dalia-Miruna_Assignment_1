using EnergyPlatformProgram.Repository.Interfaces;
using EnergyPlatformProgram.Repository.Models;
using EnergyPlatformProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.Repository.Implementations
{
    public  class ConsumtionRepository : IConsumtionRepository
    {
        private readonly DbSet<ConsumptionEntity> _dbSet;

        private readonly ApplicationDbContext _context;

        public ConsumtionRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<ConsumptionEntity>();

            _context = context;
        }

        public async Task<List<ConsumptionEntity>> GetDeviceConsumtionAsync (Guid deviceId, DateTime date)
        {
            var consumption = await _dbSet.Where(c => c.DeviceId == deviceId && c.Date.Date == date.Date).ToListAsync();

            return consumption;
        }

        public async Task AddConsumtionAsync(List<ConsumptionEntity> consumption)
        {
            await _dbSet.AddRangeAsync(consumption);

            await _context.SaveChangesAsync();
        }
    }
}
