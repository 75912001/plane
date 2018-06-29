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
            xmlPlane.damage = int.Parse(((XmlElement)planeNode).GetAttribute("damage"));
            xmlPlane.speedX = float.Parse(((XmlElement)planeNode).GetAttribute("speedX"));
            xmlPlane.speedY = float.Parse(((XmlElement)planeNode).GetAttribute("speedY"));

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
