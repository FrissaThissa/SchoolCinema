using cinema.Models;
using cinema.Repositories;

namespace cinema.Services;

public class ShowService : IShowService
{
    private readonly IShowRepository _showRepository;
    private readonly IRoomService _roomService;
    private readonly ITicketRepository _ticketRepository;

    public ShowService(IShowRepository showRepository, IRoomService roomService, ITicketRepository ticketRepository)
    {
        _showRepository = showRepository;
        _roomService = roomService;
        _ticketRepository = ticketRepository;
    }

    public Dictionary<Movie, List<Show>> GetShowsPerMovie()
    {
        Dictionary<Movie, List<Show>> result = new Dictionary<Movie, List<Show>>();
        List<Show> shows = _showRepository.FindAllIncludeMovie().ToList();
        foreach(Show show in shows)
        {
            if(result.ContainsKey(show.Movie))
                result[show.Movie].Add(show);
            else
                result.Add(show.Movie, new List<Show> { show });
        }
        return result;
    }

    public int GetAvailableSeatAmount(Show show)
    {
        int totalseats = _roomService.GetTotalSeatAmount(_roomService.GetShowRoom(show));
        int takenseats = _ticketRepository.FindTicketsByShow(show).Count();
        return totalseats - takenseats;
    }

    public Dictionary<DateOnly, Dictionary<Movie, List<Show>>> GetShowsPerMoviePerDay(List<Show> showList)
    {
        showList.Sort((a,b) => DateTime.Compare(a.StartTime,b.StartTime));
        var dateList = new List<DateOnly>();
        var showDict = new Dictionary<DateOnly, List<Show>>();
        var showPerMoviePerDateDict = new Dictionary<DateOnly, Dictionary<Movie, List<Show>>>();
        foreach (Show show in showList)
        {
            var date = DateOnly.FromDateTime(show.StartTime);
            if(!(dateList.Contains(date)))
            {
                dateList.Add(date);
            }
        }
        dateList.Sort((a, b) => (a.CompareTo(b)));
        foreach (var date in dateList)
        {
            showDict.Add(date,new List<Show>());
        }

        foreach (Show show in showList)
        {
            var date = DateOnly.FromDateTime(show.StartTime);
            showDict[date].Add(show);
        }


        foreach (var date in showDict.Keys)
        {
            var showsPerMovieDict = new Dictionary<Movie, List<Show>>();
            var showsPerDate = showDict[date];
            foreach (var show in showsPerDate)
            {
                if (!showsPerMovieDict.ContainsKey(show.Movie))
                {
                    showsPerMovieDict.Add(show.Movie,new List<Show>());
                }
                showsPerMovieDict[show.Movie].Add(show);
            }
            showPerMoviePerDateDict[date] = showsPerMovieDict;
        }

        return showPerMoviePerDateDict;
    }

    public Show getShowById(int id)
    {
        return _showRepository.FindShowByIdIncludeMovie(id);
    }
    
}