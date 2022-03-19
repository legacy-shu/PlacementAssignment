using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;

public class HomeController : Controller
{
    public IActionResult Index(string genre)
    {
        var musicLibrary = new MusicLibrary.Models.MusicLibrary();
        var alltracks = musicLibrary.GetAllDefaultTracks();
        var fillteredtracks = musicLibrary.GetMusicTracks(genre);
        var viewModel = new ViewModel{AllTracks = alltracks, FillteredTracks = fillteredtracks};
        return View(viewModel);
    }
}