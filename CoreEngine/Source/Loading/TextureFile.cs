using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CoreEngine
{
    public class TextureFile
    {
        public string FileName;
        public string FilePath;
        public Texture2D Texture2D;
        
        /// <summary>
        /// Creates a new TextureFile class to handle textures and their physical location on disc
        /// </summary>
        /// <param name="fileName">Name of the file with extention</param>
        /// <param name="filePath">Path of the file from data folder in game</param>
        public TextureFile(string fileName, string filePath, Texture2D image)
        {
            FileName = fileName;
            FilePath = filePath;
            Texture2D = image;
        }

        public void PrintInfos()
        {
            Console.WriteLine($"FileName: {FileName}, FilePath: {FilePath}, image size: {Texture2D.Width}*{Texture2D.Height}px");
        }
    }
}
