using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class Gameplay : MonoBehaviour {

	private XMLNodeList wavesXML;
	public XMLNodeList enemiesXML;
	private XMLNode waveXML;
	//private XMLNode enemyXML;
	public XMLNode xml;
	
	public List<Hero> heroes;
	public List<Enemy> enemies;
	
	private int numberOfWaves;
	private int wave;
	public int playerGold;
	public int playerLives;
	public int numberOfHeroes;
	public int maxHeroes;
	public int enemiesLeft;
	
	
	private bool paused = false;
	
	
	public void Awake(){
		xml = XMLParser.Parse((Resources.Load("data", typeof(TextAsset)) as TextAsset).text);
		
		wavesXML = xml.GetNodeList("doc>0>waves>0>wave");
		enemiesXML = xml.GetNodeList("doc>0>units>0>enemies>0>enemy");
		
		heroes = new List<Hero>();
		enemies = new List<Enemy>();

		wave = 0;
		numberOfWaves = 20;
		playerGold = 1000;
		playerLives = 20;
		numberOfHeroes = 0;
		maxHeroes = 12;
		CustomCursor c = Instantiate(Resources.Load(("CustomCursor"), typeof(CustomCursor)) as CustomCursor) as CustomCursor;
	}
	
	public void FixedUpdate(){
		
	}
	
	public void Update(){
		if(playerLives <= 0)
			Defeat();
		if (Input.GetKeyDown ("escape")) 
			Exit();
		if(Input.GetKeyDown("p"))
			Pause();
		if(wave > numberOfWaves)
			Victory();

		TextMesh textMesh;
		textMesh = GameObject.Find("TxtPlayerLives").GetComponent<TextMesh>();
		textMesh.text = "Player Lives: " + playerLives;
		
		textMesh = GameObject.Find("TxtWaveNumber").GetComponent<TextMesh>();
		textMesh.text = "Wave " + wave;
		
		textMesh = GameObject.Find("TxtPlayerGold").GetComponent<TextMesh>();
		textMesh.text = playerGold + " gold";
		
		textMesh = GameObject.Find("TxtEnemiesLeft").GetComponent<TextMesh>();
		textMesh.text = "Enemies left: " + enemiesLeft;
		
		numberOfHeroes = heroes.Count;
		
		if(enemiesLeft == 0)
			EndWave();
	}
	
	private void Pause(){
		paused = !paused;
		
		if(paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
	
	private void Exit(){
		Application.Quit ();
	}
	
	public void StartWave(){
		int numberSpawned = 0;
		float offset = 400f;
		
		waveXML = wavesXML[wave] as XMLNode;
		
		string uniqueValue = waveXML.GetValue("@unique");
		
		if(uniqueValue != ""){
			int unique = int.Parse(uniqueValue);
		}
		
		int dc = int.Parse(waveXML.GetValue("@dc"));
		
		int toSpawn =  int.Parse(waveXML.GetValue("@tospawn"));
		
		while(toSpawn > 0){
			int x = dc - 1;
			
			XMLNode enemyXML = enemiesXML[x] as XMLNode;
			Enemy e = Instantiate(Resources.Load("Enemies/" + enemyXML.GetValue("@type"), typeof(Enemy)) as Enemy) as Enemy;

			e.Birth(enemyXML);
			enemies.Add(e);
			e.index = enemies.IndexOf(e);
			
			e.name = e.name.Split("("[0])[0];
			
			toSpawn--;
			
			Transform spawner = GameObject.Find("Spawner").transform;
			e.transform.position = new Vector3(spawner.position.x, spawner.position.y + (offset * numberSpawned), -20f);
			numberSpawned++;
		}

		wave++;
		
		enemiesLeft = numberSpawned;
		
		GameObject btn;
		
		btn = GameObject.Find("BtnReady");
		btn.GetComponent<Collider>().enabled = false;
		btn.GetComponent<Renderer>().enabled = false;
	}
	
	private void EndWave(){
		GameObject btn;
	
		btn = GameObject.Find("BtnReady");
		btn.GetComponent<Collider>().enabled = true;
		btn.GetComponent<Renderer>().enabled = true;
	}
	
	private void Victory(){
	}
	
	private void Defeat(){
	}
	
	private static Gameplay instance;
	public static Gameplay Instance(){
		if(instance == null){
			instance = GameObject.Find("Gameplay").GetComponent<Gameplay>();
			DontDestroyOnLoad(instance);
		}
		return instance;
	}
}
