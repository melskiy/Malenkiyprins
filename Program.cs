
using System;

class HelloWorld {
    class Spaceship 
    {
        int[] position;
        int[] velocity;
        double angle;
        double angleVelocity;
        
        public Spaceship(int[] position, int[] velocity, double angle, double angleVelocity)
        {
            this.position = position;
            this.velocity = velocity;
            
            if(position.Length != velocity.Length)
            {
                throw new ArgumentException("Не сопадают размерности веторов position и velocity");
            }
            
            
            this.angleVelocity = angleVelocity;
            this.angle = angle;
        }
        
        public void Move()
        {
            for(int i = 0; i < position.Length; ++i)
            {
                position[i] += velocity[i];
            }
        }
        
        public void Rotate()
        {
            angle += angleVelocity;
            
            int oldVelocityX = velocity[0];
            
            velocity[0] = (int)(oldVelocityX*Math.Cos(angleVelocity) - velocity[1]*Math.Sin(angleVelocity));
            velocity[1] = (int)(oldVelocityX*Math.Sin(angleVelocity) + velocity[1]*Math.Cos(angleVelocity));
        }
        
        public override string ToString()
        {
            return String.Format("Vector{{'velocity': ({0}, {1}), 'angle': {2}}}", 
                velocity[0].ToString(), 
                velocity[1].ToString(),
                angle.ToString()
            );
        }
    }
    
  static void Main() {
    var spaceship = new Spaceship(
        new int[] {100, 100},
        new int[] {2, 0},
        0,
        Math.PI/4
    );
    
    spaceship.Rotate();
    spaceship.Rotate();
    spaceship.Rotate();
    spaceship.Rotate();
    spaceship.Rotate();
    spaceship.Rotate();
    spaceship.Rotate();
    spaceship.Rotate();
    
    Console.WriteLine(spaceship.ToString());
  }
}

