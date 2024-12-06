namespace WSCedulones.Entities
{
    public class CEDULON_INSERT_CREDITO
    {
        public int legajo { get; set; }
        public string vencimiento { get; set; }
        public decimal monto_cedulon { get; set; }
        public List<Entities.VCtasctes> Listadeuda { get; set; }
        public int nroProc { get; set; }

        public CEDULON_INSERT_CREDITO()
        {
            legajo = 0;
            vencimiento = string.Empty;
            monto_cedulon = 0;
            Listadeuda = new List<VCtasctes>();
            nroProc = 0;
        }

    }
}
