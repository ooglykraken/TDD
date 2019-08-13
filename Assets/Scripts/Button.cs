using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public string Argument;
	
	private Vector3 cursor;
	private Gameplay gameplay;
	private Tavern tavern;
	private MousePosition mousePosition;
	private InfoPopup infoPopup;
	
	public bool selected;
	
	public void Awake(){
		gameplay = Gameplay.Instance();
		tavern = Tavern.Instance();
		mousePosition = MousePosition.Instance();
		infoPopup = InfoPopup.Instance();
		
		selected = false;
	}
	
	public void Update(){
		cursor = mousePosition.position;
		
		if(InBounds()){
		
			OnHover();
			
			if(Input.GetMouseButtonUp(0)){
				OnClick();
			}
		} else {
			LeaveBounds();
		}
	}
	
	private bool InBounds(){
		return cursor.x <= gameObject.GetComponent<Collider>().bounds.max.x && 
		   cursor.x >= gameObject.GetComponent<Collider>().bounds.min.x && 
		   cursor.y - 2000f <= gameObject.GetComponent<Collider>().bounds.max.y && 
		   cursor.y - 2000f >= gameObject.GetComponent<Collider>().bounds.min.y;
	}
	
	private void OnClick(){
		switch(Argument){
			case "Tavern": 
				if(tavern.level < 20){
					if(gameplay.playerGold - tavern.levelUpCost >= 0){
						tavern.LevelUp();
						gameplay.playerGold -= tavern.levelUpCost;
					} else{
						Debug.Log("Not enough money to upgrade tavern");
					}
				}
				break;
			case "Minimize" :
				tavern.Minimize();
				break;
			case "Ready" :
				gameplay.StartWave();
				break;
			default:
				if(gameplay.numberOfHeroes < gameplay.maxHeroes && gameplay.playerGold - tavern.heroCost >= 0 && !tavern.heroSelected){
					gameplay.playerGold -= tavern.heroCost;
					Hero h = Instantiate(Resources.Load("Heroes/" + Argument, typeof(Hero)) as Hero) as Hero;
					h.Birth(Argument);
					tavern.heroSelected = true;
				} else if(gameplay.numberOfHeroes >= gameplay.maxHeroes){
					Debug.Log(gameplay.numberOfHeroes + "/" + gameplay.maxHeroes + " maximum Heroes");
				}else if(gameplay.playerGold - tavern.heroCost < 0){
					Debug.Log(gameplay.playerGold + " - " + tavern.heroCost + " <= 0");
				}else if(selected){
					Debug.Log("Currently placing");
				}
				break;
		}
	}
	
	// private void OnDrag(){
		// if(Argument == "Minimize"){
			// Transform t = GameObject.Find("BtnMinimizeTavern").transform;
			
			// t.position = new Vector3(mousePosition.position.x, mousePosition.position.y - 2000f, t.position.z);
		// }
	// }
	
	private void LeaveBounds(){
		// if(!InfoPopupHeroes.Instance().heroFound){
			// infoPopup.displaying = false;
		// }
	}
	
	private void OnHover(){
		switch(Argument){
			case "Cleric":
				infoPopup.LoadHeroInformation(2);
				break;
			case "Druid":
				infoPopup.LoadHeroInformation(3);
				break;
			case "Monk":
				infoPopup.LoadHeroInformation(4);
				break;
			case "Ranger":
				infoPopup.LoadHeroInformation(5);
				break;
			case "Rogue":
				infoPopup.LoadHeroInformation(0);
				break;
			case "Warrior":
				infoPopup.LoadHeroInformation(1);
				break;
			case "Wizard":
				infoPopup.LoadHeroInformation(6);
				break;
				
			// case "Ready":
				// break;
			// case "Minimize":
				// break;
			case "Tavern":
				infoPopup.LoadTavernInformation();
				break;
				
			default:
				Debug.Log("No proper target found");
				break;
		}
	}
}
