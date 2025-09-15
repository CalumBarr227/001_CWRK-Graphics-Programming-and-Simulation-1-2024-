using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class RenderVisitor
{
    Stack<MyMatrix> transformStack = new Stack<MyMatrix>();

    public void Visit(TransformNode pNode)
    {
        PushTransform(pNode.LocalTransform);
        Visit(pNode as GroupNode);
        PopTransform();
    }

    public void Visit(GeometryNode pNode)
    {
        transformStack.Peek().SetTransform(pNode.GameObjectInstance);
    }

    public void Visit(GroupNode pNode)
    {
        SceneGraphNode childNode;
        for (int index = 0; index < pNode.GetNumberOfChildren(); index++)
        {
            childNode = pNode.GetChildAt(index);
            childNode.Accept(this);
        }
    }

    public void PushTransform(MyMatrix pParentTransform)
    {

        if (transformStack.Count == 0)
        {
            transformStack.Push(pParentTransform);
        }
        else
        {
            MyMatrix tempMatrix = transformStack.Peek().Multiply(pParentTransform);
            transformStack.Push(tempMatrix);
        }

    }

    public void PopTransform()
    {
        transformStack.Pop();
    }
}
