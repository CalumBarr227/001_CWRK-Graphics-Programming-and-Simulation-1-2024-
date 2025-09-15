using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ExperimentScript : MonoBehaviour
{
    GroupNode rootNode;
    RenderVisitor renderVisitor = new RenderVisitor();
    Bus bus;
    Bus bus2;
    Bus bus3;
    Ball ball;
    List<Ball> _balls = new List<Ball>();
    BackBoard board;
    Capsule capsule;

    // Start is called before the first frame update
    void Start()
    {
        rootNode = new GroupNode("Root Node");


        bus = new Bus(new MyVector(5, 0, 0), new MyVector(0, MathF.PI / 3, 0), new MyVector(1, 1, 1));
        rootNode.AddChild(bus.busNode);
        bus2 = new Bus(new MyVector(7, 0, 0), new MyVector(0, MathF.PI / 2, 0), new MyVector(1, 1, 1));
        rootNode.AddChild(bus2.busNode);
        //bus3 = new Bus(new MyVector(0, 0, 0), new MyVector(0, MathF.PI / 2, 0), new MyVector(1, 1, 1));
        //rootNode.AddChild(bus3.busNode);

        ball = new Ball(new MyVector(-2, 0, 0), 0.2f, Color.red, new MyVector(1, 0, 0));
        _balls.Add(ball);
        rootNode.AddChild(ball.RootNode);

        ball = new Ball(new MyVector(-1f, 2, 0), 0.3f, Color.green, new MyVector(0, 0, 0));
        _balls.Add(ball);
        rootNode.AddChild(ball.RootNode);

        ball = new Ball(new MyVector(2, 1, 0), 0.25f, Color.grey, new MyVector(0, 0, 0));
        _balls.Add(ball);
        rootNode.AddChild(ball.RootNode);

        ball = new Ball(new MyVector(1, 1, 0), 0.15f, Color.cyan, new MyVector(0, 0, 0));
        _balls.Add(ball);
        rootNode.AddChild(ball.RootNode);


        board = new BackBoard(new MyVector(0, 0, 0), new MyVector(5, 5, 1), Color.blue);
        rootNode.AddChild(board.RootNode);
        capsule = new Capsule(new MyVector(0, 0, 0), new MyVector(0, 0, 0.8f), new MyVector(1, 1.5f, 1), Color.green);
        rootNode.AddChild(capsule.RootNode);

    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        rootNode.Accept(renderVisitor);
        foreach (Ball ball in _balls)
        {
            ball.FixedUpdate(_balls, board, capsule);
        }
    }
}
