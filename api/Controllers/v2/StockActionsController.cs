using System.ComponentModel.DataAnnotations;
using api.Utilities;
using core.Model;
using core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using core.Specifications.CustomSpecifications;
using core.Specifications.SpecificationParams;
using api.Dto;
using core.Specifications;
using core.Utils;

namespace api.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/BaseApiv2")]
[ApiVersion("2.0")]
public class StockActionsController : ControllerBase
{
        
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default120")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<IEnumerable<StockActionDto>> GetActions()
        {

            return new ()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Content = new List<StockActionDto> {
                    new StockActionDto
                    {
                        Isin = "ISIN1",
                        Title = "Action 1",
                        Symbol = "SYM1",
                        ListingMarket = "Market 1",
                        Index = "CAC40",
                        TradingCurrency = TradingCurrency.EUR,
                        Quantity = 100,
                        Misc = new { SomeProperty = "Value" },
                        MarketPrice = 92.32
                    },
                    new StockActionDto
                    {
                        Isin = "ISIN2",
                        Title = "Action 2",
                        Symbol = "SYM2",
                        ListingMarket = "Market 2",
                        Index = "SBF120",
                        TradingCurrency = TradingCurrency.EUR,
                        Quantity = 250,
                        Misc = new { SomeProperty = "Value" },
                        MarketPrice = 54.36
                    }
                }
            };
        }
}

