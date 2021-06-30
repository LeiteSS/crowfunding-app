using System.ComponentModel;

namespace Vaquinha.App.Models
{
    public class PersonViewModel
    {
        private string _name { get; set; }
        public string Name
        {
            get { return Anonymous ? "Doação anonima" : _name; }
            set { _name = value; }
        }

        public string Email { get; set; }

        [DisplayName("Doação anônima")]
        public bool Anonymous { get; set; }

        public string Message { get; set; }
    }
}