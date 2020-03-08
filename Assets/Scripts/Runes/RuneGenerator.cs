using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Positions;
using System.Xml;

public class RuneGenerator : MonoBehaviour
{
    static GameObject rune = Resources.Load("Prefabs/Rune") as GameObject;

    public static void CreateRuneXML(XmlNode XMLRune, Transform parent, string[] text, Vector3 position)
    {
        // Instantiates new object
        var _rune = Instantiate(rune, Vector3.zero, Quaternion.identity, parent) as GameObject;
        _rune.name = "Rune";

        // Counters for text
        int text_Counter = 0;

        foreach (XmlNode layer in XMLRune)
        {
            GameObject _layer = new GameObject("Layer");
            _layer.transform.parent = _rune.transform;
            float radius = Convert.ToSingle(layer.Attributes["radius"].Value);
            foreach (XmlNode element in layer)
            {
                switch (element.Name)
                {
                    case "Polygon":
                        {
                            int sides = Convert.ToInt32(element.Attributes["sides"].Value);
                            float lineWidth = Convert.ToSingle(element.Attributes["line-width"].Value);
                            Color color = Color.white;
                            ColorUtility.TryParseHtmlString(element.Attributes["color"].Value, out color);
                            MagicCircleGenerator.createPolygon(_layer.transform, sides, color, radius, lineWidth);
                            break;
                        }
                    case "Glyph":
                        {
                            float fntSize = Convert.ToSingle(element.Attributes["size"].Value);
                            Color color = Color.white;
                            ColorUtility.TryParseHtmlString(element.Attributes["color"].Value, out color);

                            MagicCircleGenerator.createGlyphs(_layer.transform, text[text_Counter], color, radius, fntSize);
                            text_Counter += 1;
                            break;
                        }
                    case "Corners":
                        {
                            GameObject _corner = new GameObject("Corner Container");
                            _corner.transform.parent = _layer.transform;
                            int sides = Convert.ToInt32(element.Attributes["sides"].Value);
                            float angle = 0;
                            for (int i = 0; i < sides; i++)
                            {  
                                Vector3 _corPos = PositionsGenerator.CircleLocation(Vector3.zero, radius, angle);
                                CreateRuneXML(element, _corner.transform, text, _corPos);
                                angle += 360 / sides;
                            }
                            break;
                        }
                }
            }
        }
        _rune.transform.position = position;
    }
}