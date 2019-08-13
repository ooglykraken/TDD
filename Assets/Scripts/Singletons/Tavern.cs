using UnityEngine;
using System.Collections;

public class Tavern : MonoBehaviour {
	public int level;
	public int levelUpCost;
	public int heroCost;
	
	public bool heroSelected;
	
	private TextMesh textMesh;
	
	public void Awake(){
		heroSelected = false;
		level = 1;
		
		textMesh = transform.Find("TxtTavernLevel").GetComponent<TextMesh>();
	}
	public void LevelUp(){
		if(level != 20)
			level++;
	}
	
	public void Minimize(){
		gameObject.SetActive(!gameObject.activeSelf);
	}
	
	public void Update(){
		levelUpCost = level * 10;
		heroCost = level * 10;
		textMesh.text = "Tavern Level: " + level;
	}
	
	private static Tavern instance;
	public static Tavern Instance(){
		if(instance == null){
			instance = GameObject.Find("Tavern").GetComponent<Tavern>();
		}
		return instance;
	}
}
