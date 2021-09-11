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
    class tour_Reader : ObjectReaderWithConnection<Tour>
    {
        public override string CommandText
        {
            get { return "SELECT tour_ID, tour_Name, tour_Distance, tour_Description FROM Tours"; }
        }

        protected override mapper_Base<Tour> GetMapper()
        {
            mapper_Base<Tour> mapper = new tour_Mapper();
            return mapper;
        }

        protected override Collection<IDataParameter> GetParameters(IDbCommand command)
        {
            Collection<IDataParameter> collection = new Collection<IDataParameter>();
            return collection;
        }
    }
}
