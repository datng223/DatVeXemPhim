﻿using DatVeXemPhim.DataContext;

namespace DatVeXemPhim.Services.Implements
{
    public class BaseService
    {
        public readonly AppDbContext _context;
        public BaseService()
        {
            _context = new AppDbContext();
        }
    }
}
