using PetStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
