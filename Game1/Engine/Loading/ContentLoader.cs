using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine
{
    public class ContentLoader
    {
        public List<TextureFile> Images;
        public int ImageCount { get => Images.Count; }

        public ContentLoader()
        {
            Images = new List<TextureFile>();
            LoadImages();
        }

        // First iteration, naive approach, no optimization
        public void LoadImages()
        {
            DirectoryInfo gameDataDirectory = new DirectoryInfo(Global.AppPath + "/data");
            FileInfo[] files = gameDataDirectory.GetFiles();
            Console.WriteLine("-------- LOADING --------");
            LoadSubDirectory(gameDataDirectory);
            Console.WriteLine("------ END LOADING ------");
        }

        /// <summary>
        /// Recursive function to Load a Directory and all the subdirectories.
        /// </summary>
        /// <param name="searchDirectory"></param>
        public void LoadSubDirectory(DirectoryInfo searchDirectory)
        {
            foreach (var directory in searchDirectory.GetDirectories())
            {
                LoadSubDirectory(directory);
                LoadDirectory(directory);
            }
        }

        /// <summary>
        /// Loads all the images in directory directory and returns the number of file loaded
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public int LoadDirectory(DirectoryInfo directory)
        {
            FileInfo[] files = directory.GetFiles();
            int filesTotal = files.Length;
            int nbFile = 0;
            string directoryStr = directory.FullName.Substring(directory.FullName.LastIndexOf("data\\")) + "\\";

            Console.WriteLine($"searching: {directoryStr}, nb of files: {filesTotal}");
            for (int i = filesTotal - 1; i >= 0; i--)
            {
                if (files[i].Extension == ".png")
                {
                    Images.Add(LoadTexture(directoryStr, files[i].Name, files[i].Extension));
                    nbFile++;
                }
            } 
            Console.WriteLine($"- loaded: {nbFile} file(s)");
            return nbFile;
        }

        /// <summary>
        /// Creates a new TextureFile corresponding to an adress
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="extention"></param>
        /// <returns></returns>
        public static TextureFile LoadTexture(string directory, string fileName, string extention)
        {
            Console.WriteLine($"-> loadingFile: {fileName}, FilePath: {directory}");

            FileStream fileStream = new FileStream(directory + fileName, FileMode.Open);
            if (fileStream.Length == 0)
            {
                Console.WriteLine("Error, file not found");
                return null;
            }

            Texture2D spr = Texture2D.FromStream(Global.GraphicsDevice, fileStream);
            fileStream.Dispose();
            return new TextureFile(fileName, directory, spr);
        }
    }
}
