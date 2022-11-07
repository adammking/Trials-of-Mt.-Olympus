using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;


public class DataManager : MonoBehaviour
{


    public Ability LoadAbilityData(string abilityToLoad)
    {
        string path = $"{Application.dataPath}/Data/Abilities/{abilityToLoad}.xml";

        XmlSerializer serializer = new XmlSerializer(typeof(Ability));
        StreamReader reader = new StreamReader(path);
        Ability deserialized = (Ability)serializer.Deserialize(reader.BaseStream);
        reader.Close();

        return deserialized;

    }

    public Ability LoadAbilityDataFullPath(string fullPath)
    {

        string path = fullPath;

        XmlSerializer serializer = new XmlSerializer(typeof(Ability));
        StreamReader reader = new StreamReader(path);
        Ability deserialized = (Ability)serializer.Deserialize(reader.BaseStream);
        reader.Close();

        return deserialized;

    }

    public void Printhing()
    {
        print("Nice :)");
    }

}