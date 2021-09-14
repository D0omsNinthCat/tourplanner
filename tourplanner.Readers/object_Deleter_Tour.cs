using System.Collections.ObjectModel;
using System.Data;
using tourplanner.Models;
using tourplanner.Mappers;

namespace tourplanner.Readers
{
    public class object_Deleter_Tour : object_Reader_With_Connection<Tour>
    {
        protected override string CommandText
        {
            get { return "DELETE FROM tours WHERE tour_ID = 1"; }
        }

        protected override CommandType CommandType
        {
            get { return CommandType.Text;}
        }
        protected override mapper_Base<Tour> GetMapper()
        {
            return new mapper_Tour();
        }
        protected override Collection<IDataParameter> GetParameters(IDbCommand command)
        {
            Collection<IDataParameter> collection = new Collection<IDataParameter>();
            return collection;
        }
        protected override Collection<Tour> Execute()
        {

        }
    }
}