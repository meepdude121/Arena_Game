using System;
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
		public Save CreateSaveFile(string a = "")
		{
			// create new save class
			Save save = new Save();
			save.A = a;
			save.version = Application.version;
			Debug.Log(save.version);
			
			return save;
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
		public string version;
		public List<Item.ItemData> InventoryData = new List<Item.ItemData>();
		public Item.ItemData Cannon;
		public Item.ItemData Shell;
		public Item.ItemData ModeOfTransport;
		public string A;
	}
}

