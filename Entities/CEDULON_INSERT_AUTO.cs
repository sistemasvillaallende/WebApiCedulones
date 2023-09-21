namespace WSCedulones.Entities
{
    public class CEDULON_INSERT_AUTO
    {
        public string dominio { get; set; }
        public string vencimiento { get; set; }
        public decimal monto_cedulon { get; set; }
        public List<Entities.VCtasctes> Listadeuda { get; set; }
        public int nroProc { get; set; }

        public CEDULON_INSERT_AUTO()
        {
            dominio = string.Empty;
            vencimiento = string.Empty;
            monto_cedulon = 0;
            Listadeuda = new List<Entities.VCtasctes>();
            nroProc = 0;
        }
    }
}
