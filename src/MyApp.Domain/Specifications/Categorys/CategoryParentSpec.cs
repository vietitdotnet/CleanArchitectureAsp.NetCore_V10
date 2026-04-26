using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyApp.Domain.Specifications.Categorys
{
    public class CategoryParentSpec : CategorySpec
    {
        public CategoryParentSpec()
        {
            Criteria = x => x.ParentCategoryId == null;
        }
    }
}
