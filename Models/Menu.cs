using System.ComponentModel.DataAnnotations.Schema;

namespace Developer.Models
{
    public class Menu
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Formulario {get; set;}
        public string Funcao {get; set;}
        public string Link {get; set;}
        public string Back {get; set;}		
		public string Front {get; set;}
		public string Layout {get; set;}
        public string ObsApp {get; set;}  
        public string OriginTable { get; set; }
        public string  DestinTable { get; set; }
        public string ObsTable  {get; set;}

        [ForeignKey("Projeto_Id")]
        public int Projeto_Id { get; set; }

        [ForeignKey("MenuPai_Id")]
        public int MenuPai_Id { get; set; }

				
    }
}