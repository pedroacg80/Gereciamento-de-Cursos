using Gerenciamento_Cursos.Exceptions;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Gerenciamento_Cursos.Applications.Regras
{
    public class Validar
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome eh obrigatorio");
            }
        }

        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainException("Email eh obrigatorio");
            }
        }
        public static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha)) //garante que a senha nao seja vazia
            {
                throw new DomainException("Senha eh obrigatoria");
            }

            using var sha256 = SHA256.Create(); //gera um hash SHA256 e devolve em byte[]
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

    }
}
