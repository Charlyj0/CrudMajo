using Carlos_Jimenez.Services.Iservices;
using Domain.DTO;
using Domain.Entities;
using Majo29AV.Context;
using Microsoft.EntityFrameworkCore;

namespace Carlos_Jimenez.Services.Services
{
    public class RolServices : IRolServices
    {
        private readonly ApplicationDbContext _context;
        public RolServices(ApplicationDbContext context)
        {
            _context = context;
        }

        //Lista

        public async Task<Response<List<Rol>>> GetAll()
        {
            try
            {
                List<Rol> response = await _context.Roles.ToListAsync();

                return new Response<List<Rol>>(response, "Lista");

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles");
            }
        }

        public async Task<Response<Rol>> GetbyId(int id)
        {
            try
            {
                Rol rol = await _context.Roles.FirstOrDefaultAsync(x => x.PkRol == id);
                return new Response<Rol>(rol, "Rol Encontrado");

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public async Task<Response<Rol>> Create(RolRequest request)
        {
            try
            {
                Rol rol1 = new Rol()
                {
                    Nombre = request.Nombre,
                };

                _context.Roles.Add(rol1);
                await _context.SaveChangesAsync();

                return new Response<Rol>(rol1, "Rol creado con exito");

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }

        }

        public async Task<Response<Rol>> Delete(int id)
        {
            try
            {
                try
                {
                    Rol rol3 = await _context.Roles.FirstOrDefaultAsync(x => x.PkRol == id);
                    if (rol3 == null)
                    {
                        throw new Exception("Rol no encontrado");
                    }
                    _context.Roles.Remove(rol3);
                    await _context.SaveChangesAsync();

                    return new Response<Rol>(rol3, "Rol eliminado");


                }
                catch (Exception ex)
                {
                    throw new Exception("Ocurrio un error " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public async Task<Response<Rol>> Update(RolRequest rol, int id)
        {
            try
            {
                try
                {
                    Rol rol2 = await _context.Roles.FirstOrDefaultAsync(x => x.PkRol == id);

                    if (rol2 == null)
                    {
                        throw new Exception("Rol no encontrado");
                    }

                    rol2.Nombre = rol.Nombre;

                    _context.Roles.Update(rol2);

                    await _context.SaveChangesAsync();

                    return new Response<Rol>(rol2, "Rol actualizado correctamente");

                }
                catch (Exception ex)
                {
                    throw new Exception("Ocurrio un error " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }
    }
}
