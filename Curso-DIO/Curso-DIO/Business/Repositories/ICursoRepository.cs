using Curso_DIO.Business.Entities;
using System.Collections.Generic;

namespace Curso_DIO.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();

        IList<Curso> ObterPorUsuario(int codigoUsuario);



    }
}
