using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using Photography.DatabaseFirst;

namespace Photography.ExportJson
{
    class ExportJsonAndXml
    {
        static void Main(string[] args)
        {
            var context = new PhotographySystemEntities();
            ExportAlbumsAsJson(context);
            ExportAlbumsAsXml(context);
        }

        private static void ExportAlbumsAsXml(PhotographySystemEntities context)
        {
            var users = context.Users
                .Where(u => u.Albums.Count >= 1)
                .OrderBy(u => u.FullName)
                .Select(u => new
                {
                    id = u.Id,
                    birthdate = u.BirthDate,
                    albums = u.Albums
                        .Select(a => new
                        {
                            albumName = a.Name,
                            Description = a.Description,
                            photos = a.Photographs.Select(p => p.Title)
                        }),
                    camera = new
                    {
                        u.Equipment.Camera.Model,
                        u.Equipment.Camera.Megapixels,
                        Lens = u.Equipment.Lens.Model
                    },
                });

            var xmlDocument = new XElement("users");
            foreach (var user in users)
            {
                var userNode = new XElement("user");
                userNode.Add(new XAttribute("id", user.id));
                userNode.Add(new XAttribute("birth-date", user.birthdate));

                var albumNodes = new XElement("albums");
                foreach (var album in user.albums)
                {
                    var albumNode = new XElement("album");
                    albumNode.Add(new XAttribute("name", album.albumName));
                    if (album.Description != null)
                    {
                        albumNode.Add(new XAttribute("description", album.Description));
                    }

                    var photoNodes = new XElement("photographs");
                    foreach (var photo in album.photos)
                    {
                        var photoNode = new XElement("photo");
                        photoNode.Add(new XAttribute("title", photo));
                        photoNodes.Add(photoNode);
                    }
                    albumNode.Add(photoNodes);
                    albumNodes.Add(albumNode);
                }
                userNode.Add(albumNodes);

                var cameraEl = new XElement("camera");
                cameraEl.Add(new XAttribute("name", user.camera.Model));
                cameraEl.Add(new XAttribute("megapixels", user.camera.Megapixels));
                cameraEl.Add(new XAttribute("lens", user.camera.Lens));

                userNode.Add(cameraEl);
                xmlDocument.Add(userNode);
            }

            xmlDocument.Save("../../users.xml");
        }

        private static void ExportAlbumsAsJson(PhotographySystemEntities context)
        {
            var albums = context.Albums
                .Where(a => a.Photographs.Count >= 1)
                .OrderBy(a => a.Photographs.Count)
                .ThenBy(a => a.Id)
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    owner = a.User.FullName,
                    photoCount = a.Photographs.Count
                });

            var albumsAsJson = JsonConvert.SerializeObject(albums, Formatting.Indented);
            File.WriteAllText("albums.json", albumsAsJson);
        }
    }
}
