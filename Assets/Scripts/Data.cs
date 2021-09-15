using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game.File
{
	public class Manager
	{
		public Save CreateSaveFile(List<Item> inventoryData = null)
		{
			// create new save class
			Save save = new Save();

			

			Debug.LogError($"Failed to create save file! Output: \n{save}");
			return null;
		}
		void SaveGame()
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = System.IO.File.Create(Application.persistentDataPath + "/gamesave.save");
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
		public List<Item> InventoryData = new List<Item>();
		public Item Cannon;
		public Item Shell;
		public Item Locomotive;
	}
	public enum Version
	{
		prealpha
	}
}

