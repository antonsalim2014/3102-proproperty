using ProProperty.Models;
using System.Collections.Generic;

namespace ProProperty.DAL
{
    interface IPremiseGateway : IDataGateway<Premise>
    {
        IEnumerable<Premise> GetPremises(int town_id, params int[] premise_type_id);
        IEnumerable<Premise> SelectAll(int town_id);
        IEnumerable<Premise> GetAllPremises();
    }
}
