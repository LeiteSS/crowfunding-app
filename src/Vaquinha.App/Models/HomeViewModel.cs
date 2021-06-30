using System.Collections.Generic;
using System.ComponentModel;

namespace Vaquinha.App.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Donors = new List<DonorViewModel>();
            Institutions = new List<CauseViewModel>();
        }

        [DisplayName("Quanto falta arrecadar?")]
        public double RemainingAmount { get; set; }

        [DisplayName("Arrecadamos quanto?")]
        public double CollectedAmount { get; set; }

        [DisplayName("Percentual Arrecadado")]
        public double CollectedPercentage { get; set; }

        [DisplayName("Quantidade de Doadores")]
        public int QuantityHonors { get; set; }

        [DisplayName("Dias Restantes")]
        public int RemainingDays { get; set; }

        [DisplayName("Horas Restantes")]
        public int RemainingHours { get; set; }

        [DisplayName("Minutos Restantes")]
        public int RemainingMinutes { get; set; }

        public IEnumerable<DonorViewModel> Donors { get; set; }
        public IEnumerable<CauseViewModel> Institutions { get; set; }
    }
}