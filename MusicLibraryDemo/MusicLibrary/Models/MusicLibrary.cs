using Microsoft.Data.Sqlite;

namespace MusicLibrary.Models;

public class MusicLibrary
{
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
    
    public IEnumerable<Track> GetAllDefaultTracks()
    {
        var result = new List<Track>();
        using (var connection = new SqliteConnection(new DB("./musiclibrary.db").getConnectionString()))
        {
            connection.Open(); 
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"select a.artist, s.title, s.duration, s.issued_date, g.genre from track
                                left join artist a on track.artist_id = a.artist_id
                                left join song s on track.song_id = s.song_id
                                left join genre g on track.genre_id = g.genre_id";
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

        return result.ToList();
    }
}