using IRF.Week06.SOAP.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace IRF.Week06.SOAP
{
    public partial class Form1 : Form
    {
        private List<RateData> Rates = new List<RateData>();
        private List<string> Currencies = new List<string>();
        public Form1()
        {
            InitializeComponent();
            

            var currMnbService = new MnbServiceReference.MNBArfolyamServiceSoapClient();
            var currRequest = new MnbServiceReference.GetCurrenciesRequestBody();
            var currResponse = currMnbService.GetCurrencies(currRequest);
            var currResult = currResponse.GetCurrenciesResult;

            var currXml = new XmlDocument();
            currXml.LoadXml(currResult);

            foreach (XmlNode node in currXml)
            {
                foreach (XmlNode currency in node.LastChild)
                {
                    Currencies.Add(currency.InnerText);
                }
            }

            dateTimePicker1.Value = new DateTime(2020,01,01);
            dateTimePicker2.Value = DateTime.Today;

            BindingSource bs = new BindingSource();
            bs.DataSource = Currencies;
            comboBox1.DataSource = bs.DataSource;

            RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();

            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-1);
                MessageBox.Show("Helytelen dátum.");
            }

            var mnbService = new MnbServiceReference.MNBArfolyamServiceSoapClient();
            var request = new MnbServiceReference.GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.Text.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            ReadXml(result);
            DrawChart();

            dataGridView1.DataSource = Rates.ToList();
        }

        private void DrawChart()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void ReadXml(string result)
        {
            // XML document létrehozása és az aktuális XML szöveg betöltése
            var xml = new XmlDocument();
            xml.LoadXml(result);

            // Végigmegünk a dokumentum fő elemének gyermekein
            foreach (XmlElement element in xml.DocumentElement)
            {
                // Létrehozzuk az adatsort és rögtön hozzáadjuk a listához
                // Mivel ez egy referencia típusú változó, megtehetjük, hogy előbb adjuk a listához és csak később töltjük fel a tulajdonságait
                var rate = new RateData();
                Rates.Add(rate);

                // Dátum
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                // Valuta
                var childElement = (XmlElement)element.ChildNodes[0];

                if (childElement == null)
                    continue;

                rate.Currency = childElement.GetAttribute("curr");

                // Érték

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                //var value = decimal.Parse(childElement.InnerText, style);

                var value = childElement.InnerText;
                value = value.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
                var val = decimal.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);

                if (unit != 0)
                    rate.Value = val / unit;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
