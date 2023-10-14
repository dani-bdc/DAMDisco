using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAMDisco.Data
{
    public class Person
    {
        public string Name { get; set; } = "";
        public int DiscoTime { get; set; } = 0;
        public int QueueMaxTime {  get; set; } = 0;
        
        //Converts QueueMaxTime to Duration String
        public string QueueMaxDurationTime
        {
            get
            {
                return TimeSpan.FromSeconds(QueueMaxTime).ToString();
            }
        }
        
        // Converts DiscoTime to Duration String
        public string DiscoDurationTime
        {
            get
            {
                return TimeSpan.FromSeconds(DiscoTime).ToString();
            }
        }
    }
}
