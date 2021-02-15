using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
   public class PercentDto
    {
       public string numberofRoom { set; get; }
       public string PuseofRoom { set; get; }
        public string readyofRoom { set; get; }
        public string emptyandcleanofRoom { set; get; }
        public string PuseandcleanofRoom { set; get; }
    }
}
