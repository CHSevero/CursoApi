using System.Collections.Generic;
using System.Linq;
using CursoAPI.Business.Entities;
using CursoAPI.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CursoAPI.Infraestruture.Data.Repositories
{
    public class CursosRepository : ICursosRepository
    {
        private readonly CursoDbContext _contexto;
        public CursosRepository(CursoDbContext contexto)
        {
            _contexto = contexto;   
        }
        public void Adicionar(Curso curso)
        {
            _contexto.Curso.Add(curso);
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public IList<Curso> ObterPorUsuario(int codigoUsuario)
        {
            return _contexto.Curso.Include(i => i.Usuario).Where(w => w.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}