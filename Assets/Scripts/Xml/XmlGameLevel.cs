using UnityEngine;
using System.Xml;
using System.Collections.Generic;


public class XmlGameLevelEnemy
{
    public int planeId;
    public float enterTime;
    public float enterX;
    public float enterY;
    public float directionX;
    public float directionY;
}
public class XmlGameLevel
{
    public List<string> bgPrefabsList;
    public List<XmlGameLevelEnemy> enemyList;
    public XmlGameLevel()
    {
        this.bgPrefabsList = new List<string>();
        this.enemyList = new List<XmlGameLevelEnemy>();
    }
    public void Clear()
    {
        this.bgPrefabsList.Clear();
        this.enemyList.Clear();
    }
    public void AddBgPrefabs(string bgPrefabsName)
    {
        this.bgPrefabsList.Add(bgPrefabsName);
    }
    public void AddEnemy(XmlGameLevelEnemy enemy)
    {
        this.enemyList.Add(enemy);
    }
}

public class XmlGameLevelMgr{
    public Dictionary<int, XmlGameLevel> gameLevelDictionary;

	public XmlGameLevelMgr(){
        this.gameLevelDictionary = new Dictionary<int, XmlGameLevel>();
    }

	public void LoadXml (){
        this.gameLevelDictionary.Clear();

        XmlDocument xml = new XmlDocument();
        string xmlPath = Application.dataPath + "/Config/gameLevel.xml";
        xml.Load(xmlPath);
        XmlElement rootElem = xml.DocumentElement;
        XmlNodeList gameLevelNodes = rootElem.GetElementsByTagName("gameLevel");
        foreach (XmlNode gameLevelNode in gameLevelNodes)
        {
            XmlGameLevel xmlGameLevel = new XmlGameLevel();
            int gameLevelId = int.Parse(((XmlElement)gameLevelNode).GetAttribute("id"));

            {
                XmlNodeList nodes = ((XmlElement)gameLevelNode).GetElementsByTagName("bg");
                foreach (XmlNode node in nodes)
                {
                    string prefabs = ((XmlElement)node).GetAttribute("prefabs");
                    xmlGameLevel.AddBgPrefabs(prefabs);
                }
            }
            {
                XmlNodeList nodes = ((XmlElement)gameLevelNode).GetElementsByTagName("enemy");
                foreach (XmlNode node in nodes)
                {
                    XmlGameLevelEnemy enemy = new XmlGameLevelEnemy();
                    enemy.planeId = int.Parse(((XmlElement)node).GetAttribute("planeId"));
                    enemy.enterTime = float.Parse(((XmlElement)node).GetAttribute("enterTime"));
                    {
                        string str = ((XmlElement)node).GetAttribute("enter");
                        string[] data = str.Split(',');
                        enemy.enterX = float.Parse(data[0]);
                        enemy.enterY = float.Parse(data[1]);
                    }

                    {
                        string str = ((XmlElement)node).GetAttribute("direction");
                        string[] data = str.Split(',');
                        enemy.directionX = float.Parse(data[0]);
                        enemy.directionY = float.Parse(data[1]);
                    }

                    xmlGameLevel.AddEnemy(enemy);
                }
            }
            this.gameLevelDictionary.Add(gameLevelId, xmlGameLevel);
        }
    }
    public XmlGameLevel Find(int gameLevel)
    {
        if (this.gameLevelDictionary.ContainsKey(gameLevel))
        {
            return this.gameLevelDictionary[gameLevel];
        }
        XmlGameLevel xmlGameLevel = new XmlGameLevel();
        return xmlGameLevel;
    }
}
