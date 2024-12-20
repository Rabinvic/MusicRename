using Microsoft.VisualBasic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace MusicRename
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String path = args[0];
            if (!Directory.Exists(path))
            {
                return;
            }

            // Get contents of the path, ensure we have access to read the directory
            String[] contents = [];
            try
            {
                contents = Directory.GetFileSystemEntries(path);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.Error.WriteLine(e.Message);
            }

            updateFiles(contents);

        }

        static void updateFiles(string[] dir)
        {
            foreach (var item in dir)
            {
                if (File.Exists(item))
                {
                    if (item.EndsWith("m3u"))
                    {
                        continue;
                    }

                    var file = TagLib.File.Create(item);

                    //StorageFile f = await StorageFile.GetFileFromPathAsync(item);
                    //MusicProperties musicProperties = await f.Properties.GetMusicPropertiesAsync();

                    //Console.Out.WriteLine(musicProperties.Artist);

                    var artist = file.Tag.Performers;
                    var title = file.Tag.Title;
                    var album = file.Tag.Album;
                    var genre = file.Tag.Genres;
                    var composers = file.Tag.Composers;
                    var comment = file.Tag.Comment;


                    //Console.Out.WriteLine((int)artist[0][0]);

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Encoding wind = Encoding.GetEncoding(1251);
                    Encoding original = Encoding.GetEncoding("UTF-16");
                    Console.WriteLine("\n\n"+item);

                    char[] removeChars = ['«', '»', '.', '(', ')', '!', '?'];

                    var tempTitle = title.Trim(removeChars);
                    if ((int)tempTitle[tempTitle.Length - 1] <= 255)
                    {

                        byte[] origBytes = original.GetBytes(title);
                        byte[] windBytes = Encoding.Convert(original, wind, origBytes);
                        String windStr = wind.GetString(origBytes);
                        Console.Out.WriteLine("Title: "+windStr);


                        //string windArtist = wind.GetString(windBytes);
                        //Console.Out.WriteLine(windStr);

                        //byte[] newWind = org.GetBytes(windStr);

                        //foreach (var b in windBytes)
                        //{
                        //    Console.Out.WriteLine(b.ToString());
                        //}

                        // Change file path accordingly.

                        windStr = windStr.Replace("\0", "");

                        //string[] artists = windStr.Split(",");

                        file.Tag.Title = windStr;
                    }
                    var tempArtist = artist[0].Trim(removeChars);
                    if ( (int)tempArtist[tempArtist.Length - 1] <= 255 )
                    {
                        byte[] origBytes = original.GetBytes(artist[0]);
                        byte[] windBytes = Encoding.Convert(original, wind, origBytes);
                        String windStr = wind.GetString(origBytes);
                        Console.Out.WriteLine("Artists: "+windStr);


                        //string windArtist = wind.GetString(windBytes);
                        //Console.Out.WriteLine(windStr);

                        //byte[] newWind = org.GetBytes(windStr);

                        //foreach (var b in windBytes)
                        //{
                        //    Console.Out.WriteLine(b.ToString());
                        //}

                        // Change file path accordingly.

                        windStr = windStr.Replace("\0", "");

                        string[] artists = windStr.Split(",");

                        file.Tag.Performers = artists;

                        //for (int i = 0; i < artists.Length; i++)
                        //{
                        //    var person = artists[i];
                        //    string newPerson = "";
                        //    for (int j = 0; j < person.Length; j++)
                        //    {
                        //        if (person[j].GetHashCode() == 0)
                        //        {
                        //            continue;
                        //        }
                        //        newPerson += person[j];
                        //        Console.Out.WriteLine(person[j]);
                        //    }
                        //    artists[i] = newPerson;
                        //}


                        //Console.Out.WriteLine(artists.Length);
                        //Console.Out.WriteLine((int)artists[0][0]);

                        //
                    }

                    var tempAlbum = album.Trim(removeChars);
                    if ((int)tempAlbum[tempAlbum.Length - 1] <= 255)
                    {
                        byte[] origBytes = original.GetBytes(album);
                        byte[] windBytes = Encoding.Convert(original, wind, origBytes);
                        String windStr = wind.GetString(origBytes);
                        Console.Out.WriteLine("Album: "+windStr);

                        windStr = windStr.Replace("\0", "");
                        file.Tag.Album = windStr;
                    }

                    var tempGenre = genre[0].Trim(removeChars);
                    if (((int)tempGenre[tempGenre.Length - 1] <= 255))
                    {
                        byte[] origBytes = original.GetBytes(genre[0]);
                        byte[] windBytes = Encoding.Convert(original, wind, origBytes);
                        String windStr = wind.GetString(origBytes);
                        Console.Out.WriteLine("Genres: "+windStr);

                        windStr = windStr.Replace("\0", "");

                        string[] genres = windStr.Split(",");

                        file.Tag.Genres = genres;
                    }

                    var tempComposer = composers[0].Trim(removeChars);
                    if (((int)tempComposer[tempComposer.Length - 1] <= 255))
                    {
                        byte[] origBytes = original.GetBytes(composers[0]);
                        byte[] windBytes = Encoding.Convert(original, wind, origBytes);
                        String windStr = wind.GetString(origBytes);
                        Console.Out.WriteLine("Composers: "+windStr);

                        windStr = windStr.Replace("\0", "");

                        string[] composer = windStr.Split(",");

                        file.Tag.Composers = composer;
                    }

                    var tempComment = comment.Trim(removeChars);
                    if (((int)tempComment[tempComment.Length - 1] <= 255))
                    {
                        byte[] origBytes = original.GetBytes(comment);
                        byte[] windBytes = Encoding.Convert(original, wind, origBytes);
                        String windStr = wind.GetString(origBytes);
                        Console.Out.WriteLine("Comment: "+windStr);

                        windStr = windStr.Replace("\0", "");
                        file.Tag.Comment = windStr;
                    }



                    //// Save Changes:
                    file.Save();
                    file.Dispose();
                }
                else
                {
                    updateFiles(Directory.GetFileSystemEntries(item));
                }
            }
        }
    }
}
