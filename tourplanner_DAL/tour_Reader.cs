using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner.DAL
{
    public class tour_Reader : ObjectReaderWithConnection<Tour>
    {
        protected override string CommandText
        {
            get { return "SELECT tour_ID, tour_Name, tour_Distance, tour_Description FROM Tours"; }
        }
        protected override CommandType CommandType
        {
            get { return CommandType.Text; }
        }

        protected override mapper_Base<Tour> GetMapper()
        {
            return new tour_Mapper();
        }

        protected override Collection<IDataParameter> GetParameters(IDbCommand command)
        {
            Collection<IDataParameter> collection = new Collection<IDataParameter>();
            return collection;
        }
    }
}
