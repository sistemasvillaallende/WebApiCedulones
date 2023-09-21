namespace WSCedulones.Entities
{
    public class CEDULON_INSERT_TASA
    {
        public int cir { get; set; }
        public int sec { get; set; }
        public int man { get; set; }
        public int par { get; set; }
        public int p_h { get; set; }
        public string vencimiento { get; set; }
        public decimal monto_cedulon { get; set; }
        public List<Entities.VCtasctes> Listadeuda { get; set; }
        public int nroProc { get; set; }

        public CEDULON_INSERT_TASA()
        {
            cir = 0;
            sec = 0;
            man = 0;    
            par = 0;
            p_h = 0;
            vencimiento = string.Empty;
            Listadeuda = new List<VCtasctes>();
            nroProc = 0;
        }
    }
}
