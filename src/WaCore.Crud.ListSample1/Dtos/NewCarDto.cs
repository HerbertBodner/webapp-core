using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Crud.ListSample1.Dtos
{
    public class NewCarDto
    {
        [Required]
        public string Model { get; set; }

        [Required]
        public string Brand { get; set; }
    }
}
