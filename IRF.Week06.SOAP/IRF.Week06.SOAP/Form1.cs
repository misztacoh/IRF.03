﻿using IRF.Week06.SOAP.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public Form1()
        {
            InitializeComponent();

            comboBox1.Text = "EUR";
            dateTimePicker1.Value = new DateTime(2020,01,01);
            dateTimePicker2.Value = DateTime.Today;


            //// A változó deklarációk jobb oldalán a "var" egy dinamikus változó típus.
            //// A "var" változó az első értékadás pillanatában a kapott érték típusát veszi fel, és később nem változtatható.
            //// Jelen példa első sora tehát ekvivalens azzal, ha a "var" helyélre a MNBArfolyamServiceSoapClient-t írjuk.
            //// Ebben a formában azonban olvashatóbb a kód, és változtatás esetén elég egy helyen átírni az osztály típusát.
            //var mnbService = new MnbServiceReference.MNBArfolyamServiceSoapClient();

            //var request = new MnbServiceReference.GetExchangeRatesRequestBody()
            //{
            //    currencyNames = "EUR",
            //    startDate = "2020-01-01",
            //    endDate = "2020-06-30"
            //};

            //// Ebben az esetben a "var" a GetExchangeRates visszatérési értékéből kapja a típusát.
            //// Ezért a response változó valójában GetExchangeRatesResponseBody típusú.
            //var response = mnbService.GetExchangeRates(request);

            //// Ebben az esetben a "var" a GetExchangeRatesResult property alapján kapja a típusát.
            //// Ezért a result változó valójában string típusú.
            //var result = response.GetExchangeRatesResult;

            RefreshData();

            dataGridView1.DataSource = Rates.ToList(); 
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
                rate.Currency = childElement.GetAttribute("curr");

                // Érték
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
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
    }
}
