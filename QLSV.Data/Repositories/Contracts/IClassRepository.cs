﻿using QLSV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories.Contracts
{
    public interface IClassRepository
    {
        Task<List<Class>?> GetAllClassAsync();
        Task<Class?> GetClassByIdAsync(int id);
        Task<string?> GetClassNameByIdAsync(int id);
    }
}
