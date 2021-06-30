using System;
using System.ComponentModel;

namespace Vaquinha.App.Models
{
    public class DonorViewModel
    {
        private string _name { get; set; }
        public string Name
        {
            get { return Anonymous ? "Doação anonima" : _name; }
            set { _name = value; }
        }

        [DisplayName("Doação anonima?")]
        public bool Anonymous { get; set; }

        [DisplayName("Mensagem de apoio")]
        public string Message { get; set; }

        public decimal Value { get; set; }

        public DateTime DateAndTime { get; set; }

        [DisplayName("Quando?")]
        public string DescriptionTime => GenerateDescriptionTime();

        private string GenerateDescriptionTime()
        {
            var description = string.Empty;

            if (DateAndTime != DateTime.MinValue)
            {
                TimeSpan interval = (DateTime.Now - DateAndTime);

                if (interval.Days > 365)
                {
                    var year = interval.Days / 365;
                    description = year + " ano";
                    if (year > 1)
                    {
                        description += "s";
                    }
                }
                else if (interval.Days > 30)
                {
                    var month = interval.Days / 30;
                    description = month + " mês";
                    if (month > 1)
                    {
                        description += "es";
                    }
                }
                else if (interval.Days > 0)
                {
                    description = interval.Days + " dia";
                    if (interval.Days > 1)
                    {
                        description += "s";
                    }
                }
                else if (interval.Hours > 0)
                {
                    description = interval.Hours + " hora";
                    if (interval.Hours > 1)
                    {
                        description += "s";
                    }
                }
                else if (interval.Minutes > 0)
                {
                    description = interval.Minutes + " minuto";
                    if (interval.Minutes > 1)
                    {
                        description += "s";
                    }
                }
                else
                {
                    return "nesse instante";
                }

                description += " atrás";
            }

            return description;
        }
    }
}