using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.Repository.Interfaces;
using EnergyPlatformProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<UserEntity> _dbSet;

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<UserEntity>();

            _context = context;
        }

        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            var user = await _dbSet.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

            return user;
        }
    }
}
