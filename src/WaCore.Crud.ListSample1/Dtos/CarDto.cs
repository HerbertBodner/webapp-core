﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Crud.ListSample1.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
