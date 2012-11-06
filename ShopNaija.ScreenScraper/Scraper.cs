﻿using HtmlAgilityPack;
using ShopNaija.ScreenScraper.Scrapers;

namespace ShopNaija.ScreenScraper
{
	public class Scraper
	{
		private readonly string rootUrlToGetDataFrom;
		private string result;
		private readonly string baseAddress = "http://www.henryjamesshoes.com";

		public Scraper(string rootUrlToGetDataFrom, string baseAddress = "http://www.henryjamesshoes.com")
		{
			this.rootUrlToGetDataFrom = rootUrlToGetDataFrom;
			this.baseAddress = baseAddress;
		}

		public ScrapedData Scrape()
		{
			IScraperImplementation scraper;
			switch (baseAddress)
			{
				case "http://www.henryjamesshoes.com":
					scraper = new ShoeScraperImplementation(rootUrlToGetDataFrom, baseAddress);
					break;
				case "http://uk.monsoon.co.uk":
					scraper = new MonsoonScraperImplementation(rootUrlToGetDataFrom, baseAddress);
					break;
				case "http://www.zara.com":
					scraper = new ZaraScraperImplementation(rootUrlToGetDataFrom, baseAddress);
					break;
				default:
					scraper = new ShoeScraperImplementation(rootUrlToGetDataFrom, baseAddress);
					break;
			}

			result = scraper.GetHtmlString();
			var data = ApplyFilter(scraper);
			return data;
		}

		private ScrapedData ApplyFilter(IScraperImplementation scraper)
		{
			var document = new HtmlDocument();
			document.LoadHtml(result);
			var scrapedData = new ScrapedData
			{
			    Data = scraper.RecurseNodes(document)
			};

			return scrapedData;
		}
	}
}