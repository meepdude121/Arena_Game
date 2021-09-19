using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game.File
{
	public class Manager
	{
		// List<Item.ItemData> inventoryData = null
		public Save CreateSaveFile(string a = " ASDFSDFSD fsDF ")
		{
			// create new save class
			Save save = new Save();
			save.A = a;
			return save;
			Debug.LogError($"Failed to create save file! Output: \n{save}"); // yes i know visual studio
			return null;
		}
		public void SaveGame()
		{
			Debug.Log(Application.persistentDataPath);
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = System.IO.File.Create(Application.persistentDataPath + "/.persistant");
			bf.Serialize(file, CreateSaveFile());
			file.Close();
		}

		void LoadGame()
		{

		}
	}
	[System.Serializable]
	public class Save
	{
		public Version version;
		public List<Item.ItemData> InventoryData = new List<Item.ItemData>();
		public Item.ItemData Cannon;
		public Item.ItemData Shell;
		public Item.ItemData Locomotive;
		public string A;
	}
	public enum Version
	{
		prealpha
	}
}

