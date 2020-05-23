using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TableTalk.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}
		public int QuestionsCount;
		public void OnGet()
		{
			string URL = "https://tabletalkwebapi.azurewebsites.net";

			HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true })
			{
				BaseAddress = new Uri(URL)
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var path = $"{URL}/questions";

			var response = client.GetAsync(path);
			var questionsCount = response.Result.Content.ReadAsStringAsync().Result;
			QuestionsCount = int.Parse(Regex.Replace(questionsCount, @"[\D-]", string.Empty));
		}
	}
}