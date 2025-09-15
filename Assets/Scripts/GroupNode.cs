using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroupNode : SceneGraphNode
{
    List<SceneGraphNode> _children = new List<SceneGraphNode>();

    public GroupNode(string pName) : base(pName)
    {
        _children = new List<SceneGraphNode>();
    }
    public void AddChild(SceneGraphNode pChildNode)
    {
        _children.Add(pChildNode);
    }
    public int GetNumberOfChildren()
    {
        int numOfChildren = 0;
        for (int i = 0; i < _children.Count; i++)
        {
            if (_children[i] != null)
            {
                numOfChildren++;
            }
        }

        return numOfChildren;
    }

    public SceneGraphNode GetChildAt(int pIndex)
    {
        return _children[pIndex];
    }

    public override void Accept(RenderVisitor pVisitor)
    {
        pVisitor.Visit(this);
    }
}
