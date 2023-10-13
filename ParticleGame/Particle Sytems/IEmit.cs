using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleGame.Particle_Sytems
{
    public interface IEmit
    {
        void Emit(Point where);
    }
}
