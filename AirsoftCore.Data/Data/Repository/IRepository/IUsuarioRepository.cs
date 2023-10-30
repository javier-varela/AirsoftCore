using AirsoftCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AirsoftCore.Data.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void BloquearUsuario(string id);

        void DesbloquearUsuario(string id);

    }
}
