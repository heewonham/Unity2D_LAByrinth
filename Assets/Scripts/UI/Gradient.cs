//==================================
// Author : Cha Kyoung Hoon
// Create : 2015-11-19
// Last Modify : 2015-11-19
// Notes : For Gradient Background
//==================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gradient : MonoBehaviour {
    public enum ANCHOR { 
        TopLeft, TopCenter, TopRight, 
        MiddleLeft, MiddleCenter, MiddleRight, 
        BottomLeft, BottomCenter, BottomRight 
    }

    public enum DIRECTION { Vertical, Horizontal }

    public bool isBuildOnAwake;
    public Vector2 size = Vector2.one;
    public ANCHOR anchor = ANCHOR.MiddleCenter;
    public DIRECTION direction = DIRECTION.Vertical;
    public Color[] colors = new Color[]{ Color.white, Color.white};

    Shader shader;
    List<GameObject> panels = new List<GameObject>();

    void Awake () {
        shader = Shader.Find("BackgroundGradient");

        if (isBuildOnAwake) Build();
    }

    public void Build () {
        if (colors.Length < 2) {
            Debug.LogWarning("The color number must be greater than one.");
            return;
        }

        float unitSize = (direction == DIRECTION.Vertical) ? size.y/(colors.Length-1) : size.x/(colors.Length-1);

        for (int i = 0; i < colors.Length - 1; i++) {
            GameObject panel = new GameObject("Panel");
            MeshFilter mf = panel.AddComponent<MeshFilter>();
            MeshRenderer mr = panel.AddComponent<MeshRenderer>();

            if (direction == DIRECTION.Vertical) {
                mf.mesh = GetMesh(size.x, unitSize);
            } else {
                mf.mesh = GetMesh(unitSize, size.y);
            }
                
            mr.material.shader = shader;
            mr.material.SetColor("_Color", colors[i]);
            mr.material.SetColor("_Color2", colors[i+1]);

            panels.Add(panel);
        }

        UpdateAlign();
    }

    void UpdateAlign () {
        if (direction == DIRECTION.Vertical) {
            UpdateAlignVertical(size.x * 0.5F, (size.y / (colors.Length-1)) * 0.5F);
        } else {
            UpdateAlignHorizontal((size.x/ (colors.Length-1)) * 0.5F, size.y * 0.5F);
        }
    }

    void UpdateAlignVertical (float w, float h) {
        Vector3 anchorPos = Vector3.zero;

        switch (anchor) {
            case ANCHOR.TopLeft : 
                anchorPos = new Vector3(w, -h, 0F);
                break;
            case ANCHOR.TopCenter : 
                anchorPos = new Vector3(0F, -h, 0F);
                break;
            case ANCHOR.TopRight : 
                anchorPos = new Vector3(-w, -h, 0F);
                break;
            case ANCHOR.MiddleLeft : 
                anchorPos = new Vector3(w, (panels.Count - 1) * h, 0F);
                break;
            case ANCHOR.MiddleCenter : 
                anchorPos = new Vector3(0F, (panels.Count - 1) * h, 0F);
                break;
            case ANCHOR.MiddleRight : 
                anchorPos = new Vector3(-w, (panels.Count - 1) * h, 0F);
                break;
            case ANCHOR.BottomLeft : 
                anchorPos = new Vector3(w, (((panels.Count - 1) * h) * 2F) + h, 0F);
                break;
            case ANCHOR.BottomCenter : 
                anchorPos = new Vector3(0F, (((panels.Count - 1) * h) * 2F) + h, 0F);
                break;
            case ANCHOR.BottomRight : 
                anchorPos = new Vector3(-w, (((panels.Count - 1) * h) * 2F) + h, 0F);
                break;
        }
        
        for (int i = 0; i < panels.Count; i++) {
            Transform tf = panels[i].GetComponent<Transform>();
            tf.parent = transform;
            tf.localPosition = Vector3.zero;

            Vector3 newPos = anchorPos;
            newPos.y -= (h * 2F * i);
            tf.localPosition = newPos;
        }
    }

    void UpdateAlignHorizontal (float w, float h) {
        Vector3 anchorPos = Vector3.zero;

        switch (anchor) {
            case ANCHOR.TopLeft : 
                anchorPos = new Vector3(w, -h, 0F);
                break;
            case ANCHOR.MiddleLeft : 
                anchorPos = new Vector3(w, 0F, 0F);
                break;
            case ANCHOR.BottomLeft : 
                anchorPos = new Vector3(w, h, 0F);
                break;
            case ANCHOR.TopCenter : 
                anchorPos = new Vector3(-((panels.Count - 1) * w), -h, 0F);
                break;
            case ANCHOR.MiddleCenter : 
                anchorPos = new Vector3(-((panels.Count - 1) * w), 0F, 0F);
                break;
            case ANCHOR.BottomCenter : 
                anchorPos = new Vector3(-((panels.Count - 1) * w), h, 0F);
                break;
            case ANCHOR.TopRight : 
                anchorPos = new Vector3(-(((panels.Count - 1) * w) * 2F) - w, -h, 0F);
                break;
            case ANCHOR.MiddleRight : 
                anchorPos = new Vector3(-(((panels.Count - 1) * w) * 2F) - w, 0F, 0F);
                break;
            case ANCHOR.BottomRight : 
                anchorPos = new Vector3(-(((panels.Count - 1) * w) * 2F) - w, h, 0F);
                break;
        }
        
        for (int i = 0; i < panels.Count; i++) {
            Transform tf = panels[i].GetComponent<Transform>();
            tf.parent = transform;
            tf.localPosition = Vector3.zero;

            Vector3 newPos = anchorPos;
            newPos.x += (w * 2F * i);
            tf.localPosition = newPos;
        }
    }

    Mesh GetMesh (float _width, float _height) {
        Mesh m = new Mesh();
        m.name = "BackgroundGradient";

        float w = _width * 0.5F;
        float h = _height * 0.5F;

        m.vertices = new Vector3[] {
            new Vector3(-w, h, 0F),
                new Vector3(w, h, 0F),
                new Vector3(-w, -h, 0F),
                new Vector3(w, -h, 0F)
        };

        if (direction == DIRECTION.Vertical) {
            m.uv = new Vector2[] {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };
        } else {
            m.uv = new Vector2[] {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(1, 1)
            };
        }

        m.triangles = new int[] { 0, 1, 2, 2, 1, 3};
        m.RecalculateNormals();

        return m;
    }
}
