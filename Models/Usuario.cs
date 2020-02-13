namespace Developer.Models
{
    public class Usuario
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Email  {get; set;}
        public string Cpf  {get; set;}
        public string Senha  {get; set;}

        //valor aletório/randomico concatenado a senha, para dificultar ataques Hacker de força bruta e dicionário:
        public string Salt{get; set;} 
        public char Ativo {get; set;}
        public int Perfil_Id {get; set;}
    }
}