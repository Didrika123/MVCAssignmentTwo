using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public interface IHasIdAndName
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
