using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace PSC06.Models.ViewModels
{
    public class QueryViewModels
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Edad { get; set; }
        public int? idEstatus { get; set; }
    }
    public class AddUserViewModels
    {
        [Required]
        [Display(Name ="Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Nombre del Usuario")]
        public string Usuario { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(50, ErrorMessage ="digite correo", MinimumLength = 6)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirma Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden. Por favor, verifica.")]
        public string ConfirmaPassword { get; set; }

        [Required]
        [Display(Name = "Edad")]
        public int? Edad { get; set; }
    }

    public class EditUserViewModels
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Nombre del Usuario")]
        public string Usuario { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "digite correo", MinimumLength = 6)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirma Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden. Por favor, verifica.")]
        public string ConfirmaPassword { get; set; }


        [Required]
        [Display(Name = "Edad")]
        public int? Edad { get; set; }
    }
}