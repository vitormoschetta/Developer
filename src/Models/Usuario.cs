using System;

namespace Developer.Models
{
    public class Usuario
    {        
        public Usuario(string nome, string email, string cpf, string senha, string salt, char ativo, int perfil_Id)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Senha = senha;
            Salt = salt;
            Ativo = ativo;
            Perfil_Id = perfil_Id;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public string Salt { get; set; }
        public char Ativo { get; set; }
        public int Perfil_Id { get; set; }
    }
}