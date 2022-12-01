using System.IO;
using System;
using UnityEngine;

namespace ES3Internal
{
	public static class ES3IO
	{
		public enum ES3FileMode {Read, Write, Append}

		public static DateTime GetTimestamp(string filePath)
		{
			if(!FileExists(filePath))
				return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			return File.GetLastWriteTime(filePath).ToUniversalTime();
		}

		public static string GetExtension(string path)
		{
			return Path.GetExtension(path);
		}

		public static void DeleteFile(string filePath)
		{ 
			if(FileExists(filePath))
				File.Delete(filePath);	
		}

		public static bool FileExists(string filePath) { return File.Exists(filePath); }
		public static void MoveFile(string sourcePath, string destPath) { File.Move(sourcePath, destPath); }
		public static void CopyFile(string sourcePath, string destPath) { File.Copy(sourcePath, destPath); }

		/*
		 * 	Given a path, it returns the directory that path points to.
		 * 	eg. "C:/myFolder/thisFolder/myFile.txt" will return "C:/myFolder/thisFolder".
		 */

		public static void CreateDirectory(string directoryPath){ Directory.CreateDirectory(directoryPath); }
		public static bool DirectoryExists(string directoryPath) { return Directory.Exists(directoryPath); }

		/*
		 * 	Given a path, it returns the directory that path points to.
		 * 	eg. "C:/myFolder/thisFolder/myFile.txt" will return "C:/myFolder/thisFolder".
		 */
		public static string GetDirectoryName(string path){ return Path.GetDirectoryName(path); }

		public static string[] GetDirectories(string path, bool getFullPaths = true)
		{
			var paths = Directory.GetDirectories(path);
			if(!getFullPaths)
			{
				for(int i=0; i<paths.Length; i++)
					paths[i] = Path.GetFileName(paths[i]);
			}
			return paths;
		}

		public static void DeleteDirectory(string directoryPath)
		{
			if(DirectoryExists(directoryPath))
				Directory.Delete( directoryPath, true ); 
		}

		public static string[] GetFiles(string path, bool getFullPaths = true)
		{
			var paths = Directory.GetFiles(path);
			if(!getFullPaths)
			{
				for(int i=0; i<paths.Length; i++)
					paths[i] = Path.GetFileName(paths[i]);
			}
			return paths;
		}

		public static byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);
		}

		public static void WriteAllBytes(string path, byte[] bytes)
		{
			File.WriteAllBytes(path, bytes);
		}

		public static void CommitBackup(ES3Settings settings)
		{
			if(settings.location == ES3.Location.File)
			{
				// Delete the old file before overwriting it.
				DeleteFile(settings.FullPath);
				// Rename temporary file to new file.
				MoveFile(settings.FullPath + ES3.temporaryFileSuffix, settings.FullPath);
			}
			else if(settings.location == ES3.Location.PlayerPrefs)
			{
				PlayerPrefs.SetString(settings.FullPath, PlayerPrefs.GetString(settings.FullPath + ES3.temporaryFileSuffix));
				PlayerPrefs.DeleteKey(settings.FullPath + ES3.temporaryFileSuffix);
				PlayerPrefs.Save();
			}
		}
	}
}