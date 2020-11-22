using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEEK08.Patterns.Abstractions;

namespace WEEK08.Patterns.Entities
{
    class PresentFactory : IToyFactory
    {
        public Color Ribbon { get; set; }
        public Color Box { get; set; }

        public Toy CreateNew()
        {
            return new Present(Ribbon, Box);
        }
    }
}
