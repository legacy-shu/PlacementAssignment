## Table of contents
* [General info](#general-info)
* [Database Design](#Database-Design)
* [Code snippets](#Code-snippets)

## General info
### SQL
* The database consists of a track, genre, artist, song tables.
* The fields returned are include artist, title, duration, issued_date and genre.
* The tracks returned are from the 2021 year.
* The tracks returned are ordered by the artist and the title ascending.
### Music Library
* GetMusicTracks method validates for zero duration tracks and it is filtered by a given genre.
### Music Library Tests
* When the GetMusicTracks method returns one or more tracks, then it is true. 
* When a given genre is 'Pop' and the method returned 3 tracks, then it is true.
* When the tracks include zero duration then it is false. 
### Web Page
* The html file is located in the "MusicLibraryDemo/MusicLibrary/Views/Home/index.cshtml"
* Basically, it shows all tracks from the database and filtered data by the GetMusicTracks method.
* Furthermore, it shows filtered by a given genre when your click links.
## Usage Demo Project
* Open terminal then move to MusicLibrary 
* Enter dotnet run (NOTE: it tested in .net6)
```
~/Download/PlacementAssignment/MusicLibraryDemo/MusicLibrary
$ dotnet run
```
## Database Design
![Screenshot](./db_uml.png)

## Code snippets
SQL
```sqlite
select a.artist, s.title, s.duration, s.issued_date, g.genre from track
left join artist a on track.artist_id = a.artist_id
left join song s on track.song_id = s.song_id
left join genre g on track.genre_id = g.genre_id
where issued_date > 2021
order by artist, title
```
C# MusicLibrary
```c#
public IEnumerable<Track> GetMusicTracks(string genre="")
{
    var result = new List<Track>();
    using (var connection = new SqliteConnection(new DB("./musiclibrary.db").getConnectionString()))
    {
        connection.Open(); 
        var cmd = connection.CreateCommand();
        cmd.CommandText = @"select a.artist, s.title, s.duration, s.issued_date, g.genre from track
                            left join artist a on track.artist_id = a.artist_id
                            left join song s on track.song_id = s.song_id
                            left join genre g on track.genre_id = g.genre_id
                            where issued_date > 2021
                            order by artist, title";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var track = new Track
                {
                    Artist = reader.GetString(0),
                    Title = reader.GetString(1),
                    Duration = reader.GetString(2),
                    IssuedDate = reader.GetString(3),
                    Genre = reader.GetString(4)
                };
                result.Add(track);
            }
        }
    }

    if (genre != null && genre.Length > 0)
    {
        return result.Where(g => g.Genre == genre).Where(t => t.Duration != "0:00").ToList();
    }
        
    return result.Where(t => t.Duration != "0:00").ToList();
}
    
```
C# MusicLibrary Tests
```c#
public void GetMusicTracksReturnsAtleastOnetrackOrMore()
{
    var db = new DB("./musiclibrary.db");
    db.CreateTable();
    db.InsertValue();

    var musicLibrary = new MusicLibrary.Models.MusicLibrary();
    var tracks = musicLibrary.GetMusicTracks();
    Assert.True(tracks.Count() > 0);
}
```
```c#
public void GetMusicTracksReturnsTracksWithMatchingGenre()
{
    var db = new DB("./musiclibrary.db");
    db.CreateTable();
    db.InsertValue();

    var musicLibrary = new MusicLibrary.Models.MusicLibrary();
    var tracks = musicLibrary.GetMusicTracks("Pop");
    Assert.True(tracks.Count() == 3);
}
```
```c#
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
```

