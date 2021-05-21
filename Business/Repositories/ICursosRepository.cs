using System.Collections.Generic;
using CursoAPI.Business.Entities;

namespace CursoAPI.Business.Repositories
{
    public interface ICursosRepository
    {
        void Adicionar(Curso curso);
        void Commit();
        IList<Curso> ObterPorUsuario(int codigoUsuario);
    }
}