using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using vaultgamesesh;
using System.IO;
using server;
using api;

namespace api
{
    class CustomRooms
    {
        private static readonly string savePath = "SaveData\\Rooms\\Downloaded";

        private static void EnsureSaveDirectory()
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }

        private static void WriteSafe(string fileName, string content)
        {
            try
            {
                File.WriteAllText(Path.Combine(savePath, fileName), content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to {fileName}: {ex.Message}");
            }
        }

        public static void RoomDecode(string text)
        {
            EnsureSaveDirectory();
            ModernRooms.Root root2 = JsonConvert.DeserializeObject<ModernRooms.Root>(text);

            WriteSafe("roomname.txt", root2.Name);
            WriteSafe("roomid.txt", root2.RoomId.ToString());
            WriteSafe("datablob.txt", root2.SubRooms[0].DataBlob);
            WriteSafe("roomsceneid.txt", root2.SubRooms[0].UnitySceneId);
            WriteSafe("imagename.txt", root2.ImageName);
            WriteSafe("cheercount.txt", root2.Stats.CheerCount.ToString());
            WriteSafe("favcount.txt", root2.Stats.FavoriteCount.ToString());
            WriteSafe("visitcount.txt", root2.Stats.VisitCount.ToString());

            room = new Room
            {
                RoomId = 29,
                Name = root2.Name,
                Description = "OpenRec Downloaded Room",
                ImageName = root2.ImageName,
                CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                State = 0,
                Accessibility = 1,
                SupportsLevelVoting = false,
                IsAGRoom = false,
                CloningAllowed = false,
                SupportsScreens = true,
                SupportsWalkVR = true,
                SupportsTeleportVR = true,
                ReplicationId = null,
                ReleaseStatus = 0
            };

            scene = new List<Scene>
            {
                new Scene()
                {
                    RoomSceneId = 1,
                    RoomId = 29,
                    RoomSceneLocationId = root2.SubRooms[0].UnitySceneId,
                    Name = "Home",
                    IsSandbox = true,
                    DataBlobName = root2.SubRooms[0].DataBlob,
                    MaxPlayers = 20,
                    CanMatchmakeInto = true,
                    DataModifiedAt = root2.SubRooms[0].DataSavedAt,
                    ReplicationId = null,
                    UseLevelBasedMatchmaking = false,
                    UseAgeBasedMatchmaking = false,
                    UseRecRoyaleMatchmaking = false,
                    ReleaseStatus = 0,
                    SupportsJoinInProgress = true
                }
            };

            root = new Root
            {
                Room = room,
                Scenes = scene,
                CoOwners = new List<ulong>(),
                InvitedCoOwners = new List<ulong>(),
                Hosts = new List<ulong>(),
                InvitedHosts = new List<ulong>(),
                CheerCount = root2.Stats.CheerCount,
                FavoriteCount = root2.Stats.FavoriteCount,
                VisitCount = root2.Stats.VisitCount,
                Tags = new List<aTag>
                {
                    new aTag() { Tag = "rro", Type = 2 }
                }
            };

            WriteSafe("RoomDetails.json", JsonConvert.SerializeObject(root));
        }

        public static void RoomGet(string roomnames)
        {
            EnsureSaveDirectory();
            string webdata = new WebClient().DownloadString($"https://rooms.rec.net/rooms?name={roomnames}&include=297");
            ModernRooms.Root root2 = JsonConvert.DeserializeObject<ModernRooms.Root>(webdata);

            room = new Room
            {
                RoomId = 29,
                Name = root2.Name,
                Description = "OpenRec Downloaded Room",
                ImageName = root2.ImageName,
                CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                State = 0,
                Accessibility = 1,
                SupportsLevelVoting = false,
                IsAGRoom = false,
                CloningAllowed = false,
                SupportsScreens = true,
                SupportsWalkVR = true,
                SupportsTeleportVR = true,
                ReplicationId = null,
                ReleaseStatus = 0
            };

            scene = new List<Scene>
            {
                new Scene()
                {
                    RoomSceneId = 1,
                    RoomId = 29,
                    RoomSceneLocationId = root2.SubRooms[0].UnitySceneId,
                    Name = "Home",
                    IsSandbox = true,
                    DataBlobName = root2.SubRooms[0].DataBlob,
                    MaxPlayers = 20,
                    CanMatchmakeInto = true,
                    DataModifiedAt = root2.SubRooms[0].DataSavedAt,
                    ReplicationId = null,
                    UseLevelBasedMatchmaking = false,
                    UseAgeBasedMatchmaking = false,
                    UseRecRoyaleMatchmaking = false,
                    ReleaseStatus = 0,
                    SupportsJoinInProgress = true
                }
            };

            root = new Root
            {
                Room = room,
                Scenes = scene,
                CoOwners = new List<ulong>(),
                InvitedCoOwners = new List<ulong>(),
                Hosts = new List<ulong>(),
                InvitedHosts = new List<ulong>(),
                CheerCount = root2.Stats.CheerCount,
                FavoriteCount = root2.Stats.FavoriteCount,
                VisitCount = root2.Stats.VisitCount,
                Tags = new List<aTag>
                {
                    new aTag() { Tag = "rro", Type = 2 }
                }
            };

            WriteSafe("roomname.txt", root2.Name);
            WriteSafe("roomid.txt", root2.RoomId.ToString());
            WriteSafe("datablob.txt", root2.SubRooms[0].DataBlob);
            WriteSafe("roomsceneid.txt", root2.SubRooms[0].UnitySceneId);
            WriteSafe("imagename.txt", root2.ImageName);
            WriteSafe("cheercount.txt", root2.Stats.CheerCount.ToString());
            WriteSafe("favcount.txt", root2.Stats.FavoriteCount.ToString());
            WriteSafe("visitcount.txt", root2.Stats.VisitCount.ToString());
            WriteSafe("RoomDetails.json", JsonConvert.SerializeObject(root));
        }

        public static Room room { get; set; }
        public static List<Scene> scene { get; set; }
        public static Root root { get; set; }

        public class Room
        {
            public ulong RoomId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public ulong CreatorPlayerId { get; set; }
            public string ImageName { get; set; }
            public int State { get; set; }
            public int Accessibility { get; set; }
            public bool SupportsLevelVoting { get; set; }
            public bool IsAGRoom { get; set; }
            public bool CloningAllowed { get; set; }
            public bool SupportsScreens { get; set; }
            public bool SupportsWalkVR { get; set; }
            public bool SupportsTeleportVR { get; set; }
            public object ReplicationId { get; set; }
            public int ReleaseStatus { get; set; }
        }

        public class Scene
        {
            public int RoomSceneId { get; set; }
            public ulong RoomId { get; set; }
            public string RoomSceneLocationId { get; set; }
            public string Name { get; set; }
            public bool IsSandbox { get; set; }
            public string DataBlobName { get; set; }
            public int MaxPlayers { get; set; }
            public bool CanMatchmakeInto { get; set; }
            public DateTime DataModifiedAt { get; set; }
            public object ReplicationId { get; set; }
            public bool SupportsJoinInProgress { get; set; }
        }

        public class Root
        {
            public Room Room { get; set; }
            public List<Scene> Scenes { get; set; }
            public int CheerCount { get; set; }
            public int FavoriteCount { get; set; }
            public int VisitCount { get; set; }
        }

        public class aTag
        {
            public string Tag { get; set; }
            public int Type { get; set; }
        }
    }
}
