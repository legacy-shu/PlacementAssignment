using System.Linq;
using Xunit;

public class UnitTest1
{
    [Fact]
    public void GetMusicTracksReturnsAtleastOnetrackOrMore()
    {
        var db = new DB("./musiclibrary.db");
        db.CreateTable();
        db.InsertValue();

        var musicLibrary = new MusicLibrary.Models.MusicLibrary();
        var tracks = musicLibrary.GetMusicTracks();
        Assert.True(tracks.Count() > 0);
    }
    
    [Fact]
    public void GetMusicTracksReturnsTracksWithMatchingGenre()
    {
        var db = new DB("./musiclibrary.db");
        db.CreateTable();
        db.InsertValue();

        var musicLibrary = new MusicLibrary.Models.MusicLibrary();
        var tracks = musicLibrary.GetMusicTracks("Pop");
        Assert.True(tracks.Count() == 3);
    }
    
    [Fact]
    public void GetMusicTracksReturnsNoTracksWithZeroDuration()
    {
        var db = new DB("./musiclibrary.db");
        db.CreateTable();
        db.InsertValue();

        var musicLibrary = new MusicLibrary.Models.MusicLibrary();
        var tracks = musicLibrary.GetMusicTracks();
        var isIncludeZeroDuration = tracks.Any(r => r.Duration == "0:00");
        Assert.True(isIncludeZeroDuration == false);
    }
}