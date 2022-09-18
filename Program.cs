using System;

class Server
{
    interface iMovable
    {
        public void setPosition(int[] pos);
        public int[] getPosition();
        public void setVeloсity(int[] vel);
        public int[] getVelocity();

    }

    interface iRotatable
    {
        public void setAngel(double ang);
        public double getAngel();
        public void setPosition(int[] pos);
        public int[] getPosition();
        public void setAngelVelosity(double angvel);
        public double getAngelVelosity();
    }
    class Spaceship : iMovable, iRotatable
    {

        int[] position;
        int[] velocity;
        double angle;
        double angleVelocity;

        public Spaceship(int[] position, int[] velocity, double angle, double angleVelocity)
        {
            this.position = position;
            this.velocity = velocity;

            if (position.Length != velocity.Length)
            {
                throw new ArgumentException("Не совпадают размерности векторов position и velocity");
            }


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
        public void setVeloсity(int[] vel)
        {
            velocity = vel;
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
        public void setAngelVelosity(double angvel)
        {
            angleVelocity = angvel;
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
    class Moving
    {
        public static void Move(iMovable a)
        {
            int[] help = a.getPosition();
            for (int i = 0; i < help.Length; i++)
            {
                help[i] += a.getVelocity()[i];
            }
            a.setPosition(help);
        }
    }
    class Rotating
    {
        public static void Rotate(iRotatable a)
        {
            // double xnew = a.getPosition()[0] * Math.Cos(a.getAngel()) - a.getPosition()[1] * Math.Sin(a.getAngel());
            // double ynew = a.getPosition()[0] * Math.Sin(a.getAngel()) + a.getPosition()[1] * Math.Cos(a.getAngel());
            double newangle = (a.getAngel() + a.getAngelVelosity());
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
    static void Main()
    {
        var spaceship = new Spaceship(
            new int[] { 100, 100 },
            new int[] { 2, 0 },
            0,
            Math.PI / 4
        );

        Moving.Move(spaceship);
        Rotating.Rotate(spaceship);

        Console.WriteLine(spaceship.ToString());
    }
}

