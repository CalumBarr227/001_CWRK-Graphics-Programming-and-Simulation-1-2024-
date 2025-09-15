using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball 
{
    public TransformNode RootNode { get; private set; }
    private TransformNode positionNode { get; set; }
    private TransformNode scaleNode { get; set; }

    private MyVector Position { get; set; }
    public float Radius { get; private set; }
    public Color Colour { get; set; }
    public MyVector Velocity { get; set; }
    public MyVector OldPosition { get; set; }
    private float density = 1.0f;
    public float Volume { get; set; }
    public float Mass => density * (4 / 3) * Mathf.PI * Mathf.Pow(Radius, 3);

    public Ball(MyVector pPosition, float pRadius, Color pColour, MyVector pVelocity)
    {
        Position = pPosition;
        Radius = pRadius;
        Colour = pColour;
        Velocity = pVelocity;
        this.density = density;
        InitialiseSceneGraph();
    }

    public void InitialiseSceneGraph()
    {
        MyMatrix positionTranslation = MyMatrix.CreateTranslation(Position);
        positionNode = new TransformNode("PositionTranslation", positionTranslation);

        MyMatrix scaleVector = MyMatrix.CreateScale(new MyVector(2 * Radius, 2 * Radius, 2 * Radius));
        scaleNode = new TransformNode("Scale", scaleVector);
        positionNode.AddChild(scaleNode);

        GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.GetComponent<Renderer>().material.color = Colour;
        GeometryNode ballGeometry = new GeometryNode("BallGeometry", ball);
        scaleNode.AddChild(ballGeometry);

        RootNode = positionNode;
    }

    public void FixedUpdate(List<Ball> pBalls, BackBoard pBackBoard, Capsule pCapsule)
    {
        Position = Position.Add(Velocity.Multiply(Time.fixedDeltaTime));
        RootNode.LocalTransform = MyMatrix.CreateTranslation(Position);

        OldPosition = Position;
        Position = Position.Add(Velocity.Multiply(Time.fixedDeltaTime));
        //RootNode.LocalTransform = MyMatrix.CreateTranslation(Position);

        if ((Position.X + Radius) >= pBackBoard.Scale.X / 2 || (Position.X - Radius) <= -pBackBoard.Scale.X / 2)
        {
            Position = OldPosition;
            Velocity = new MyVector(-Velocity.X, Velocity.Y, Velocity.Z);
        }
        if ((Position.Y + Radius) >= pBackBoard.Scale.Y / 2 || (Position.Y - Radius) <= -pBackBoard.Scale.Y / 2)
        {
            Position = OldPosition;
            Velocity = new MyVector(Velocity.X, -Velocity.Y, Velocity.Z);
        }
        //if ((Position.Z + Radius) >= pBackBoard.Scale.Z / 2 || (Position.Z - Radius) <= -pBackBoard.Scale.Z / 2)
        //{
        //    Position = OldPosition;
        //    Velocity = new MyVector(Velocity.X, Velocity.Y, -Velocity.Z);
        //}

        foreach (Ball ball in pBalls)
        {
            if (ball != this)
            {
                MyVector ballToBall = Position.Subtract(ball.Position);
                if (ballToBall.Magnitude() < Radius + ball.Radius)
                {
                    Position = OldPosition;
                    CollisionResponse(ball);
                }
            }
        }

        Velocity = Velocity.Add(new MyVector(0, -9.81f, 0).Multiply(Time.fixedDeltaTime));
        MyVector capsuleStart = pCapsule.StartPosition;
        MyVector capsuleEnd = pCapsule.EndPosition;
       
        MyVector StartToBall = Position.Subtract(capsuleStart);
        MyVector NormalisedLineDirection = capsuleEnd.Subtract(capsuleStart).Normalise();
        float dot = StartToBall.DotProduct(NormalisedLineDirection);
        MyVector ClosestPointOnLine = capsuleStart.Add(NormalisedLineDirection.Multiply(dot));
        float closestDistance = (Position.Subtract(ClosestPointOnLine)).Magnitude();
        if (closestDistance < 0.5f * pCapsule.Scale.X + Radius)
        {
            Position = OldPosition;
            Velocity = BounceBallAgainstFixedObject(Position.Subtract(ClosestPointOnLine).Normalise());
        }
    }
    private MyVector BounceBallAgainstFixedObject(MyVector pNormal)
    {
        MyVector inverseNormal = pNormal.Multiply(-1f);
        float dot = inverseNormal.DotProduct(Velocity);
        return Velocity.Add(pNormal.Multiply(2 * dot));
    }

    private void CollisionResponse(Ball pBall)
    {
        MyVector thisNormalisedCollisionDirection = Position.Subtract(pBall.Position).Normalise();
        MyVector thisParallelComponent = thisNormalisedCollisionDirection.Multiply(Velocity.DotProduct(thisNormalisedCollisionDirection));
        MyVector thisPerpendicularComponent = Velocity.Subtract(thisParallelComponent);

        MyVector pBallNormalisedCollisionDirection = pBall.Position.Subtract(Position).Normalise();
        MyVector pBallParallelComponent = pBallNormalisedCollisionDirection.Multiply(pBall.Velocity.DotProduct(pBallNormalisedCollisionDirection));
        MyVector pBallPerpendicularComponent = pBall.Velocity.Subtract(pBallParallelComponent);

        MyVector newParallelComponent = thisParallelComponent.Multiply((Mass - pBall.Mass) / (Mass + pBall.Mass));
        newParallelComponent = newParallelComponent.Add(pBallParallelComponent.Multiply((2 * pBall.Mass) / (Mass + pBall.Mass)));

        Velocity = thisPerpendicularComponent.Add(newParallelComponent);

        MyVector pBallNewParallelComponent = pBallParallelComponent.Multiply((pBall.Mass - Mass) / (pBall.Mass + Mass));
        pBallNewParallelComponent = pBallNewParallelComponent.Add(thisParallelComponent.Multiply((2 * Mass) / (pBall.Mass + Mass)));

        pBall.Velocity = pBallPerpendicularComponent.Add(pBallNewParallelComponent);
    }   
}
