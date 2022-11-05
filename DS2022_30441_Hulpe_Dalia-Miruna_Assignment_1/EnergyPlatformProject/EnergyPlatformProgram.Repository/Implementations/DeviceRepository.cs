using EnergyPlatformProgram.Repository.Data;
using EnergyPlatformProgram.Repository.Data.EFCore;
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
    public class DeviceRepository : EfCoreRepository<DeviceEntity, ApplicationDbContext>, IDeviceRepository
    {
        private readonly DbSet<DeviceEntity> _dbSet;

        private readonly ApplicationDbContext _context;

        public DeviceRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = context.Set<DeviceEntity>();

            _context = context;
        }

        public DeviceEntity Update(DeviceEntity entity)
        {
            var local = _dbSet
                .Local
                .FirstOrDefault(d => d.Id.Equals(entity.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();

            return entity;
        }

        public async Task<DeviceEntity> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.Where(d => d.Id == id).FirstOrDefaultAsync();

            return entity;
        }

        public async Task<List<DeviceEntity>> GetByOwnerIdAsync(Guid id)
        {
            var entity = await _dbSet.Where(d => d.Owner.Id == id).ToListAsync();

            return entity;
        }

        public async Task<List<DeviceEntity>> GetAllWithOwnerAsync()
        {
            var entity = await _dbSet.Include(d => d.Owner).ToListAsync();

            return entity;
        }

        public async Task RemoveMultipleAsync(List<DeviceEntity> devices)
        {
             _dbSet.RemoveRange(devices);

            await SaveChangesAsync();
        }


        public async Task AddMultipleAsync(List<DeviceEntity> devices)
        {
            await _dbSet.AddRangeAsync(devices);

            await SaveChangesAsync();
        }
    }
}
