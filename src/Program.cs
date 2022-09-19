﻿namespace Game;
using System;

public interface iMovable
{
    public void setPosition(int[] pos);
    public int[] getPosition();
    public int[] getVelocity();

}

public interface iRotatable
{
    public void setAngel(double ang);
    public double getAngel();
    public double getAngelVelosity();
}
public class Spaceship : iMovable, iRotatable
{

    int[] position;
    int[] velocity;
    double angle;
    double angleVelocity;

    public Spaceship(int[] position, int[] velocity, double angle, double angleVelocity)
    {
        this.position = position;
        this.velocity = velocity;
        this.angleVelocity = angleVelocity;
        this.angle = angle;
    }

    public void setPosition(int[] pos)
    {
        position = pos;
    }
    public int[] getPosition()
    {
        return position;
    }

    public int[] getVelocity()
    {
        return velocity;
    }



    public void setAngel(double ang)
    {
        angle = ang;
    }
    public double getAngel()
    {
        return angle;
    }
    public double getAngelVelosity()
    {
        return angleVelocity;
    }

    public override string ToString()
    {
        return String.Format("Vector{{'position': ({0}, {1}),  'angle': {2}}}",
            // velocity[0].ToString(),
            // velocity[1].ToString(),
            position[0].ToString(),
            position[1].ToString(),
            angle.ToString()
        );
    }
}
public class Moving
{
    public static void Move(iMovable a)
    {

        if (a.getPosition().Length != a.getVelocity().Length)
        {
            throw new ArgumentException("Не совпадают размерности векторов position и velocity");
        }
        int[] help = a.getPosition();
        for (int i = 0; i < help.Length; i++)
        {
            help[i] += a.getVelocity()[i];
        }
        a.setPosition(help);
    }
}
public class Rotating
{
    public static void Rotate(iRotatable a)
    {
        // double xnew = a.getPosition()[0] * Math.Cos(a.getAngel()) - a.getPosition()[1] * Math.Sin(a.getAngel());
        // double ynew = a.getPosition()[0] * Math.Sin(a.getAngel()) + a.getPosition()[1] * Math.Cos(a.getAngel());
        double newangle = (a.getAngel() + a.getAngelVelosity()) % 360;
        a.setAngel(newangle);
        // int maxDirections = (int) (2 * Math.PI / (a.getAngel() * 180 / Math.PI)); //максимальное количество направлений;
        // int velocityDirection = 1;//угловая скорость, выраженная в направлении
        // double rotate;
        // int direction = 0;
        // rotate = (direction + velocityDirection) % maxDirections;
        // int oldVelocityX = velocity[0];
        // double alpha = Math.PI / maxDirections * direction;
        // velocity[0] = Math.Round(oldVelocityX * Math.Cos(alpha) - velocity[1] * Math.Sin(alpha));
        // velocity[1] = Math.Round(oldVelocityX * Math.Sin(alpha) + velocity[1] * Math.Cos(alpha));
    }
}