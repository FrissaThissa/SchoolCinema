using cinema.Models;

namespace cinema.Services;

public interface IShowService
{
    public Dictionary<DateOnly, Dictionary<Movie, List<Show>>> GetShowsPerMoviePerDay(List<Show> showList);

    public Dictionary<Movie, List<Show>> GetShowsPerMovie();

    public Show getShowById(int id);

    public int GetAvailableSeatAmount(Show show);
}