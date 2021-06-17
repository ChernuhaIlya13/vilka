namespace PlayEvent
{
    public class Vilk
    {
        public int TimeOfLife { get; set; }

        public double Percent { get; set; }

        public string BookmakerFirst { get; set; }

        public PlayEvt BookmakerFirstEvent { get; set; }

        public string BookmakerSecond { get; set; }

        public PlayEvt BookmakerSecondEvent { get; set; }

        public string Rate { get; set; }

        public double Coefficient { get; set; }

        public Vilk(int TimeOfLife,double Percent,string BookmakerFirst,PlayEvt BookMakerFirstEvent,string BookmakerSecond,PlayEvt BookmakerSecondEvent,string Rate,double Coefficient)
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
