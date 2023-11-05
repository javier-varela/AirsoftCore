using AirsoftCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirsoftCore.Data.Data.Repository.IRepository
{
    public interface IMonopolyRepository : IRepository<Monopoly>
    {

        void Update(Monopoly monopoly);

    }
}
