using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XmlBattleLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LoadXml();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void LoadXml (){
		//创建xml文档
		XmlDocument xml = new XmlDocument ();
		xml.Load (Application.dataPath + "/Xmldata.xml");
		//得到Object节点下的所有子节点
		XmlNodeList xmlNodeList = xml.SelectSingleNode ("Object").ChildNodes;
		foreach(XmlElement xl1 in xmlNodeList) {
			if (xl1.GetAttribute ("Id") == "1") {
				//继续遍历Id为1的节点下的子节点
				foreach(XmlElement xl2 in xl1) {
					//判断是否是Name == Any的
					if (xl2.GetAttribute ("Name") == "Any") {
						print (xl2.GetAttribute ("Name")+ " : " + xl2.InnerText);
					}
					//判断是否是Task == First的
					if (xl2.GetAttribute ("Task") == "First") {
						print (xl2.GetAttribute ("Task")+ " : " + xl2.InnerText);
					}
				}
			}
		}
		print(xml.OuterXml);
	}
}
