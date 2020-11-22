using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WEEK08.Patterns.Abstractions;
using WEEK08.Patterns.Entities;

namespace WEEK08.Patterns
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var toy in _toys)
            {
                toy.Movetoy();
                if (toy.Left > maxPosition)
                {
                    maxPosition = toy.Left;
                }
            }

            if (maxPosition > 1000)
            {
                var oldesttoy = _toys[0];
                mainPanel.Controls.Remove(oldesttoy);
                _toys.Remove(oldesttoy);
            }

        }
    }
}
