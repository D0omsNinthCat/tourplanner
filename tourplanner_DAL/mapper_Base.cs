using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace tourplanner_DAL
{
    abstract class mapper_Base<T>
    {
        protected abstract T Map(IDataRecord record);
        public Collection<T> MapAll(IDataReader reader)
        {
            Collection<T> collection = new Collection<T>();
            while (reader.Read())
            {
                try
                {
                    collection.Add(Map(reader));
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
            return collection;
        }
    }

}
