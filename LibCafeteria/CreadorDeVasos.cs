using System.Collections.Generic;

namespace LibCafeteria
{
    public class CreadorDeVasos
    {
        public List<Vaso> CrearVasos()
        {
            return new List<Vaso>
            {
                new Vaso("Chico", 50),
                new Vaso("Mediano", 100),
                new Vaso("Grande", 250)
            };
        }
    }
}