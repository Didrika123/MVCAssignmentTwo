using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    //container for the information you need in yourpeopleview

    public class PeopleViewModel
    {
        public enum SortMode //Maybe as enums?
        {
            NameAscending, NameDescending
        }
        public enum FilterMode
        {

        }
        public  SortMode Sort{ get; set; }
        public FilterMode Filter { get; set; }
        public List<Person> Persons { get; set; }
    }
}
