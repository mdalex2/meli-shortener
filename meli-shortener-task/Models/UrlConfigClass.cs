using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace meli.Models
{
    public class UrlConfigClass
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "URL Larga")]
        public string UrlLarga { get; set; }

        public string UrlCorta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)] // para visualizar bien la fecha en el date picker del html5
        public Nullable<System.DateTime> FechaCreacion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)] // para visualizar bien la fecha en el date picker del html5
        public Nullable<System.DateTime> FechaExpira { get; set; }
        public bool Habilitado { get; set; }
        public int NumVisitas { get; set; }
    }
}
