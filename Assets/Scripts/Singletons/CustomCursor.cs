using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {	

	private Vector3 mousePosition;
	
	private void Awake () 
	{
		Cursor.visible = false;
		
		GameObject[] cursors = GameObject.FindGameObjectsWithTag("Cursor");
		foreach(GameObject c in cursors){
			if(cursors.Length == 1)
				return;
			else
				Destroy(c);
		}
	}
	
	private void Update(){
		mousePosition = MousePosition.Instance().position;
	}
	
	private void LateUpdate(){
		Move();
	}
	
	private void Move() 
	{
		float offset = 40f;
		float UIOffset = 2000f;
		transform.position = new Vector3(mousePosition.x + offset, mousePosition.y - offset - UIOffset, -50f);
	}
}
