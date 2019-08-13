using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class InfoPopup : MonoBehaviour {
	
	private List<MeshRenderer> childrenRenderers = new List<MeshRenderer>();
	
	public bool displaying;
	
	private int tavernLevel;
	
	private TextMesh txtName;
	private TextMesh txtLevel;
	private TextMesh txtHealth;
	private TextMesh txtDamage;
	private TextMesh txtSpeed;
	private TextMesh txtRange;
	private TextMesh txtAbilities;
	
	private XMLNode xml;
	private XMLNodeList heroesXML;

	public void Awake(){
		foreach(MeshRenderer r in GetComponentsInChildren<MeshRenderer>()){
			childrenRenderers.Add(r);
		}
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
	}
	
	public void Update(){
		if(!displaying){
			DisablePopup();
		}
	}
	
	public void LoadHeroInformation(int argument){
		ClearInformation();
		
		EnablePopup();
		
		tavernLevel = Tavern.Instance().level;
		
		XMLNode heroXML = heroesXML[argument] as XMLNode;
		
		string className = heroXML.GetValue("@class");
		
		XMLNode levelXML = heroXML.GetNodeList("levels>0>level")[tavernLevel] as XMLNode;
		
		Debug.Log(className);
		
		txtRange.text = "Range: " + levelXML.GetValue("@range");
		txtDamage.text = "Damage: " + levelXML.GetValue("@damage");
		txtSpeed.text = "Speed: " + levelXML.GetValue("@speed");
		txtHealth.text = "Health: " + levelXML.GetValue("@health");
		txtLevel.text = "LV " + tavernLevel.ToString();
		txtName.text = className;
	}
	
	public void LoadTavernInformation(){
		ClearInformation();
		
		EnablePopup();
		
		txtName.text = "Tavern";
		txtLevel.text = "LV " + Tavern.Instance().level.ToString();
		txtDamage.text = "Cost to Level";
		txtSpeed.text = "Up the tavern";
		txtAbilities.text = Tavern.Instance().levelUpCost.ToString() + " Gold";
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
	
	private static InfoPopup instance;
	
	public static InfoPopup Instance(){
		if(instance == null){
			instance = GameObject.Find("InfoPopup").GetComponent<InfoPopup>();
			DontDestroyOnLoad(instance);
		}
		return instance;
	}
}
