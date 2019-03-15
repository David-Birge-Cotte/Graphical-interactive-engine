using System;
using System.IO;

namespace CoreEngine
{
    public static class FileLoader
    {
        public static string LoadScene(string sceneName)
        {
            return LoadFile("scene.lua", sceneName);
        }

        public static string LoadFile(string fileName, string sceneName)
        {
            DirectoryInfo contentDirectory = new DirectoryInfo(Application.ContentDirectory);
            DirectoryInfo scenesDirectory =  
                new DirectoryInfo(Path.Combine(contentDirectory.FullName, "scenes"));
            if (!scenesDirectory.Exists)
                return ($"print('error, no scenes directory')");
            
            DirectoryInfo sceneDirectory = 
                new DirectoryInfo(Path.Combine(scenesDirectory.FullName, sceneName));
            if (!sceneDirectory.Exists)
                return ($"print('error, no scene { sceneName } directory')");
            
            
            var files = sceneDirectory.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Name == fileName)
                {
                    var stream = file.OpenText();
                    return stream.ReadToEnd();
                }
            }
            return "print('error')";
        }
    }
}