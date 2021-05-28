using UnityEngine;
using UnityEditor;
public class Menu_Main : MonoBehaviour
{
	/// <summary>
	/// Quits the game/editor session immediately.
	/// </summary>
	public static void Quit()
	{ 
		Application.Quit(0);
		EditorApplication.ExecuteMenuItem("Edit/Play");
	}
}