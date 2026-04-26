using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Communes
{
    public class CommuneByCodeSpec : CommuneSpec
    {
        public CommuneByCodeSpec(string code)  
        {
            Criteria = x => x.Code == code;
        }
    }
}
