using IRF.week05.VaR.Entities;
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

namespace IRF.week05.VaR
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;
        List<PortfolioItem> Portfolio = new List<PortfolioItem>();
        public Form1()
        {
            InitializeComponent();
            Ticks = context.Ticks.ToList();
            //dataGridView1.DataSource = Ticks;
            CreatePortfolio();
            //List<decimal> Nyereségek = new List<decimal>();
            //int intervalum = 30;
            //DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            //DateTime záróDátum = new DateTime(2016, 12, 30);
            //TimeSpan z = záróDátum - kezdőDátum;
            //for (int i = 0; i < z.Days - intervalum; i++)
            //{
            //    decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
            //               - GetPortfolioValue(kezdőDátum.AddDays(i));
            //    Nyereségek.Add(ny);
            //    Console.WriteLine(i + " " + ny);
            //}

            //var nyereségekRendezve = (from x in Nyereségek
            //                          orderby x
            //                          select x)
            //                            .ToList();
            //MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 5].ToString());

            var kapcsolt =
    from
        x in Ticks
    join
y in Portfolio
    on x.Index equals y.Index
    select new
    {
        Index = x.Index,
        Date = x.TradingDay,
        Value = x.Price,
        Volume = y.Volume
    };
            dataGridView1.DataSource = kapcsolt.ToList();
        }

  
        private void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;
        }

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolio)
            {
                var last = (from x in Ticks
                            where item.Index == x.Index.Trim()
                               && date <= x.TradingDay
                            select x)
                            .First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Nyereseg> Hozamok = new List<Nyereseg>();
            int intervalum = 30;
            DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;
            for (int i = 0; i < z.Days - intervalum; i++)
            {
                Nyereseg ny = new Nyereseg();
                decimal hozam = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));
                ny.Idoszak = kezdőDátum.ToString() + "-" + záróDátum.ToString();
                ny.Hozam = hozam;
                Hozamok.Add(ny);
            }

            var nyereségekRendezve = (from x in Hozamok
                                      orderby x.Hozam
                                      select new { x.Idoszak, x.Hozam });

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Txt Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream s = sfd.OpenFile();
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.WriteLine("Időszak;Nyereség");

                    foreach (var item in nyereségekRendezve)
                    {
                        sw.WriteLine(item.Idoszak.ToString() + ";" + item.Hozam.ToString());
                    }
                }
            }
        }
    }
}
