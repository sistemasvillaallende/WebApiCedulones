namespace WSCedulones.Entities
{
    public class VCtasctes
    {
        public int nro_transaccion { get; set; }
        public string periodo { get; set; }
        public decimal importe { get; set; }
        public string fecha_vencimiento { get; set; }
        public decimal deudaOriginal { get; set; }
        public decimal intereses { get; set; }
        public int nro_cedulon_paypertic { get; set; }
        public bool pago_parcial { get; set; }
        public decimal pago_a_cuenta { get; set; }
        public int categoria_deuda { get; set; }

        public VCtasctes()
        {
            nro_transaccion = 0;
            periodo = string.Empty;
            importe = 0;
            fecha_vencimiento = string.Empty;
            deudaOriginal = 0;
            intereses = 0;
            nro_cedulon_paypertic = 0;
            pago_parcial = false;
            pago_a_cuenta = 0;
            categoria_deuda = 0;
        }
    }
}
