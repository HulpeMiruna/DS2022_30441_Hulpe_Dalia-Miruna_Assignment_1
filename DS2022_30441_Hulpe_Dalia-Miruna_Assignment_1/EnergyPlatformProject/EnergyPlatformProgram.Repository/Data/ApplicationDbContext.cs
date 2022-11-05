using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EnergyPlatformProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> ApplicationUsers { set; get; }

        public DbSet<DeviceEntity> Devices { set; get; }

        public DbSet<ConsumptionEntity> Consumtion { set; get; }
    }
}
