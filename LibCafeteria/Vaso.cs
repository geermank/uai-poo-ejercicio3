namespace LibCafeteria
{
    public class Vaso
    {
        private string nombre;
        private float medidaEnCm3;

        public Vaso(string nombre, float medidaEnCm3)
        {
            this.nombre = nombre;
            this.medidaEnCm3 = medidaEnCm3;
        }
        
        public string Nombre
        {
            get { return nombre; }
        }

        public float Medida
        {
            get { return medidaEnCm3; }
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}