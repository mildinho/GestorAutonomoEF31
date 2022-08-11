using Newtonsoft.Json;
using GestorAutonomo.Models;

namespace GestorAutonomo.Session
{
    public class SessaoUsuario
    {

        private string _key = "Login.Usuario";
        private Sessao _sessao;

        public SessaoUsuario(Sessao sessao)
        {
            _sessao = sessao;
        }


        public void Login(Login usuario)
        {
           string obj =  JsonConvert.SerializeObject(usuario);
            _sessao.Cadastrar(_key, obj);
        }

        public Login GetLoginUsuario()
        {
            if (_sessao.Existe(_key))
            {
                string obj = _sessao.Consultar(_key);
                Login login = JsonConvert.DeserializeObject<Login>(obj); 

                return login;
            }
            else
            {
                return null;
            }
        }

        

        public void Logout()
        {
            _sessao.RemoverTodos();
        }

    }
}
