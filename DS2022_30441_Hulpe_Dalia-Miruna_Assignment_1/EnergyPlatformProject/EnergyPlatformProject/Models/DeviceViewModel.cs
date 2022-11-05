using EnergyPlatform.Repository.Entitys;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnergyPlatformProject.Models
{
    public class DeviceViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address required")]
        public string Address { get; set; }

        [Display(Name = "Max hours")]
        [Required(ErrorMessage = "Max hours required")]
        public string MaximuHourlyEnergyConsumtion { get; set; }

        public UserEntity Owner { get; set; }
    }
}
