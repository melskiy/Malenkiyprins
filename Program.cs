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
            return String.Format("Vector{{'velocity': ({0}, {1}),  'angle': {2}}}",
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
            int maxDirection = (int)(2 * Math.PI / a.getAngel()); //максимальное количество направлений;
            // int velocityDirection = угловая скорость, выраженная в направлении
            // direction = a.getAngel() + velocityDirection % maxDirection;
            //int oldVelocityX = velocity[0];
            // double alpha = Math.PI / maxDirection * direction 
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

        Console.WriteLine(spaceship.ToString());
    }
}

