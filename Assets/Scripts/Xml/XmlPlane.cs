﻿using UnityEngine;
using System.Xml;
using System.Collections.Generic;


public class Item
{
    public int id;
    public int cnt;
}

public class XmlPlane
{
    public int id;
    public string prefabs;
    public int damage;
    public float speedX;
    public float speedY;
    public int hp;
    public List<int> bulletList;
    public List<Item> dropItemList;

    public XmlPlane()
    {
        this.bulletList = new List<int>();
        this.dropItemList = new List<Item>();
    }
    public void Clear()
    {
        this.bulletList.Clear();
        this.dropItemList.Clear();
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
        XmlNodeList nodes = rootElem.GetElementsByTagName("plane");
        foreach (XmlNode node in nodes)
        {
            XmlPlane xmlPlane = new XmlPlane();
            xmlPlane.id = int.Parse(((XmlElement)node).GetAttribute("id"));
            xmlPlane.prefabs = ((XmlElement)node).GetAttribute("prefabs");
            GameObject planePrefabs = (GameObject)Resources.Load(xmlPlane.prefabs);
            if (null == planePrefabs)
            {
                Debug.LogErrorFormat("XmlPlaneMgr未找到{0}", xmlPlane.prefabs);
            }
            xmlPlane.damage = int.Parse(((XmlElement)node).GetAttribute("damage"));
            xmlPlane.hp = int.Parse(((XmlElement)node).GetAttribute("hp"));
            {
                string str = ((XmlElement)node).GetAttribute("speed");
                string[] data = str.Split(',');
                xmlPlane.speedX = float.Parse(data[0]);
                xmlPlane.speedY = float.Parse(data[1]);
            }
            {
                string str = ((XmlElement)node).GetAttribute("bullets");
                string[] data = str.Split(',');
                foreach (var v in data)
                {
                    xmlPlane.bulletList.Add(int.Parse(v));
                }
            }
            #region 掉落物品
            {
                string str = ((XmlElement)node).GetAttribute("dropItems");
                if (0 != str.Length)
                {
                    string[] strItems = str.Split(';');
                    foreach (var vItems in strItems)
                    {
                        string[] strItem = vItems.Split(',');
                        Item item = new Item();
                        item.id = int.Parse(strItem[0]);
                        item.cnt = int.Parse(strItem[1]);
                        xmlPlane.dropItemList.Add(item);
                    }
                }
            }
            #endregion

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
