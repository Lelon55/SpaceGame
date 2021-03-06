﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class XmlOperations : MonoBehaviour
{
    internal class MemberList
    {
        public int Value;

        public MemberList(int value)
        {
            Value = value;
        }
    }

    internal List<MemberList> AllyID = new List<MemberList>();
    internal List<MemberList> AllyMemberID = new List<MemberList>();

    internal XDocument documentXML;

    internal void SaveXML(string path)
    {
        documentXML.Save(GetPath(path));
    }

    internal XDocument LoadXML(string path)
    {
        return XDocument.Load(GetPath(path));
    }

    internal void CreateXMLFile(string path, int memberID, int ID, string name, int point, int life, int steer, float maxLasers)
    {
        documentXML = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Allies",
                    new XElement("Ally",
                        new XAttribute("MemberID", memberID),
                        new XElement("ID", ID),
                        new XElement("Name", name),
                        new XElement("Point", point),
                        new XElement("Life", life),
                        new XElement("Steer", steer),
                        new XElement("MaxLasers", maxLasers))));
        SaveXML(path);
    }

    internal void ClearFile(string path)
    {
        XDocument root = LoadXML(path);

        var query = from ally in root.Root.Elements("Ally")
                    select ally;

        foreach (var item in query)
        {
            item.Remove();
            documentXML = root.Document;
            SaveXML(path);
        }
    }

    internal void AddAlly(string path, int memberID, int ID, string name, int point, int life, int steer, float maxLasers)
    {
        XElement root = LoadXML(path).Root;
        root.Add(new XElement("Ally",
                new XAttribute("MemberID", memberID),
                new XElement("ID", ID),
                new XElement("Name", name),
                new XElement("Point", point),
                new XElement("Life", life),
                new XElement("Steer", steer),
                new XElement("MaxLasers", maxLasers)));
        documentXML = root.Document;
        SaveXML(path);
    }

    internal void DeleteAlly(string path, int memberID)
    {
        XDocument root = LoadXML(path);
        //find the ally with MemberID = memberID
        var query = from ally in root.Root.Elements("Ally")
                    where int.Parse(ally.Attribute("MemberID").Value) == memberID
                    select ally;

        foreach (var item in query)
        {
            item.Remove();
            documentXML = root.Document;
            SaveXML(path);
        }
    }

    internal void LoadAllyID(string path)
    {
        XDocument root = LoadXML(path);
        var queryID = from allyID in root.Root.Descendants("ID")
                      select allyID;

        foreach (var item in queryID)
        {
            AllyID.Add(new MemberList(int.Parse(item.Value)));
        }
    }

    internal void LoadAllyMemberID(string path)
    {
        XDocument root = LoadXML(path);
        var queryID = from allyID in root.Root.Elements("Ally").Attributes("MemberID")
                      select allyID;

        foreach (var item in queryID)
        {
            AllyMemberID.Add(new MemberList(int.Parse(item.Value)));
        }
    }

    internal string LoadAllyData(string path, string withTag, int memberID)
    {
        XDocument root = LoadXML(path);
        var queryID = from allyID in root.Root.Elements("Ally")
                      where int.Parse(allyID.Attribute("MemberID").Value) == memberID
                      select allyID.Element(withTag);

        foreach (var item in queryID)
        {
            return item.Value;
        }
        return "";
    }

    internal int CountItems(string path, string withTag)
    {
        XDocument root = LoadXML(path);
        return root.Descendants(withTag).Count();
    }

    internal string GetPath(string fileName)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                return Application.dataPath + "/Resources/" + fileName;
            case RuntimePlatform.Android:
                return "jar:file://" + Application.dataPath + "/Resources/" + fileName;
            case RuntimePlatform.IPhonePlayer:
                return Application.persistentDataPath + "/Resources/" + fileName;
            default:
                return fileName;
        }
    }
}