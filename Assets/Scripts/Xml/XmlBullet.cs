using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class XmlBullet
{
    //子弹ID
    public int id;
    //子弹预制件
    public string prefabs;
    //子弹伤害
    public int damage;
    //子弹速度X
    public float speedX;
    //子弹速度Y
    public float speedY;
    //方向(偏正对手的修正)
    public float directionOffsetX;
    public float directionOffsetY;
    //位置偏移量
    public float positionOffsetX;
    public float positionOffsetY;
    //冷却时间
    public float coolDown;

    public XmlBullet()
    {
    }
    public void Clear()
    {

    }
}

public class XmlBulletMgr{
    public Dictionary<int, XmlBullet> bulletDictionary;

    public XmlBulletMgr()
    {
        this.bulletDictionary = new Dictionary<int, XmlBullet>();
    }

	public void LoadXml (){
        this.bulletDictionary.Clear();

        XmlDocument xml = new XmlDocument();
        string xmlPath = Application.dataPath + "/Config/bullet.xml";
        xml.Load(xmlPath);
        XmlElement rootElem = xml.DocumentElement;
        XmlNodeList nodes = rootElem.GetElementsByTagName("bullet");
        foreach (XmlNode node in nodes)
        {
            XmlBullet xmlBullet = new XmlBullet();
            xmlBullet.id = int.Parse(((XmlElement)node).GetAttribute("id"));
            xmlBullet.prefabs = ((XmlElement)node).GetAttribute("prefabs");
            GameObject planePrefabs = (GameObject)Resources.Load(xmlBullet.prefabs);
            if (null == planePrefabs)
            {
                Debug.LogErrorFormat("XmlBulletMgr未找到{0}", xmlBullet.prefabs);
            }
            xmlBullet.damage = int.Parse(((XmlElement)node).GetAttribute("damage"));
            {
                string str = ((XmlElement)node).GetAttribute("speed");
                string[] data = str.Split(',');
                xmlBullet.speedX = float.Parse(data[0]);
                xmlBullet.speedY = float.Parse(data[1]);
            }
            {
                string str = ((XmlElement)node).GetAttribute("directionOffset");
                string[] data = str.Split(',');
                xmlBullet.directionOffsetX = float.Parse(data[0]);
                xmlBullet.directionOffsetY = float.Parse(data[1]);
            }
            {
                string str = ((XmlElement)node).GetAttribute("positionOffset");
                string[] data = str.Split(',');
                xmlBullet.positionOffsetX = float.Parse(data[0]);
                xmlBullet.positionOffsetY = float.Parse(data[1]);
            }
            xmlBullet.coolDown = float.Parse(((XmlElement)node).GetAttribute("coolDown"));

            this.bulletDictionary.Add(xmlBullet.id, xmlBullet);
        }
    }
    public XmlBullet Find(int bulletId)
    {
        if (this.bulletDictionary.ContainsKey(bulletId))
        {
            return this.bulletDictionary[bulletId];
        }
        XmlBullet xmlBullet = new XmlBullet();
        return xmlBullet;
    }
}
