using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartTrack.Reader.Model
{
    public class TagModel
    {
        public TagModel()
        {
            LastReadTime = DateTime.Now;
        }
        public int Antenna { get; set; }
        public string TID { get; set; }
        public DateTime LastReadTime { get; set; }
        public TagDirection Direction { get; set; }
    }

    public enum TagDirection
    {
        IN,
        OUT
    }
}
