using ProProperty.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ProProperty.DAL
{
    public class PropertyGateway : DataGateway<Property>, IPropertyGateway
    {
        public List<string> GetPropertyTypes()
        {
            List<string> types = new List<string>();

            List<Property> property = data.SqlQuery("SELECT * FROM ict2106_t12_testdb.property LIMIT 1;").ToList();

            foreach(Property p in property)
            {
                if(!types.Contains(p.propertyType.ToUpper()))
                {
                    types.Add(p.propertyType.ToUpper());
                }
            }

            return types;
        }

        public List<Property> GetProperties(int townId, int minPrice, int maxPrice, int minBuiltSize, int maxBuiltSize, string propertyType)
        {
            List<Property> properties = new List<Property>();

            //properties = SelectAll().Where(
            //    property => property.HDBTown == townId &&
            //    (property.valuation >= minPrice && property.valuation <= maxPrice) &&
            //    (property.built_size_in_sqft >= minBuiltSize &&
            //    property.built_size_in_sqft <= maxBuiltSize) &&
            //    property.propertyType == propertyType && property.available == false).ToList();

            string query = "SELECT * FROM property WHERE available = {0} AND propertyType = '{1}' AND hdbtown = {2} AND " +
                "(valuation >= {3} AND valuation <= {4}) AND (built_size_in_sqft >= {5} AND built_size_in_sqft <= {6})";
            string modifiedQuery = string.Format(query, 1, propertyType, townId, minPrice, maxPrice, minBuiltSize, maxBuiltSize);

            try
            {
                properties = data.SqlQuery(modifiedQuery).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return properties;
        }
    }
}