namespace LibCafeteria
{
    public class MaquinaDeCafe
    {
        private Cafe cafe;

        private readonly float cantidadMaximaEnCm;
        private float cantidadDisponibleEnCm;

        private int vecesRecargada = 0;

        public MaquinaDeCafe(Cafe cafe, float cantidadMaximaEnCm, float cantidadDisponibleEnCm)
        {
            this.cafe = cafe;
            this.cantidadMaximaEnCm = cantidadMaximaEnCm;
            this.cantidadDisponibleEnCm = cantidadDisponibleEnCm;
        }

        public Cafe Cafe
        {
            get { return cafe; }
            set { cafe = value; }
        }

        public float CantidadDisponible 
        { 
            get { return cantidadDisponibleEnCm; }
        }

        public float CantidadMaxima 
        {
            get { return cantidadMaximaEnCm; }
        }

        public int Recargas
        {
            get { return vecesRecargada; }
        }

        public void Recargar()
        {
            this.cantidadDisponibleEnCm = cantidadMaximaEnCm;
            this.vecesRecargada++;
        }

        public bool ServirCafe(Vaso vaso)
        {
            if (cantidadDisponibleEnCm < vaso.Medida)
            {
                return false;
            }
            cantidadDisponibleEnCm -= vaso.Medida;
            return true;
        }

        public override string ToString()
        {
            return "Maquina con " + cafe.ToString();
        }
    }
}