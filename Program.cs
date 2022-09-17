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
        public void setAngel();
        public void setAngelVelosity();
        public void Rotate();
    }
    class Spaceship : iMovable
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



        public void Rotate()
        {
            angle += angleVelocity;
            int oldVelocityX = velocity[0];

            velocity[0] = (int)(oldVelocityX * Math.Cos(angleVelocity) - velocity[1] * Math.Sin(angleVelocity));
            velocity[1] = (int)(oldVelocityX * Math.Sin(angleVelocity) + velocity[1] * Math.Cos(angleVelocity));
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

