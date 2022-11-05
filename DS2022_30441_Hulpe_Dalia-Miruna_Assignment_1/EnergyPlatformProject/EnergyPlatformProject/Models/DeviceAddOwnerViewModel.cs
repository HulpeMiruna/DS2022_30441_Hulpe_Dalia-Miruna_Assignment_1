using EnergyPlatform.Repository.Entitys;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnergyPlatformProject.Models
{
    public class DeviceAddOwnerViewModel
    {
        public Guid DeviceId { get; set; }

        [Display(Name ="Users")]
        public List<SelectListItem> UserEmails { get; set; }

        public string OwnerEmail { get; set; }
    }
}
