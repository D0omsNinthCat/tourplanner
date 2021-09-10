using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner_DAL
{
    class tourmapper: mapper_Base<Tour>
    {
        protected override Tour Map(IDataRecord record)
        {
            try
            {
                Tour t = new Tour();
                t.tour_ID = (DBNull.Value == record["tour_ID"]) ? 0 : (int)record["tour_ID"];
                t.tour_Name = (DBNull.Value == record["tour_Name"]) ? string.Empty : (string)record["tour_Name"];
                t.tour_Description = (DBNull.Value == record["tour_Description"]) ? string.Empty : (string)record["tour_Description"];
                t.tour_Distance = (DBNull.Value == record["tour_Distance"]) ? 0 : (float)record["tour_Distance"];
                return t;
            }
            catch
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
