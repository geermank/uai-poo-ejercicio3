using LibCafeteria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio3
{
    public partial class Form1 : Form
    {
        private Cafeteria cafeteria;

        public Form1()
        {
            InitializeComponent();
            cafeteria = new Cafeteria();
            cafeteria.DelCafeVendido += Cafeteria_DelCafeVendido;
            cafeteria.DelRecargarMaquinaCafe += Cafeteria_DelRecargarMaquinaCafe;
            cafeteria.DelMaquinaCreada += Cafeteria_DelMaquinaCreada;
        }

        private void Cafeteria_DelMaquinaCreada(MaquinaDeCafe maquinaDeCafe)
        {
            listBox1.Items.Add(maquinaDeCafe);
        }

        private void Cafeteria_DelRecargarMaquinaCafe(MaquinaDeCafe maquinaDeCafe)
        {
            MessageBox.Show("La maquina no tiene cafe. Se realizara la recarga automatica");
            maquinaDeCafe.Recargar();
        }

        private void Cafeteria_DelCafeVendido(Venta venta)
        {
            MessageBox.Show("Cafe vendido! Cobrado: " + venta.Importe);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(MaquinaDeCafe maquinaDeCafe in cafeteria.MaquinasDeCafe)
            {
                listBox1.Items.Add(maquinaDeCafe);
            }
            foreach(Vaso vaso in cafeteria.VasosDisponible)
            {
                listBox2.Items.Add(vaso);
            }
            foreach(Cafe cafe in cafeteria.CafeDisponible)
            {
                listBox3.Items.Add(cafe);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MaquinaDeCafe maquinaDeCafe = listBox1.SelectedItem as MaquinaDeCafe;
            Vaso vaso = listBox2.SelectedItem as Vaso;
            if (maquinaDeCafe == null)
            {
                MessageBox.Show("Elegi una maquina de cafe");
                return;
            }
            if (vaso == null)
            {
                MessageBox.Show("Elegi un vaso");
                return;
            }
            cafeteria.VenderCafe(maquinaDeCafe, vaso);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Recaudacion total: " + cafeteria.ObtenerRecaudacionTotal());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "";

            IDictionary<MaquinaDeCafe, float> recPorMaquina = cafeteria.ObtenerRecaudacionPorMaquina();
            foreach(KeyValuePair<MaquinaDeCafe, float> maquinaDeCafe in recPorMaquina)
            {
                message += maquinaDeCafe.Key.ToString() + " " + maquinaDeCafe.Value.ToString() + "\n";
            }

            MessageBox.Show(message);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cafe cafe = listBox3.SelectedItem as Cafe;
            if (cafe == null)
            {
                MessageBox.Show("Elegi el cafe");
                return;
            }
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("Ingresa las capacidades");
                return;
            }

            float capacidadMax = float.Parse(textBox1.Text);
            float cantidadInicial = float.Parse(textBox2.Text);

            cafeteria.CrearMaquinaCafe(cafe, capacidadMax, cantidadInicial);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Maquina que mas sirvio: " + cafeteria.ObtenerLaMaquinaQueMasSirvio().ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Maquina que menos sirvio: " + cafeteria.ObtenerLaMaquinaQueMenosSirvio().ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Maquina que mas recaudó: " + cafeteria.ObtenerMaquinaMasRecaudadora().ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Maquina que menos recaudó: " + cafeteria.ObtenerMaquinaMenosRecaudadora().ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cafe mas servido: " + cafeteria.ObtenerElCafeQueMasSeSirvio().ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cafe menos servido: " + cafeteria.ObtenerElCafeQueMenosSeSirvio().ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cafe mayor recaudacion: " + cafeteria.ObtenerElCafeConMayorRecaudacion().ToString());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cafe menor recaudacion: " + cafeteria.ObtenerElCafeConMenorRecaudacion().ToString());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Maquina mas recargada: " + cafeteria.ObtenerMaquinaDeCafeMasRecargada().ToString());
        }
    }
}
