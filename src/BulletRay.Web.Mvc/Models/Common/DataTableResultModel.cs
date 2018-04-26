using System.Collections.Generic;

namespace BulletRay.Web.Models.Common
{
    public class DataTableResultModel<T>
    {
        public DataTableResultModel(int drawParam, int recordsTotalParam, int recordsFilteredParam, IReadOnlyList<T> dataParam)
        {
            draw = drawParam;
            recordsTotal = recordsTotalParam;
            recordsFiltered = recordsFilteredParam;
            data = dataParam;
        }

        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IReadOnlyList<T> data { get; set; }
    }
}
