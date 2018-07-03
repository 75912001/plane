using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class XmlPlane
{
    public int id;
    public string prefabs;
    public int damage;
    public float speedX;
    public float speedY;
    public int hp;

    public XmlPlane()
    {
    }
    public void Clear()
    {
    }
}

public class XmlPlaneMgr{
    public Dictionary<int, XmlPlane> planeDictionary;

    public XmlPlaneMgr()
    {
        this.planeDictionary = new Dictionary<int, XmlPlane>();
    }

	public void LoadXml (){
        this.planeDictionary.Clear();

        XmlDocument xml = new XmlDocument();
        string xmlPath = Application.dataPath + "/Config/plane.xml";
        xml.Load(xmlPath);
        XmlElement rootElem = xml.DocumentElement;
        XmlNodeList planeNodes = rootElem.GetElementsByTagName("plane");
        foreach (XmlNode planeNode in planeNodes)
        {
            XmlPlane xmlPlane = new XmlPlane();
            xmlPlane.id = int.Parse(((XmlElement)planeNode).GetAttribute("id"));
            xmlPlane.prefabs = ((XmlElement)planeNode).GetAttribute("prefabs");
            GameObject planePrefabs = (GameObject)Resources.Load(xmlPlane.prefabs);
            if (null == planePrefabs)
            {
                Debug.LogErrorFormat("XmlPlaneMgr未找到{0}", xmlPlane.prefabs);
            }
            xmlPlane.damage = int.Parse(((XmlElement)planeNode).GetAttribute("damage"));
            xmlPlane.hp = int.Parse(((XmlElement)planeNode).GetAttribute("hp"));
            {
                string str = ((XmlElement)planeNode).GetAttribute("speed");
                string[] data = str.Split(',');
                xmlPlane.speedX = float.Parse(data[0]);
                xmlPlane.speedY = float.Parse(data[1]);
            }

            this.planeDictionary.Add(xmlPlane.id, xmlPlane);
        }
    }
    public XmlPlane Find(int planeId)
    {
        if (this.planeDictionary.ContainsKey(planeId))
        {
            return this.planeDictionary[planeId];
        }
        XmlPlane xmlPlane = new XmlPlane();
        return xmlPlane;
    }
}
