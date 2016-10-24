using ProProperty.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProProperty.DAL
{
    public class PremiseGateway : DataGateway<Premise>, IPremiseGateway
    {
        public IEnumerable<Premise> GetPremises(int town_id, params int[] premise_type_id)
        {
            string typeID = "";
            foreach(int id in premise_type_id)
            {
                typeID += id + ", ";
            }
            typeID = typeID.Remove(typeID.Length - 2);
            string query = string.Format("SELECT * FROM Premises WHERE town_id = {0} and premises_type_id IN ({1})", town_id, typeID);
            return data.SqlQuery(query).ToList();       
        }

        public IEnumerable<Premise> SelectAll(int town_id)
        {
            string query = string.Format("SELECT * FROM Premises where town_id=" + town_id);
            return data.SqlQuery(query).ToList();
        }

        public IEnumerable<Premise> GetAllPremises()
        {
            string query = string.Format("SELECT * FROM Premises");
            return data.SqlQuery(query).ToList();
        }
    }
}