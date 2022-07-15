namespace LibCafeteria
{
    public class Venta
    {
        private MaquinaDeCafe maquina;
        private Cafe cafe;
        private Vaso vaso;
        private float importe;

        public Venta(MaquinaDeCafe maquina, Cafe cafe, Vaso vaso, float importe)
        {
            this.maquina = maquina;
            this.cafe = cafe;
            this.vaso = vaso;
            this.importe = importe;
        }
        
        public MaquinaDeCafe MaquinaDeCafe
        {
            get { return maquina; }
        }

        public Cafe Cafe
        {
            get { return cafe; }
        }

        public Vaso Vaso
        {
            get { return vaso; }
        }

        public float Importe
        {
            get { return importe; }
        }
    }
}