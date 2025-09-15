using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class SceneGraphNode
{
    string name;
    public MyMatrix localTransform;
    GameObject gameObject;

    public SceneGraphNode(string pName)
    {
        name = pName;
    }
    public virtual void Draw(MyMatrix pParentTransform)
    {

    }
    public abstract void Accept(RenderVisitor pVisitor);
}


