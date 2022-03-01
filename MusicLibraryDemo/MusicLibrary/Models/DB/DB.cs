using Microsoft.Data.Sqlite;

public class DB
{
    private readonly SqliteConnectionStringBuilder _connectionStringBuilder;
    
    public DB(string path)
    { 
        _connectionStringBuilder = new SqliteConnectionStringBuilder();
        _connectionStringBuilder.DataSource = path;
    }
    
    public void CreateTable()
    {
        
        using (var connection = new SqliteConnection(_connectionStringBuilder.ConnectionString))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "DROP TABLE IF EXISTS track";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DROP TABLE IF EXISTS song";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DROP TABLE IF EXISTS artist";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DROP TABLE IF EXISTS genre";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "CREATE TABLE artist(artist_id INTEGER PRIMARY KEY, artist TEXT)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "CREATE TABLE genre(genre_id INTEGER PRIMARY KEY, genre TEXT)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "CREATE TABLE song(song_id INTEGER PRIMARY KEY, title TEXT, duration TEXT,issued_date YEAR)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"CREATE TABLE track(track_id  INTEGER PRIMARY KEY, artist_id INT, song_id INT, genre_id  INT, 
                CONSTRAINT artist___fk FOREIGN KEY (artist_id) REFERENCES artist (artist_id),
                CONSTRAINT genre___fk FOREIGN KEY (genre_id) REFERENCES genre (genre_id),
                CONSTRAINT song___fk FOREIGN KEY (song_id) REFERENCES song (song_id));";
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
        }
    }
    public void InsertValue()
    {
        using (var connection = new SqliteConnection(_connectionStringBuilder.ConnectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var cmd = connection.CreateCommand();
                
                cmd.CommandText = "INSERT INTO artist (artist) VALUES ('BTS')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO artist (artist) VALUES ('Ed Sheeran')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO artist (artist) VALUES ('Taylor Swift')";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "INSERT INTO genre(genre) VALUES ('Hiphop')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO genre(genre) VALUES ('Pop')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO genre(genre) VALUES ('K-Pop')";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO genre(genre) VALUES ('Rock')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Shivers', '3:31', '2022')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Shape of you', '3:54', '2022')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Sing', '3:49', '2020')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Mic drop', '3:51', '2021')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Butter', '3:34', '2022')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Dinamite', '4:11', '2022')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Love story', '3:38', '2020')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Blank space', '3:56', '2022')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('Shake it off', '3:44', '2022')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO song (title, duration, issued_date) VALUES ('cardigan', '0:00', '2022')";
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (2, 1, 2)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (2, 2, 4)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (2, 3, 2)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (1, 4, 1)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (1, 5, 3)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (1, 6, 3)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (3, 7, 2)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (3, 8, 2)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (3, 9, 2)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO track (artist_id, song_id, genre_id) VALUES (3, 10, 2)";
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
        }
    }

    public void testReadData()
    {
        using (var connection = new SqliteConnection(_connectionStringBuilder.ConnectionString))
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
                    Console.WriteLine($"{reader.GetString(0)}|{reader.GetString(1)}|{reader.GetString(2)}|{reader.GetString(3)}|{reader.GetString(4)}");
                }
            }
        }
    }
    
    public string getConnectionString()
    {
        return _connectionStringBuilder.ConnectionString;
    }

}