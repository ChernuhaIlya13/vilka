using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlayEvent
{
    [Serializable]
    public class PlayEvt
    {
        [Key]
        public string FirstPlayer { get; set; }

        public string SecondPlayer { get; set; }

        public ScoreGame Score { get; set; }

        public int? NumberOfSet { get; set; }

        public PlayEvt(string FirstPlayer, string SecondPlayer, ScoreGame Score, int NumberOfset)
        {
            this.FirstPlayer = FirstPlayer;
            this.SecondPlayer = SecondPlayer;
            this.Score = Score;
            this.NumberOfSet = NumberOfset;
        }
    }
}
