using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Provinces
{
    public class ProvinceByCodeSpec : ProvinceSpec
    {
        public ProvinceByCodeSpec(string code)
        {
            Criteria = x => x.Code == code;
        }
    }
}
