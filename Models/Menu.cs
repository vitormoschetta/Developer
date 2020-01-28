using System;
using System.Collections.Generic;

namespace Developer.Models
{
    public class Menu
    {
        public string nome {get; set;}
        public string descricao {get; set;}
        public string formulario {get; set;}
        public string link {get; set;}
        public string obs {get; set;}
        public string back {get; set;}		
        public string obsBack {get; set;}
		public string front {get; set;}
        public string obsFront {get; set;}
		public string layout {get; set;}
        public string obsLayout {get; set;}

        public int? MenuId { get; set; }        
        public ICollection<Menu> SubMenu { get; set; }
				
    }
}