using System;
using System.Data;
using tourplanner.Models;

namespace tourplanner.Mappers
{
    public class mapper_Tour : mapper_Base<Tour>
    {
        protected override Tour Map(IDataReader reader)
        {
            try
            {
                Tour t = new Tour();
                t.tour_ID = (DBNull.Value == reader["tour_ID"]) ? 0 : (int)reader["tour_ID"];
                t.tour_Name = (DBNull.Value == reader["tour_Name"]) ? string.Empty : (string)reader["tour_Name"];
                t.tour_Description = (DBNull.Value == reader["tour_Description"]) ? string.Empty : (string)reader["tour_Description"];
                t.tour_Distance = (DBNull.Value == reader["tour_Distance"]) ? 0 : (float)reader["tour_Distance"];
                return t;
            }
            catch (Exception)
            {
                throw;
                // NOTE:
                // Source: https://www.c-sharpcorner.com/article/an-elegant-C-Sharp-data-access-layer-using-the-template-pattern-a/
                // consider handling exeption here instead of re-throwing  
                // if graceful recovery can be accomplished 
            }
        }
    }
}