using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class XmlGameLevel
{
    public List<string> bgPrefabsList;
    public XmlGameLevel()
    {
        this.bgPrefabsList = new List<string>();
    }
    public void Clear()
    {
        this.bgPrefabsList.Clear();
    }
    public void AddBgPrefabs(string bgPrefabsName)
    {
        this.bgPrefabsList.Add(bgPrefabsName);
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
            int gameLevelId = int.Parse(((XmlElement)gameLevelNode).GetAttribute("id"));
            XmlNodeList bgNodes = ((XmlElement)gameLevelNode).GetElementsByTagName("bg");
            XmlGameLevel xmlGameLevel = new XmlGameLevel();
            foreach (XmlNode bgNode in bgNodes)
            {
                string prefabs = ((XmlElement)bgNode).GetAttribute("prefabs");
                xmlGameLevel.AddBgPrefabs(prefabs);
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
