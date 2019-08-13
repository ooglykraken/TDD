using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class XML:MonoBehaviour {
	
	private XMLNode data;
	
	public void Awake() {
		data = Parse((Resources.Load("data", typeof(TextAsset)) as TextAsset).text);
	}
	
	public XMLNode Parse(string xml) {
		xml = xml.Replace("&amp;", "&");
		xml = xml.Replace("&nbsp;", " ");
		xml = xml.Replace(" <br> ", "\n");
		xml = xml.Replace(" <br/> ", "\n");
		xml = xml.Replace("<br>", "\n");
		xml = xml.Replace("<br/>", "\n");
		
		XMLNode xmlNode = XMLParser.Parse(xml);
		
		return xmlNode;
	}
	
	public XMLNode GetChildNode(XMLNode target, string value) {
		XMLNodeList list = target.GetNodeList(value);
		if (list == null)
			return null;
		return list[0] as XMLNode;
	}
	
	public XMLNode GetData() {
		return data;
	}
	
	public string GetTextFormatted(XMLNode node, TextMesh textMesh){
		string text = node.GetValue("_text");
		return text;	
	}
	
	public XMLNodeList GetScenes() {
		return data.GetNodeList("doc>0>scenes>0>scene");
	}	
 	
	public XMLNode GetScene(string value) {
		XMLNodeList scenes = GetScenes();
		
		int i;
		for ( i = 0; i < scenes.Count; i++ ){
			XMLNode scene = scenes[i] as XMLNode;
			if(scene.GetValue("@name") == value){
				return scene;	
			}
		}
		
		Debug.Log("No Scene Found");
		
		return null;
	}
	
	
	
	public string AddLineBreaks(string value, int limit) {
		if (value.Length <= limit)
			return value;

		string[] valueArray = value.Split(" "[0]);
		List<string> outputArray = new List<string>();
		
		int i;
		for (i = 0; i < valueArray.Length; i++) {
			string str = valueArray[i].ToString();
			if (outputArray.Count > 0) {
				string current = outputArray[outputArray.Count - 1].ToString();
				string spacing = "";
				if (current.Length > 0)
					spacing = " ";
				string next =  current + spacing + str;
				if (next.Length > limit) {
					outputArray.Add(str);
				}
				else {
					outputArray[outputArray.Count - 1] = next;
				}
			}
			else {
				outputArray.Add(str);
			}
		}
		
		string output = "";
		for (i = 0; i < outputArray.Count; i++) {
			if (i > 0)
				output += "\n";
			output += outputArray[i].ToString();
		}
		
		return output;
	}
	
	public XMLNodeList GetVideoList(string value) {
		XMLNode scene = GetScene(value);
		XMLNodeList videos = scene.GetNodeList("videos>0>video");
		return videos;
	}	
	public XMLNodeList GetPdfList(string value) {
		XMLNode scene = GetScene(value);
		XMLNodeList pdfs = scene.GetNodeList("pdfs>0>pdf");
		return pdfs;
	}	
	
	public int GetInstanceCount(string value, string target){
        string result = target.Replace(value, "");
        return (target.Length - result.Length) / value.Length;
    }
	
	private static XML instance;
	public static XML Instance() {
		if (instance == null) {
			instance = new GameObject("XML").AddComponent<XML>();
			DontDestroyOnLoad(instance);
		}

		return instance;
	}
}