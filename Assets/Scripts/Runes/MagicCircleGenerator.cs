using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Positions;
using TMPro;

public class MagicCircleGenerator : MonoBehaviour
{
    public static List<GameObject> createGlyphs(Transform parent, string[] text, float radius = 0.2f)
    {
        var glyph = Resources.Load("Prefabs/Glyph") as GameObject;
        List<GameObject> glyphs = new List<GameObject>();
        var _radius = radius;
        for (int ring = 0; ring < text.Length; ring++)
        {
            string phrase = text[ring];
            float length = phrase.Length;
            float angle = 0;
            for (int i = 0; i < phrase.Length; i++)
            {
                Vector3 _pos = PositionsGenerator.CircleLocation(parent.position, _radius, angle);
                Quaternion _rot = Quaternion.FromToRotation(Vector3.right, parent.position - _pos);
                GameObject _glyph = Instantiate(glyph, _pos, _rot, parent);
                TextMeshPro _tmp = _glyph.GetComponent<TextMeshPro>();
                _tmp.text = phrase[i].ToString();
                _tmp.fontSize = 0.5f;
                _tmp.enabled = true;
                glyphs.Add(_glyph);
                angle -= (360f / (length - 1f));
            }
            _radius += 0.1f;
        }
        return glyphs;
    }

    public static List<GameObject> createGlyphs(Transform parent, string text, Color color, float radius, float fontSize = 0.5f)
    {
        var glyph = Resources.Load("Prefabs/Glyph") as GameObject;
        List<GameObject> glyphs = new List<GameObject>();
        float angle = 0;
        float length = text.Length;
        for (int i = 0; i < length; i++)
        {
            Vector3 _pos = PositionsGenerator.CircleLocation(parent.position , radius, angle);
            Quaternion _rot = Quaternion.FromToRotation(Vector3.right, parent.position - _pos);
            GameObject _glyph = Instantiate(glyph, _pos, _rot, parent);
            TextMeshPro _tmp = _glyph.GetComponent<TextMeshPro>();
            _tmp.text = text[i].ToString();
            _tmp.fontSize = fontSize;
            _tmp.color = color;
            _tmp.enabled = true;
            glyphs.Add(_glyph);
            angle -= (360f / (length - 1f));
        }
        return glyphs;
    }

    public static List<GameObject> createGlyphsPolygon(Transform parent, string[] text, int sides, float radius = 0.2f)
    {
        var glyph = Resources.Load("Prefabs/Glyph") as GameObject;
        List<GameObject> glyphs = new List<GameObject>();
        var _radius = radius + 0f;
        float angle_half_internal = (180 - (360 / sides)) / 2;

        // For every layer
        for (int ring = 0; ring < text.Length; ring++)
        {
            string phrase = text[ring];
            float length = phrase.Length;
            float lengthPerSide = length / sides;
            float anglePerSide = 360 / sides;
            // For every side
            float angle = 0;
            float angleElapsed = 0;
            for (int side = 0; side < sides; side++)
            {
                for (int i = 0; i < lengthPerSide; i++)
                {
                    Vector3 _pos = PositionsGenerator.TriangleLocation(parent.position, angleElapsed + angle, angle_half_internal, radius);
                    Quaternion _rot = Quaternion.FromToRotation(Vector3.right, parent.position - _pos);
                    GameObject _glyph = Instantiate(glyph, _pos, _rot, parent);
                    TextMeshPro _tmp = _glyph.GetComponent<TextMeshPro>();
                    _tmp.text = phrase[i].ToString();
                    _tmp.fontSize = 0.5f;
                    _tmp.enabled = true;
                    glyphs.Add(_glyph);
                    angle -= (anglePerSide / lengthPerSide);
                }
                angleElapsed += angle;
            }
            _radius += 0.1f;
        }
        return glyphs;
    }
    public static void adjustGlyph(GameObject obj, string form)
    {
        switch (form)
        {
            case "sphere":
                Vector3 c_Rot = Quaternion.FromToRotation(Vector3.forward, obj.transform.parent.position - obj.transform.position).eulerAngles;
                obj.transform.Rotate(c_Rot);
                break;
            case "vert":
                obj.transform.Rotate(90, 0, 0);
                break;
        }
    }

    public static void createPolygon(Transform parent, int sides, Color color, float radius = 0.1f, float lineWidth = 0.05f)
    {
        var circle = Resources.Load("Prefabs/Line") as GameObject;
        var _circle = Instantiate(circle, Vector3.zero, Quaternion.identity, parent);
        var _line = _circle.GetComponent<LineRenderer>() as LineRenderer;
        FormatLine(_line, lineWidth);
        _line.positionCount = sides + 1;
        var _points = new Vector3[_line.positionCount];
        for (int i = 0; i < _line.positionCount; i++)
        {
            var ang = Mathf.Deg2Rad * (i * 360 / sides);
            _points[i] = PositionsGenerator.CircleLocation(radius, ang, "z");
        }
        _circle.transform.position = parent.position;
        _line.SetPositions(_points);
        _line.startColor = color;
        _line.endColor = color;
        _line.useWorldSpace = false;
        _line.numCapVertices = _line.numCornerVertices = sides;
    }

    public static void createPolygonCorners(Transform parent, int sides, Color color, float radius = 0.5f, float linewidth = 0.02f)
    {
        var rune = Resources.Load("Prefabs/Rune") as GameObject;
        Vector3[] CircleLocation = new Vector3[sides + 1];
        for (int i = 0; i < sides + 1; i++)
        {
            var ang = Mathf.Deg2Rad * (i * 360f / (sides));
            Vector3 _C = parent.position + PositionsGenerator.CircleLocation(radius, ang, "z");
            GameObject _d = Instantiate(rune, _C, Quaternion.identity, parent);
            createPolygon(_d.transform, 360, color, 0.07f, linewidth);
            CircleLocation[i] = _C - parent.position;
        }
    }

    public static void ChainPoints(Transform parent, Vector3[] points, int sides, float lineWidth = 0.01f)
    {
        var lines = Resources.Load("Prefabs/Line") as GameObject;
        GameObject _lines = Instantiate(lines, parent);
        var _line = _lines.GetComponent<LineRenderer>();
        _line = FormatLine(_line, lineWidth);
        _line.positionCount = sides + 1;
        _line.SetPositions(points);
        _line.useWorldSpace = false;
    }

    static LineRenderer FormatLine(LineRenderer _line, float lineWidth)
    {
        _line.material = Resources.Load("Materials/DefaultWhite") as Material;
        _line.startColor = Color.white;
        _line.endColor = Color.white;
        _line.startWidth = lineWidth;
        _line.endWidth = lineWidth;
        _line.useWorldSpace = false;
        return _line;
    }
}
