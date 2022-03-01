namespace MusicLibrary.Models;

public class ViewModel
{
    public IEnumerable<Track> AllTracks { get; set; }
    public IEnumerable<Track> FillteredTracks { get; set; }
}