using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RandomLetterGenerator;
using System.Xml;

public class RuneController : MonoBehaviour
{
    RLgenerator rlg;
    void Start()
    {
        // Init the RLG
        rlg = new RLgenerator(Random.state.GetHashCode());
        rlg.setString_Set("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
    }

    void Update()
    {
        string[] text = rlg.GenText(10, 5, 7);
        if (Input.GetKeyDown("f")) {
            RuneGenerator.CreateRuneXML(PathToXML("Rune_Instructions/SampleRune"), gameObject.transform, text, transform.position);
        } else if (Input.GetKeyDown("g")) {
            RuneGenerator.CreateRuneXML(PathToXML("Rune_Instructions/SampleRune2"), gameObject.transform, text, transform.position);
        }
    }

    public static XmlNode PathToXML (string path) {
        XmlDocument RuneInstruction = new XmlDocument();
        TextAsset xml = (TextAsset)Resources.Load(path, typeof(TextAsset));
        RuneInstruction.LoadXml(xml.text);
        XmlNode XMLRune = RuneInstruction.DocumentElement.SelectSingleNode("/Rune");
        return XMLRune;
    }
}