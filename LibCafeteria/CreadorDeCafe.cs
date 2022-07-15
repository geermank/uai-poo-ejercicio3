using System.Collections.Generic;

namespace LibCafeteria
{
    public class CreadorDeCafe
    {
        public List<Cafe> CrearCafe()
        {
            return new List<Cafe> {
                new Cafe("Arabigo", 50f),
                new Cafe("Robusto", 58f)
            };
        }
    }
}