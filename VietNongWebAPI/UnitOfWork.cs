﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietNongWebAPI.Models;

namespace ClinicData
{
    public class UnitOfWork
    {
        private VietGrowthContext _context;
      

        public UnitOfWork()
        {
            _context = new VietGrowthContext();
        }
       
    }
}
