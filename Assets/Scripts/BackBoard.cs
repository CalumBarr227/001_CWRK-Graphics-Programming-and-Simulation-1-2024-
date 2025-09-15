using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBoard
{
    public TransformNode RootNode { get; private set; }
    private TransformNode positionNode { get; set; }
    private TransformNode scaleNode { get; set; }

    public MyVector Position { get; private set; }
    public MyVector Scale { get; set; }
    public Color Colour { get; set; }

    public BackBoard(MyVector pPosition, MyVector pScale, Color pColour)
    {
        Position = pPosition;
        Scale = pScale;
        Colour = pColour;
        initialiseSceneGraph();
    }
    private void initialiseSceneGraph()
    {
        MyVector translationVector = Position;
        MyMatrix translationMatrix = MyMatrix.CreateTranslation(translationVector);
        positionNode = new TransformNode("BackBoardPosition", translationMatrix);

        MyMatrix scaleMatrix = MyMatrix.CreateScale(Scale);
        scaleNode = new TransformNode("BackBoardScale", scaleMatrix);
        positionNode.AddChild(scaleNode);

        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<Renderer>().material.color = Colour;
        GeometryNode backboardGeometry = new GeometryNode("BackBoardGeometry", quad);
        scaleNode.AddChild(backboardGeometry);
        RootNode = positionNode;
    }
}
