using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestPlantilla.Models
{
    public class AutomovilCE
    {
        [Required]
        [Display(Name = "Ingresa marca")]
        public string Marca { get; set; }
        [Required]
        [Display(Name = "Ingresa modelo")]
        public string Modelo { get; set; }
        [Required]
        [Display(Name = "Ingresa color")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Ingresa transmision")]
        public string TipoTransmision { get; set; }
        [Required]
        [Display(Name = "Ingresa la estetica")]
        public string Estetica { get; set; }
    }
}