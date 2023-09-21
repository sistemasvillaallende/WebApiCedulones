namespace WSCedulones.Entities
{
    public class VCtasctes
    {
        private Int32 _nro_transaccion = -1;
        private string _periodo = string.Empty;
        private decimal _importe = 0;
        private string _fecha_vencimiento = "";
        private Int32 _categoria_deuda = 0;

        public decimal deudaOriginal { get; set; }
        public decimal intereses { get; set; }
        public int nro_cedulon_paypertic { get; set; }
        public bool pago_parcial { get; set; }
        public decimal pago_a_cuenta { get; set; }

        public Int32 nro_transaccion
        {
            get { return _nro_transaccion; }
            set { _nro_transaccion = value; }
        }

        public string periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }

        public decimal importe
        {
            get { return _importe; }
            set { _importe = value; }
        }

        public string fecha_vencimiento
        {
            get { return _fecha_vencimiento; }
            set { _fecha_vencimiento = value; }
        }

        public Int32 categoria_deuda
        {
            get { return _categoria_deuda; }
            set { _categoria_deuda = value; }
        }

        public VCtasctes()
        {
            this.Clear();
        }

        public void Clear()
        {
            _nro_transaccion = -1;
            _periodo = "";
            _importe = 0;
            _fecha_vencimiento = "";
            _categoria_deuda = 0;
            deudaOriginal = 0;
            intereses = 0;
            nro_cedulon_paypertic = 0;

        }

        public VCtasctes(Int32 nro_transaccion, string periodo, decimal importe, string fecha_vencimiento, Int32 categoria_deuda)
        {
            this.nro_transaccion = nro_transaccion;
            this.periodo = periodo;
            this.importe = importe;
            this.fecha_vencimiento = fecha_vencimiento;
            this.categoria_deuda = categoria_deuda;
        }
    }
}
