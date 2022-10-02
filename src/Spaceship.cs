// public class Spaceship : IMovable, IRotatable
// {
//     Dictionary<string, object> SetOfProperties = new Dictionary<string, object>();
//     Vector position;
//     Vector velocity;
//     double angle;
//     double angleVelocity;

//     public Spaceship(Vector position, Vector velocity, double angle, double angleVelocity)
//     {
//         this.position = position;
//         this.velocity = velocity;
//         this.angleVelocity = angleVelocity;
//         this.angle = angle;
//     }

//     public void setPosition(Vector pos)
//     {
//         position = pos;
//     }
//     public Vector getPosition()
//     {
//         return position;
//     }

//     public Vector getVelocity()
//     {
//         return velocity;
//     }
//     public void setVelosity(Vector vel)
//     {
//         velocity = vel;
//     }
//     public void setAngle(double ang)
//     {
//         angle = ang;
//     }
//     public double getAngle()
//     {
//         return angle;
//     }
//     public double getAngleVelosity()
//     {
//         return angleVelocity;
//     }

//     public override string ToString()
//     {
//         return String.Format("Vector{{'velocity': ({0}, {1}), 'position': ({2}, {3}),  'angle': {4}}}",
//             velocity[0].ToString(),
//             velocity[1].ToString(),
//             position[0].ToString(),
//             position[1].ToString(),
//             angle.ToString()
//         );
//     }
// }