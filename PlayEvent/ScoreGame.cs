using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayEvent
{
    [Serializable]
    public class ScoreGame
    {
        [Key]
        public int FirstScore { get; set; } = 0;
        public int SecondScore { get; set; } = 0;
    }
}
