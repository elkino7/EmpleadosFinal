using ConectarDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UsusariosController : ApiController
    {
        private UsuariosEntities dbContext = new UsuariosEntities();
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            using (UsuariosEntities usuariosentities = new UsuariosEntities())
            {
                return usuariosentities.Usuarios.ToList();
            }
        }
        [HttpGet]
        public Usuario Get(int id)
        {
            using (UsuariosEntities usuariosentities = new UsuariosEntities())
            {
                return usuariosentities.Usuarios.FirstOrDefault(e => e.Id == id);
            }
        }
        [HttpPost]
        public IHttpActionResult AgregarUsuario([FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                dbContext.Usuarios.Add(usuario);
                dbContext.SaveChanges();
                return Ok(usuario);
            }
            else {
                return BadRequest();
            }
        }
        [HttpPut]
        public IHttpActionResult ActualizarUsuario(int id, [FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExiste = dbContext.Usuarios.Count(c => c.Id == id) > 0;
                if (usuarioExiste)
                {
                    dbContext.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                    return Ok();
                }
                else {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult eliminarUsuario(int id)
        {
            var usuarioExiste = dbContext.Usuarios.Find(id);
            if (usuarioExiste != null)
            {
                dbContext.Usuarios.Remove(usuarioExiste);
                dbContext.SaveChanges();
                return Ok(usuarioExiste);
            }
            else
            {
                return NotFound();
            }
        }
        

    }
}
