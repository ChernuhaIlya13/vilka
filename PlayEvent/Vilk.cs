using System;
using System.ComponentModel.DataAnnotations;

namespace PlayEvent
{
    [Serializable]
    public class Vilk
    {
        [Key]
        public string TimeOfLife { get; set; }

        public string Percent { get; set; }

        public string BookmakerFirst { get; set; }

        public PlayEvt BookmakerFirstEvent { get; set; }

        public string BookmakerSecond { get; set; }

        public PlayEvt BookmakerSecondEvent { get; set; }

        public string[] Rate { get; set; }

        public string[] Coefficient { get; set; }

        public Vilk(string TimeOfLife, string Percent, string BookmakerFirst, PlayEvt BookMakerFirstEvent, string BookmakerSecond, PlayEvt BookmakerSecondEvent, string[] Rate, string[] Coefficient)
        {
            this.TimeOfLife = TimeOfLife;
            this.Percent = Percent;
            this.BookmakerFirst = BookmakerFirst;
            this.BookmakerFirstEvent = BookMakerFirstEvent;
            this.BookmakerSecond = BookmakerSecond;
            this.BookmakerSecondEvent = BookmakerSecondEvent;
            this.Rate = Rate;
            this.Coefficient = Coefficient;
        }
    }
}
