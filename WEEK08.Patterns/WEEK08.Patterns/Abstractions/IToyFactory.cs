using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEEK08.Patterns.Entities;

namespace WEEK08.Patterns.Abstractions
{
    public interface IToyFactory
    {
        Toy CreateNew();
    }
}
