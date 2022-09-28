namespace Game;
using System;

public interface iMovable
{
    public void setPosition(int[] pos);
    public int[] getPosition();
    public int[] getVelocity();

}

public interface iRotatable
{
    public void setAngle(double ang);
    public double getAngle();
    public double getAngleVelosity();
    public int[] getVelocity();
    public void setVelosity(int[] vel);
    public int[] getPosition();
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
    public void setVelosity(int[] vel)
    {
        velocity = vel;
    }
    public void setAngle(double ang)
    {
        angle = ang;
    }
    public double getAngle()
    {
        return angle;
    }
    public double getAngleVelosity()
    {
        return angleVelocity;
    }

    public override string ToString()
    {
        return String.Format("Vector{{'velocity': ({0}, {1}), 'position': ({2}, {3}),  'angle': {4}}}",
            velocity[0].ToString(),
            velocity[1].ToString(),
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
        double newangle = (a.getAngle() + a.getAngleVelosity()) % 360;
        a.setAngle(newangle);
        double angleRadian = a.getAngleVelosity() * Math.PI / 180;
        

        int xnew = (int)(a.getPosition()[0] * Math.Cos(angleRadian) - a.getPosition()[1] * Math.Sin(angleRadian));
        int ynew = (int)(a.getPosition()[0] * Math.Sin(angleRadian) + a.getPosition()[1] * Math.Cos(angleRadian));
        int[] v = {xnew, ynew};
        a.setVelosity(v);

        // int maxDirections = (int) (2 * Math.PI / (a.getAngel() * 180 / Math.PI)); //максимальное количество направлений;
        // int velocityDirection = 1;//угловая скорость, выраженная в направлении
        // double rotate;
        // int direction = 0;
        // rotate = (direction + velocityDirection) % maxDirections;
        // double alpha = Math.PI / maxDirections * direction;

        // int xnew = (int)(a.getVelocity()[0] * Math.Cos(a.getAngle()) - a.getVelocity()[1] * Math.Sin(a.getAngle()));
        // int ynew = (int)(a.getVelocity()[0] * Math.Sin(a.getAngle()) + a.getVelocity()[1] * Math.Cos(a.getAngle()));
        // int[] v = { xnew, ynew };
        // a.setVelosity(v);
       
       
    }
}