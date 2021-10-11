using CarFinder.Api.Mappers;
using CarFinder.Api.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarFinder.Controllers
{
    [ApiController]
    [Route("plate")]
    public class PlateController : ControllerBase
    {
        public PlateController()
        {
        }

        [HttpGet]
        public void Get()
        {
            IWebDriver webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://www.keplaca.com/");

            IWebElement plateInputElement = webDriver.FindElement(By.Id("sPlaca"));

            plateInputElement.SendKeys("LIS6088");

            IWebElement searchButtonElement = webDriver.FindElement(By.XPath("//input"));
            searchButtonElement.Submit();

            IWebElement carInfosTableElement = webDriver.FindElement(By.XPath("//table"));

            IList<IWebElement> tableRow = carInfosTableElement.FindElements(By.TagName("tr"));
            IList<IWebElement> rowTD;

            var carInfos = new List<CarInfo>();
            CarInfo carInfo = new();

            foreach (IWebElement row in tableRow)
            {
                rowTD = row.FindElements(By.TagName("td"));

                string column = rowTD[0].Text;
                string value = rowTD[1].Text;
                carInfos.Add(carInfo);

                column = column.Replace(":", "").Replace(" ", "");

                if (column.Equals("marca", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.Marca = value;
                    continue;
                }

                if (column.Equals("ano", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.Ano = value;
                    continue;
                }

                if (column.Equals("cor", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.Cor = value;
                    continue;
                }

                if (column.Equals("modelo", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.Modelo = value;
                    continue;
                }

                if (column.Equals("placa", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.Placa = value;
                    continue;
                }

                if (column.Equals("ano", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.Ano = value;
                    continue;
                }

                if (column.Equals("potencia", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.Potencia = value;
                    continue;
                }

                if (column.Equals("uf", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.UF = value;
                    continue;
                }

                if (column.Equals("município", StringComparison.OrdinalIgnoreCase))
                {
                    carInfo.Municipio = value;
                    continue;
                }

                Console.WriteLine($"{column} {value}");
            }

            this.WriteCSVFile("C:\\Users\\lgoncalves\\Desktop\\informacoes-dos-carros.csv", carInfos);
        }

        public List<CarInfo> ReadCSVFile(string location)
        {
            try
            {
                using var reader = new StreamReader(location, Encoding.Default);
                using var csv = new CsvReader(reader);

                csv.Configuration.RegisterClassMap<CarInfosMapper>();

                var records = csv.GetRecords<CarInfo>().ToList();

                return records;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void WriteCSVFile(string path, List<CarInfo> carInfos)
        {
            using StreamWriter sw = new(path, false, new UTF8Encoding(true));
            using CsvWriter cw = new(sw);

            cw.WriteHeader<CarInfo>();
            cw.NextRecord();

            foreach (CarInfo info in carInfos)
            {
                cw.WriteRecord<CarInfo>(info);
                cw.NextRecord();
            }
        }
    }
}
