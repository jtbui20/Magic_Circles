using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class RuneController : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("f")) {
            RuneGenerator.CreateRuneXML(PathToXML("Rune_Instructions/SampleRune"), gameObject.transform, transform.position);
        } else if (Input.GetKeyDown("g")) {
            RuneGenerator.CreateRuneXML(PathToXML("Rune_Instructions/SampleRune2"), gameObject.transform, transform.position);
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