using DAL.DbModel;
using DAL.NhibernateBase;
namespace DAL.Repository
{
    public class UsuarioRepository : ManagerBase<Usuario, int>
    {
        public UsuarioRepository() { }
    }
}
