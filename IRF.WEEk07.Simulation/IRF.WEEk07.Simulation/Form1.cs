using IRF.WEEk07.Simulation.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRF.WEEk07.Simulation
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();

            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            numericUpDown1.Minimum = 2005;
            numericUpDown1.Maximum = 2024;
            numericUpDown1.Value = 2006;

        }

        private List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> deathprobabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathprobabilities.Add(new DeathProbability()
                    {
                        Age = int.Parse(line[1]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        P = GetDouble(line[2])
                    });
                }
            }

            return deathprobabilities;
        }

        private List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> birthprobabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {

                    var line = sr.ReadLine().Split(';');
                    birthprobabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = GetDouble(line[2])
                    });
                }
            }

            return birthprobabilities;
        }

        private List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }

        private double GetDouble(string value)
        {
            if (value == "1")
            {
                value = "1,0";
            }
            string[] num = value.Split(',');
            double doublenum = Convert.ToDouble(num[0] + "." + num[1]);
            return doublenum;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.P).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.AcceptsTab = true;
            Simulation();
        }
        private void Simulation()
        {
            try
            {
                Population = GetPopulation(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Nem jó népesség adatok");
            }

            int szimulaciovege = Convert.ToInt32(numericUpDown1.Value);
            //List<string[]> adatok = new List<string[]>();
            //string[] thisYearData = new string[3];
            
            // Végigmegyünk a vizsgált éveken
            for (int year = 2005; year <= szimulaciovege; year++)
            {
               
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();

                //kiváltja a string arraylistet
                richTextBox1.AppendText(String.Format("{3}Szimulációs év:{0} {3}\tFiúk:{1} {3}\tLányok:{2}{3}", year, nbrOfMales, nbrOfFemales, Environment.NewLine));

                //thisYearData[0] = year.ToString();
                //thisYearData[1] = nbrOfMales.ToString();
                //thisYearData[2] = nbrOfFemales.ToString();
                //adatok.Add(thisYearData);
            }

            //foreach (var item in adatok)
            //{
            //    richTextBox1.AppendText(String.Format("{3}Szimulációs év:{0} {3}\tFiúk:{1} {3}\tLányok:{2}{3}", item[0], item[1], item[2], Environment.NewLine));
            //}

            MessageBox.Show("Kész");
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }
    }
}
