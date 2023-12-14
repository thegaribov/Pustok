using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Pustok.Areas.Admin.Controllers;


[Route("admin/cat-facts")]
[Authorize(Roles = $"{RoleNames.SuperAdmin},{RoleNames.Moderator}")]
[Area("Admin")]
public class CatFactController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly PustokDbContext _pustokDbContext;
    private readonly IConfiguration _configuration;

    public CatFactController(
        IHttpClientFactory httpClientFactory, 
        PustokDbContext pustokDbContext, 
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _pustokDbContext = pustokDbContext;
        _configuration = configuration;
    }


    [HttpGet]
    public IActionResult List()
    {
        var catFacts = _pustokDbContext.CatFacts.ToList();

        return View(catFacts);
    }

    [HttpGet("fetch-and-save", Name = "cat-fact-fetch")]
    public async Task<IActionResult> FetchAndSaveFactsAsync()
    {
        var endpoint = _configuration.GetSection("CatFactEndpoint").Value;
        var httpClient = _httpClientFactory.CreateClient();
        var httpResponse = await httpClient.GetAsync(endpoint);

        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
            var catFactModels = await httpResponse.Content.ReadFromJsonAsync<List<CatFactApiModel>>();

            foreach (var catFactModel in catFactModels)
            {
                var catFact = new CatFact
                {
                    Text = catFactModel.Text
                };

                _pustokDbContext.Add(catFact);
            }

            _pustokDbContext.SaveChanges();

        }
        else
        {

        }



        return RedirectToAction(nameof(List));
    }
}
