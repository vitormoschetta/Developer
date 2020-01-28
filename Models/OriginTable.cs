using System;

namespace Developer.Models
{
    public class OriginTable
    {
        public string table {get; set;}
        public string obs {get; set;}
        public Menu m_Menu {get; set;}
    
		public OriginTable(Menu objMenu){
            m_Menu = objMenu;
		}
    }
}