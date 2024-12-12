using Ball_Game_API.Data;
using Ball_Game_API.DTO.Request;
using Ball_Game_API.DTO.Response;
using Ball_Game_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal.Postgres;

namespace FatihTestServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ScoreHistoryController : Controller
    {
        private readonly IScoreHistoryService _scoreHistoryService;
        

        public ScoreHistoryController(IScoreHistoryService scoreHistoryService)
        {
            _scoreHistoryService = scoreHistoryService;
          
        }



        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var test = await _scoreHistoryService.GetScoreHistoriesAsync();
            return Ok(test);
        }



        [HttpPost]
        public async Task<ActionResult<ScoreHistoryResponse>> AddHistory(ScoreHistoryRequest scoreHistoryRequest)
        {

            var response = await _scoreHistoryService.InsertScoreHistoryAsync(scoreHistoryRequest);

            return Ok(response);
        }



    }

}

