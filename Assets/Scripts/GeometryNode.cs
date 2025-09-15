using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryNode : SceneGraphNode
{
    public GameObject GameObjectInstance { get; set; }

    public GeometryNode(string pName, GameObject pGameObject) : base(pName)
    {
        this.GameObjectInstance = pGameObject;
    }

    public override void Accept(RenderVisitor pVisitor)
    {
        pVisitor.Visit(this);
    }
}
