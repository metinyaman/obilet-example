using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OBilet.Core.BusJourneys;
using OBilet.Core.BusLocations;
using OBilet.Core.Sessions;
using OBilet.Web.Models;
using UAParser;


namespace OBilet.Web.Controllers
{
    public class OBiletController : Controller
    {
        #region Fields

        private readonly ISessionService _sessionService;
        private readonly IBusLocationService _busLocationService;
        private readonly IBusJourneyService _busJourneyService;

        public const string SessionKeySessionId = "_SessionId";
        public const string SessionKeyDeviceId = "_DeviceId";
        public const string SessionKeyLastSearch = "_OriginId";

        #endregion


        #region ctor

        public OBiletController(ISessionService sessionService, IBusLocationService busLocationService,
            IBusJourneyService busJourneyService)
        {
            _sessionService = sessionService;
            _busLocationService = busLocationService;
            _busJourneyService = busJourneyService;
        }

        #endregion


        #region Views

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await CreateSession();

            var viewModel = await GetIndexViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> JourneyIndex()
        {
            if (HttpContext.Session.GetString(SessionKeySessionId) == null ||
                HttpContext.Session.GetInt32(SessionKeyLastSearch) == null)
                return RedirectToAction("Index");

            var requestData =
                JsonSerializer.Deserialize<IndexViewModel>(HttpContext.Session.GetString(SessionKeyLastSearch)!);

            var viewModelList = await GetJourneyIndexViewModel(requestData!);

            return View(viewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> JourneyIndex(IndexViewModel viewModel)
        {
            if (HttpContext.Session.GetString(SessionKeySessionId) == null) return RedirectToAction("Index");

            if (viewModel.OriginId == viewModel.DestinationId) return Redirect("Index");

            var viewModelList = await GetJourneyIndexViewModel(viewModel);

            return View(viewModelList);
        }

        #endregion


        #region API Operation

        private async Task CreateSession()
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            var clientInfo = uaParser.Parse(userAgent);

            var request = new GetSessionRequest()
            {
                Application = new Application(),
                Connection = new Connection()
                {
                    IdAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Port = Request.HttpContext.Connection.RemotePort
                },
                Browser = new Browser()
                {
                    Name = clientInfo.UA.Family,
                    Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}"
                }
            };

            var response = await _sessionService.GetSession(request);

            if (response?.Data?.SessionId != null && response.Data?.DeviceId != null)
            {
                HttpContext.Session.SetString(SessionKeySessionId, response.Data.SessionId);
                HttpContext.Session.SetString(SessionKeyDeviceId, response.Data.DeviceId);
            }
        }

        private async Task<List<BusJourney>> GetBusJourneyList(int originId,int destinationId, DateTime departureDate)
        {
            var request = new GetBusJourneyRequest()
            {
                Data = new GetBusJourneyRequestData()
                {
                    OriginId = originId,
                    DestinationId = destinationId,
                    DepartureDate = departureDate
                },
                DeviceSession = new Session()
                {
                    SessionId = HttpContext.Session.GetString(SessionKeySessionId),
                    DeviceId = HttpContext.Session.GetString(SessionKeyDeviceId)
                },
                Date = DateTime.Now
            };

            var journeys = await _busJourneyService.GetBusJourneys(request);

            return journeys ?? new List<BusJourney>();
        }

        private async Task<List<BusLocation>> GetBusLocationList(string prefix)
        {
            var request = new GetBusLocationsRequest()
            {
                Data = prefix,
                DeviceSession = new Session()
                {
                    SessionId = HttpContext.Session.GetString(SessionKeySessionId),
                    DeviceId = HttpContext.Session.GetString(SessionKeyDeviceId)
                },
                Date = DateTime.Now
            };

            var busLocations = await _busLocationService.GetBusLocations(request);

            return busLocations;
        }

        #endregion


        #region View Operations

        private async Task<IndexViewModel> GetIndexViewModel()
        {
            IndexViewModel viewModel;

            if (HttpContext.Session.GetInt32(SessionKeyLastSearch) != null)
                viewModel = JsonSerializer.Deserialize<IndexViewModel>(HttpContext.Session.GetString(SessionKeyLastSearch)!)!;

            else
            {
                var request = new GetBusLocationsRequest()
                {
                    Data = null,
                    DeviceSession = new Session()
                    {
                        SessionId = HttpContext.Session.GetString(SessionKeySessionId),
                        DeviceId = HttpContext.Session.GetString(SessionKeyDeviceId)
                    },
                    Date = DateTime.Now
                };

                var busLocations = await _busLocationService.GetBusLocations(request);

                viewModel = new IndexViewModel()
                {
                    OriginId = busLocations[0].Id,
                    OriginName = busLocations[0].Name,
                    DestinationId = busLocations[2].Id,
                    DestinationName = busLocations[2].Name,
                    DepartureDate = DateTime.Now.AddDays(1)
                };

                var lastSearch = JsonSerializer.Serialize(viewModel);
                HttpContext.Session.SetString(SessionKeyLastSearch, lastSearch);
            }

            return viewModel;
        }

        private async Task<List<JourneyIndexViewModel>> GetJourneyIndexViewModel(IndexViewModel viewModel)
        {
            var lastSearch = JsonSerializer.Serialize(viewModel);
            HttpContext.Session.SetString(SessionKeyLastSearch, lastSearch);

            ViewBag.OriginLocation = viewModel.OriginName!;
            ViewBag.DestinationLocation = viewModel.DestinationName!;
            ViewBag.DateInfo = DateInfo(viewModel.DepartureDate);

            var journeys = await GetBusJourneyList(viewModel.OriginId,viewModel.DestinationId,viewModel.DepartureDate);

            var viewModelList = journeys.Select(r =>
                {
                    if (r.Journey == null) return null;
                    return new JourneyIndexViewModel()
                    {
                        Departure = r.Journey.Departure,
                        Arrival = r.Journey.Arrival,
                        Origin = r.Journey.Origin,
                        Destination = r.Journey.Destination,
                        OriginLocationId = r.OriginLocationId,
                        OriginLocation = r.OriginLocation,
                        DestinationLocationId = r.DestinationLocationId,
                        DestinationLocation = r.DestinationLocation,
                        DateInfo = DateInfo(r.Journey.Departure),
                        Id = r.Id
                    };
                })
                .ToList();

            return viewModelList!;
        }

        public async Task<IActionResult> Change()
        {
            var viewModel =
                JsonSerializer.Deserialize<IndexViewModel>(HttpContext.Session.GetString(SessionKeyLastSearch)!);

            var newViewModel = new IndexViewModel()
            {
                OriginId = viewModel!.DestinationId,
                OriginName = viewModel.DestinationName,
                DestinationId = viewModel.OriginId,
                DestinationName = viewModel.OriginName,
                DepartureDate = viewModel.DepartureDate
            };

            HttpContext.Session.SetString(SessionKeyLastSearch, JsonSerializer.Serialize(newViewModel));

            return await Task.FromResult(RedirectToAction("Index"));
        }

        public async Task<JsonResult> AutoComplete(string prefix)
        {
            var busLocations = await GetBusLocationList(prefix);

            var locations = (from c in busLocations
                             where c.Name != null && c.Name.ToLower().StartsWith(prefix.ToLower())
                             select new { name = c.Name, id = c.Id });

            return Json(locations);
        }

        public void RedirectOBiletDetailPage(JourneyIndexViewModel data)
        {
            var url =
                $"https:////www.obilet.com/seferler/{data.OriginLocationId}-{data.DestinationLocationId}" +
                $"/{data.Departure:yyyy-MM-dd}/{data.Id}";

            Response.Redirect(url);
        }

        private static string DateInfo(DateTime dateValue)
        {
            var dayNumber = dateValue.Day;
            var month = dateValue.ToString("MMMM", new CultureInfo("tr-TR"));
            var day = dateValue.ToString("dddd", new CultureInfo("tr-TR"));

            var dayInfo = $"{dayNumber} {month} {day}";

            return dayInfo;
        }

        #endregion


        #region Error Page

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}