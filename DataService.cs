using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using static System.Net.WebRequestMethods;
using System.Security.Policy;
using Newtonsoft.Json.Linq;

namespace BasketBallStatApp
{
    /**
     * For converting responses into final objects I need two generic classes
     * that contain a single member named Data that is of Generic type.
     * One classes is instantiated by using simply the object (called APIResponseSingle) of the Generic type
     * while the other classes uses an array of Generic type (called APIResponseMultiple).
     * When converting the json response into concrete objects we do the following
     * APIResponseSingle<Player> JsonSerializer.Deserialize<ApiResponse<Player>>(jsonResponse);
    */
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public DataService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            if (apiKey == null)
            {
                _apiKey = "63beaf41-1c7a-4bfb-82c0-7181e86fbd84";
            }
            else
            {
                _apiKey = apiKey;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<APIResponseSingle<PlayerSeasonStats>?> GetPlayerSeasonStatsAsync(int playerId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://api.balldontlie.io/v1/players/{playerId}");
                return JsonConvert.DeserializeObject<APIResponseSingle<PlayerSeasonStats>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

        }
    }

    public class APIResponseSingle<T> where T : new()
    {
        public T Data { get; set; } = new T();
    }

    public class APIResponseMultiple<T> where T : new()
    {
        public T[] Data { get; set; } = Array.Empty<T>();

    }
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string JerseyNumber { get; set; } = string.Empty;
        public string College { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int DraftYear { get; set; }
        public int DraftRound { get; set; }
        public int DraftNumber { get; set; }

        public Team Team { get; set; } = Team.Empty;
    }
    public class Team
    {
        public int Id { get; set; };
        public string Conference { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;



        public static readonly Team Empty = new Team();

    }


    public class BasePlayerStats()
    {
        public int FieldGoalsMade { get; set; }
        public int FieldGoalsAttempted { get; set; }
        public float FieldGoalPercentage { get; set; }
        public int FieldGoal3PtMade { get; set; }
        public int FieldGoal3PtAttempted { get; set; }
        public float FieldGoal3PtPercentage { get; set; }

        public int FreeThrowsMade { get; set; }
        public int FreeThrowsAttempted { get; set; }
        public float FreeThrowPercentage { get; set; }
        public int OffensiveRebounds { get; set; }
        public int DefensiveRebounds { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }
        public int Points { get; set; }
        public string Minutes { get; set; } = string.Empty;

        public int PersonalFouls { get; set; }

    }

    public class PlayerSeasonStats : BasePlayerStats
    {
        public int GamesPlayed { get; set; }
        public int PlayerId { get; set; }
        public int Season { get; set; }

    }

    public class PlayerGameStats : BasePlayerStats
    {
        public int
    }


}
