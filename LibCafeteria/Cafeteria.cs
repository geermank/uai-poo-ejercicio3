using System;
using System.Collections.Generic;
using System.Linq;

namespace LibCafeteria
{
    public class Cafeteria
    {
        public event delRecargarMaquinaCafe DelRecargarMaquinaCafe;
        public event delCafeVendido DelCafeVendido;
        public event delMaquinaDeCafeCreada DelMaquinaCreada;

        private List<MaquinaDeCafe> maquinas;

        private List<Cafe> cafes;
        private List<Vaso> vasos;

        private List<Venta> ventas;

        public Cafeteria()
        {
            maquinas = new List<MaquinaDeCafe>();
            vasos = new CreadorDeVasos().CrearVasos();
            cafes = new CreadorDeCafe().CrearCafe();
            ventas = new List<Venta>();
        }

        public List<Cafe> CafeDisponible 
        { 
            get { return cafes; }
        }

        public List<Vaso> VasosDisponible
        {
            get { return vasos; }
        }

        public List<MaquinaDeCafe> MaquinasDeCafe
        {
            get { return maquinas; }
        }

        public void CrearMaquinaCafe(Cafe cafe, float cantMaxima, float cantInicial)
        {
            MaquinaDeCafe maquinaDeCafe = new MaquinaDeCafe(cafe, cantMaxima, cantInicial);
            maquinas.Add(maquinaDeCafe);
            DelMaquinaCreada(maquinaDeCafe);
        }

        public void RecargarMaquina(MaquinaDeCafe maquinaDeCafe)
        {
            maquinaDeCafe.Recargar();
        }

        public void VenderCafe(MaquinaDeCafe maquina, Vaso vaso)
        {
            bool resultado = maquina.ServirCafe(vaso);
            if (resultado)
            {
                float importeDeVenta = CalcularImporteDeVenta(vaso, maquina.Cafe);
                Venta venta = new Venta(maquina, maquina.Cafe, vaso, importeDeVenta);
                ventas.Add(venta);
                DelCafeVendido(venta);
            }
            else
            {
                DelRecargarMaquinaCafe(maquina);
            }
        }

        private float CalcularImporteDeVenta(Vaso vaso, Cafe cafe)
        {
            return (vaso.Medida * cafe.PrecioPorLitro) / 1000;
        }

        public float ObtenerRecaudacionTotal()
        {
            return ventas.Sum(v => v.Importe);
        }

        public IDictionary<MaquinaDeCafe, float> ObtenerRecaudacionPorMaquina()
        {
            IDictionary<MaquinaDeCafe, float> recaudacionPorMaquina = new Dictionary<MaquinaDeCafe, float>();
            foreach(MaquinaDeCafe m in maquinas)
            {
                var recaudacion = (from v in ventas
                                   where v.MaquinaDeCafe.Equals(m)
                                   select v.Importe).ToList().Sum();
                recaudacionPorMaquina.Add(m, recaudacion);
            }
            return recaudacionPorMaquina;
        }

        public MaquinaDeCafe ObtenerLaMaquinaQueMasSirvio()
        {
            MaquinaDeCafe maquinaDeCafe = null;
            float cafeServidoMax = 0f;

            foreach (MaquinaDeCafe m in maquinas)
            {
                var cafeServido = (from v in ventas
                                   where v.MaquinaDeCafe.Equals(m)
                                   select v.Vaso.Medida).ToList().Sum();
                if (cafeServido >= cafeServidoMax)
                {
                    cafeServidoMax = cafeServido;
                    maquinaDeCafe = m;
                }
            }

            return maquinaDeCafe;
        }

        public MaquinaDeCafe ObtenerLaMaquinaQueMenosSirvio()
        {
            MaquinaDeCafe maquinaDeCafe = null;
            float cafeServidoMin = 0f;

            foreach (MaquinaDeCafe m in maquinas)
            {
                var cafeServido = (from v in ventas
                                   where v.MaquinaDeCafe.Equals(m)
                                   select v.Vaso.Medida).ToList().Sum();
                if (maquinaDeCafe == null || cafeServido <= cafeServidoMin)
                {
                    cafeServidoMin = cafeServido;
                    maquinaDeCafe = m;
                }
            }

            return maquinaDeCafe;
        }

        public MaquinaDeCafe ObtenerMaquinaMasRecaudadora()
        {
            MaquinaDeCafe maxMaquina = null;
            float recaudacionMax = 0f;
            foreach(KeyValuePair<MaquinaDeCafe, float> par in ObtenerRecaudacionPorMaquina())
            {
                if (maxMaquina == null || par.Value > recaudacionMax) 
                {
                    maxMaquina = par.Key;
                    recaudacionMax = par.Value;
                }
            }
            return maxMaquina;
        }

        public MaquinaDeCafe ObtenerMaquinaMenosRecaudadora()
        {
            MaquinaDeCafe minMaquina = null;
            float recaudacionMin = 0f;
            foreach (KeyValuePair<MaquinaDeCafe, float> par in ObtenerRecaudacionPorMaquina())
            {
                if (minMaquina == null || par.Value < recaudacionMin)
                {
                    minMaquina = par.Key;
                    recaudacionMin = par.Value;
                }
            }
            return minMaquina;
        }

        public Cafe ObtenerElCafeQueMasSeSirvio()
        {
            Cafe cafeMasServido = null;
            int vecesServidoMax = 0;
            foreach(Cafe c in cafes)
            {
                var vecesServido = (from v in ventas
                                    where v.Cafe == c
                                    select v).Count();
                if (cafeMasServido == null || vecesServidoMax < vecesServido)
                {
                    cafeMasServido = c;
                    vecesServidoMax = vecesServido;
                }
            }

            return cafeMasServido;
        }

        public Cafe ObtenerElCafeQueMenosSeSirvio()
        {
            Cafe cafeMenosServido = null;
            int vecesServidoMin = 0;
            foreach (Cafe c in cafes)
            {
                var vecesServido = (from v in ventas
                                    where v.Cafe == c
                                    select v).Count();
                if (cafeMenosServido == null || vecesServidoMin >= vecesServido)
                {
                    cafeMenosServido = c;
                    vecesServidoMin = vecesServido;
                }
            }

            return cafeMenosServido;
        }

        public Cafe ObtenerElCafeConMayorRecaudacion()
        {
            Cafe cafe = null;
            float recMax = 0f;

            foreach (Cafe c in cafes)
            {
                var recaudacion = (from v in ventas
                                    where v.Cafe == c
                                    select v.Importe).Sum();
                if (cafe == null || recMax <= recaudacion)
                {
                    cafe = c;
                    recMax = recaudacion;
                }
            }

            return cafe;
        }

        public Cafe ObtenerElCafeConMenorRecaudacion()
        {
            Cafe cafe = null;
            float recMin = 0f;

            foreach (Cafe c in cafes)
            {
                var recaudacion = (from v in ventas
                                   where v.Cafe == c
                                   select v.Importe).Sum();
                if (cafe == null || recMin >= recaudacion)
                {
                    cafe = c;
                    recMin = recaudacion;
                }
            }

            return cafe;
        }

        public MaquinaDeCafe ObtenerMaquinaDeCafeMasRecargada()
        {
            MaquinaDeCafe maquina = null;
            int vecesRecargada = 0;

            foreach (MaquinaDeCafe maquinaDeCafe in maquinas)
            {
                if (maquina == null || maquinaDeCafe.Recargas >= vecesRecargada)
                {
                    maquina = maquinaDeCafe;
                    vecesRecargada = maquina.Recargas;
                }
            }

            return maquina;
        }
    }
}