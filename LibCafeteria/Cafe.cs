namespace LibCafeteria
{
    public class Cafe
    {
        private string nombre;
        private float precioPorLitro;

        public Cafe(string nombre, float precioPorLitro)
        {
            this.nombre = nombre;
            this.precioPorLitro = precioPorLitro;
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public float PrecioPorLitro
        {
            get { return precioPorLitro; }
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}