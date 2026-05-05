
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Paginations.Parameters
{
    public class UserParameters : PagingParameters
    {
        protected override int DefaultPageSize => 10;


        public override void Normalize()
        {
            base.Normalize();
          
        }
    }

}
