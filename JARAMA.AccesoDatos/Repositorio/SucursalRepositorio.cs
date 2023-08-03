using JAMARA.Data;
using JAMARA.Modelos;
using JARAMA.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARAMA.AccesoDatos.Repositorio
{
    public class SucursalRepositorio : Repositorio<Sucursal>, ISucursalRepositorio
    {
        private readonly ApplicationDbContext _db;
        public SucursalRepositorio(ApplicationDbContext db) : base(db) 
        {
            db = _db;
        }
        public void Actualizar(Sucursal sucursal)
        {
            var sucursalBD = _db.Sucursales.FirstOrDefault(b => b.Id == sucursal.Id);
        if (sucursalBD != null)
            {
                sucursalBD.Nombre = sucursal.Nombre;
                sucursalBD.Descripcion = sucursal.Descripcion;
                sucursalBD.Estado = sucursal.Estado;
                _db.SaveChanges();
            }
        }
    }
}
