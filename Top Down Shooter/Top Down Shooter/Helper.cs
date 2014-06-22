using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Windows.Storage;

namespace Top_Down_Shooter
{
    // This class provides a set of helper methods
    public static class Helper
    {
        // Centers a gun in relation to the player's position or a projectile in relation to the player's gun's position (and more!)
        // In the former example above, the player is the parent object, and the gun is the child object
        // In the latter example above, the gun is the parent object, and the projectile is the child object
        // This method returns the child object's position
        public static Vector2 CenterGraphic(LevelObject.Direction dir, LevelObject Parent, LevelObject Child)
        {
            // Store the X and Y position of the child graphic, respectively
            int X = 0, Y = 0;

            switch (dir)
            {
                case LevelObject.Direction.Left:
                    X = (int)(Parent.ObjectPos.X - Child.ObjectTexture.Width);
                    Y = (int)(Parent.ObjectPos.Y + (Parent.ObjectTexture.Height / 2) - (Child.ObjectTexture.Height / 2));

                    break;
                case LevelObject.Direction.Right:
                    X = (int)(Parent.ObjectPos.X + Parent.ObjectTexture.Width);
                    Y = (int)(Parent.ObjectPos.Y + (Parent.ObjectTexture.Height / 2) - (Child.ObjectTexture.Height / 2));

                    break;
                case LevelObject.Direction.Up:
                    X = (int)(Parent.ObjectPos.X + (Parent.ObjectTexture.Width / 2) - (Child.ObjectTexture.Width / 2));
                    Y = (int)(Parent.ObjectPos.Y - Child.null.Height);

                    break;
                case LevelObject.Direction.Down:
                    X = (int)(Parent.ObjectPos.X + (Parent.ObjectTexture.Width / 2) - (Child.ObjectTexture.Width / 2));
                    Y = (int)(Parent.ObjectPos.Y + Parent.ObjectTexture.Height);

                    break;
            }

            // Return a new Vector2 with the coordinates for the child object
            return (new Vector2(X, Y));
        }

        // Retrieves a subfolder from the app's designated storage location
        public static async Task<StorageFolder> GetFolder(String FolderName)
        {
            // Get the app's local storage folder
            StorageFolder folder = ApplicationData.Current.LocalFolder;

            // Try to get the subfolder in the storage folder. If the folder is not found, the StorageFolder will be null
            StorageFolder subfolder = (StorageFolder)await folder.TryGetItemAsync(FolderName);

            // Return the subfolder
            return subfolder;
        }

        // Creates a new folder in the app's designated storage location
        public static async void CreateFolder(String FolderName)
        {
            // Get the app's local storage folder
            StorageFolder folder = ApplicationData.Current.LocalFolder;

            // Try to create the folder
            await folder.CreateFolderAsync(FolderName, CreationCollisionOption.OpenIfExists)
        }

        // Retrieves a file from the app's designated storage location
        public static async Task<StorageFile> GetFile(String FileName)
        {
            // Get the app's local storage folder
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            
            // Try to get the file from the folder. If the file is not found, the StorageFile will be null
            StorageFile file = (StorageFile) await folder.TryGetItemAsync(FileName);

            // Return the file
            return file;
        }

        // Creates a new file in the app's designated storage location
        public static async void CreateFile(String FileName)
        {
            // Get the app's local storage folder
            StorageFolder folder = ApplicationData.Current.LocalFolder;

            // Try to create the file
            await folder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
        }


    }
}