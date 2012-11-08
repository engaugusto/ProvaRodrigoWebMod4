using System.Web;
using DAL.DbModel;
namespace BL
{
    public class SessionHandler
    {
        public Usuario GetUsuarioLogado()
        {
            return (Usuario)HttpContext.Current.Session["UsuarioLogado"];
        }
        public void SetUsuarioLogado(Usuario usuario)
        {
            HttpContext.Current.Session["UsuarioLogado"] = usuario;
        }
    }
}
