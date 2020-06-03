using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarTrack.Dashboard.Models
{
    public class LookUpBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class LookUpModel: LookUpBase
    {
        public LookUpType Type { get; set; }
    }
    public class LookUpResult
    {
        public LookUpResult()
        {
            LookUp = new List<LookUpBase>();
        }
        public List<LookUpBase> LookUp { get; set; }
        public int RowsCount { get; set; }
    }
    public enum LookUpType
    {
        Site  =0,
        Type =1,
        Department =2,
        Location =3,
        Category = 4,
        Antenna =5 ,
        Reader =6,
        User =7
    }
}