using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;

public class HomeController : Controller
{
    // GET
    public IActionResult Index(string genre = "")
    {
        var musicLibrary = new MusicLibrary.Models.MusicLibrary();
        var alltracks = musicLibrary.GetAllDefaultTracks();
        var fillteredtracks = musicLibrary.GetMusicTracks(genre);
        var viewModel = new ViewModel{AllTracks = alltracks, FillteredTracks = fillteredtracks};
        foreach (var track in fillteredtracks)
        {
            Console.WriteLine($"{track.Artist}|{track.Title}|{track.Duration}|{track.IssuedDate}|{track.Genre}");
        }
        return View(viewModel);
    }
}