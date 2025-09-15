using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformNode : GroupNode
{
    public MyMatrix LocalTransform { get; set; }

    public TransformNode(string pName, MyMatrix pLocalTransform) : base(pName)
    {
        LocalTransform = pLocalTransform;
    }

    public override void Accept(RenderVisitor pVisitor)
    {
        pVisitor.Visit(this);
    }
}
