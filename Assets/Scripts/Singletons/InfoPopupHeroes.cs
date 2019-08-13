using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class InfoPopupHeroes : MonoBehaviour {
	
	public bool heroFound;
	
	private List<MeshRenderer> childrenRenderers = new List<MeshRenderer>();
	
	private List<Hero> heroes;
	
	private TextMesh txtName;
	private TextMesh txtLevel;
	private TextMesh txtHealth;
	private TextMesh txtDamage;
	private TextMesh txtSpeed;
	private TextMesh txtRange;
	private TextMesh txtAbilities;
	
	private XMLNode xml;
	private XMLNodeList heroesXML;
	
	private MousePosition mousePosition;

	public void Awake(){
		foreach(MeshRenderer r in GetComponentsInChildren<MeshRenderer>()){
			childrenRenderers.Add(r);
		}
		
		heroes = Gameplay.Instance().heroes;
	}
	
	public void Start(){
		txtRange = transform.Find("TxtRange").GetComponent<TextMesh>();
		txtDamage = transform.Find("TxtDamage").GetComponent<TextMesh>();
		txtSpeed = transform.Find("TxtSpeed").GetComponent<TextMesh>();
		txtHealth = transform.Find("TxtHealth").GetComponent<TextMesh>();
		txtLevel = transform.Find("TxtLevel").GetComponent<TextMesh>();
		txtName = transform.Find("TxtName").GetComponent<TextMesh>();
		txtAbilities = transform.Find("TxtAbilities").GetComponent<TextMesh>();
		
		xml = Gameplay.Instance().xml;
		
		heroesXML = xml.GetNodeList("doc>0>units>0>heroes>0>hero");
		
		transform.parent = GameObject.Find("CustomCursor(Clone)").transform;
		
		mousePosition = MousePosition.Instance();
		
		heroFound = false;
	}
	
	private void Update(){
		heroes = Gameplay.Instance().heroes;
		
		heroFound = false;
		
		DetectHero();
	}
	
	public void LoadHeroInformation(Hero h){
		ClearInformation();
		
		EnablePopup();

		txtRange.text = "Range: " + h.range.ToString();
		txtDamage.text = "Damage: " + h.damage.ToString();
		txtSpeed.text = "Speed: " + h.attackSpeed.ToString();
		txtHealth.text = "Health: " + h.health.ToString();
		txtLevel.text = "LV " + h.lv.ToString();
		txtName.text = h.firstName + h.lastName;
	}

	private void ClearInformation(){
		foreach(TextMesh tm in GetComponentsInChildren<TextMesh>()){
			tm.text = "";
		}
	}
	
	private void EnablePopup(){
		foreach(MeshRenderer r in childrenRenderers){
			r.enabled = true;
		}
	}
	
	private void DisablePopup(){
		foreach(MeshRenderer r in childrenRenderers){
			r.enabled = false;
		}
	}
	
	private void DetectHero(){
		Vector2 popup = new Vector2(mousePosition.position.x, mousePosition.position.y);
	
		foreach(Hero h in heroes){
			Vector2 heroPosition = new Vector2(h.transform.position.x, h.transform.position.y);
			if(Vector2.Distance(popup, heroPosition) <  70f){
				heroFound = true;
				LoadHeroInformation(h);
				break;
			}
		}
	}
	
	private static InfoPopupHeroes instance;
	
	public static InfoPopupHeroes Instance(){
		if(instance == null){
			instance = GameObject.Find("InfoPopup").GetComponent<InfoPopupHeroes>();
			DontDestroyOnLoad(instance);
		}
		return instance;
	}
}

