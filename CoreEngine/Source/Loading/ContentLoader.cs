using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoreEngine
{
    public class ContentLoader
    {
        private DirectoryInfo contentDirectory;
        private List<TextureFile> images;
        public int ImageCount { get => images.Count; }

        public ContentLoader()
        {
            images = new List<TextureFile>();
            contentDirectory = new DirectoryInfo(Application.ContentDirectory);

            //LoadImages();
        }

        // First iteration, naive approach, no optimization
        public void LoadImages()
        {
            Console.WriteLine("-------- LOADING --------");

            if(!contentDirectory.Exists)
                return ;
                
            FileInfo[] files = contentDirectory.GetFiles();
            LoadTreeDirectory(contentDirectory);
            Console.WriteLine("------ END LOADING ------");
        }

        /// <summary>
        /// Recursive function to Load a Directory and all the subdirectories.
        /// </summary>
        /// <param name="searchDirectory"></param>
        public void LoadTreeDirectory(DirectoryInfo searchDirectory)
        {
            foreach (var directory in searchDirectory.GetDirectories())
            {
                LoadTreeDirectory(directory);
            }
            LoadDirectory(searchDirectory);
        }

        /// <summary>
        /// Loads all the images in directory directory and returns the number of file loaded
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public int LoadDirectory(DirectoryInfo directory)
        {
            var files = directory.GetFiles();
            int filesTotal = files.Length;
            int nbFile = 0;
            Console.WriteLine(directory.FullName);
            string directoryStr = directory.FullName;

            Console.WriteLine($"searching: {directoryStr}, nb of files: {filesTotal}");
            for (int i = filesTotal - 1; i >= 0; i--)
            {
                if (files[i].Extension == ".png" || files[i].Extension == ".jpg")
                {
                    images.Add(LoadTexture(directoryStr, files[i].Name, files[i].Extension, files[i].FullName));
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
        public static TextureFile LoadTexture(string directory, string fileName, string extention, string fullpath)
        {
            Console.WriteLine($"-> loadingFile: {fileName}, FilePath: {directory}");

            FileStream fileStream = new FileStream(fullpath, FileMode.Open);
            if (fileStream.Length == 0)
            {
                Console.WriteLine("Error, file not found");
                return null;
            }

            Texture2D spr = Texture2D.FromStream(Application.Graphics.GraphicsDevice, fileStream);
            fileStream.Dispose();
            return new TextureFile(fileName, directory, spr);
        }
    }

    
}
