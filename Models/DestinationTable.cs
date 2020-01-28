using System;

namespace Developer.Models
{
    public class DestinationTable
    {
        public string table {get; set;}
        public string obs {get; set;}
        public Menu m_Menu {get; set;}
    
		public DestinationTable(Menu objMenu){
            m_Menu = objMenu;
		}   
    }
}