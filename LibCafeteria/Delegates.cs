using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCafeteria
{
    public delegate void delRecargarMaquinaCafe(MaquinaDeCafe maquinaDeCafe);
    public delegate void delCafeVendido(Venta venta);
    public delegate void delMaquinaDeCafeCreada(MaquinaDeCafe maquinaDeCafe);
}