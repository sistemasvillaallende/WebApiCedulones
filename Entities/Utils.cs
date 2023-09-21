using System.Text;

namespace WSCedulones.Entities
{
    public class Utils
    {
        public static string armoDenominacion(int cir, int sec, int man, int par, int p_h)
        {
            try
            {
                StringBuilder denominacion = new StringBuilder();

                if (cir < 10)
                    denominacion.AppendFormat("CIR: 0{0} - ", cir);
                if (cir > 9 && cir < 100)
                    denominacion.AppendFormat("CIR: {0} - ", cir);

                if (sec < 10)
                    denominacion.AppendFormat("SEC: 0{0} - ", sec);
                if (sec > 9 && sec < 100)
                    denominacion.AppendFormat("SEC: {0} - ", sec);

                if (man < 10)
                    denominacion.AppendFormat("MAN: 00{0} - ", man);
                if (man > 9 && man < 100)
                    denominacion.AppendFormat("MAN: 0{0} - ", man);
                if (man > 99)
                    denominacion.AppendFormat("MAN: {0} - ", man);

                if (par < 10)
                    denominacion.AppendFormat("PAR: 00{0} - ", par);
                if (par > 9 && par < 100)
                    denominacion.AppendFormat("PAR: 0{0} - ", par);
                if (par > 99)
                    denominacion.AppendFormat("PAR: {0} - ", par);

                if (p_h < 10)
                    denominacion.AppendFormat("P_H: 00{0}", p_h);
                if (p_h > 9 && p_h < 100)
                    denominacion.AppendFormat("P_H: 0{0}", p_h);
                if (p_h > 99)
                    denominacion.AppendFormat("P_H: {0}", p_h);

                return denominacion.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
