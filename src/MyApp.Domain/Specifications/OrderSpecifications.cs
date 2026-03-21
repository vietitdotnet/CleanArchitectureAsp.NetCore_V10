
using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;


namespace MyApp.Domain.Specifications
{
    public static class OrderSpecifications
    {
        /// <summary>
        /// 🔹 Lấy tất cả đơn hàng (không include gì)
        /// </summary>
        public static BaseSpecification<Order> All()
        {
            return new BaseSpecification<Order>(x => true);
        }

        /// <summary>
        /// 🔹 Lấy đơn hàng theo Id (không include)
        /// </summary>
        public static BaseSpecification<Order> ById(int id)
        {
            return new BaseSpecification<Order>(x => x.Id == id);
        }

        /// <summary>
        /// 🔹 Lấy tất cả đơn hàng kèm danh sách sản phẩm (OrderProducts + Product)
        /// </summary>
        public static BaseSpecification<Order> AllWithProducts()
        {
            var spec = new BaseSpecification<Order>(x => true);

            spec.AddInclude(x => x.OrderProducts);
            spec.AddInclude(x => x.OrderProducts.Select(op => op.Product)); // ThenInclude

            return spec;
        }

        /// <summary>
        /// 🔹 Lấy đơn hàng theo Id kèm danh sách sản phẩm
        /// </summary>
        public static BaseSpecification<Order> ByIdWithProducts(int id)
        {
            var spec = new BaseSpecification<Order>(x => x.Id == id);

            spec.AddInclude(x => x.OrderProducts);
            spec.AddInclude(x => x.OrderProducts.Select(op => op.Product));
            spec.ApplyAsNoTracking();
            return spec;
        }

        /// <summary>
        /// 🔹 Lấy tất cả đơn hàng kèm thông tin User
        /// </summary>
        public static BaseSpecification<Order> AllWithUser()
        {
            var spec = new BaseSpecification<Order>(x => true);

            spec.AddInclude(x => x.User);
            spec.ApplyAsNoTracking();
            return spec;
        }

        /// <summary>
        /// 🔹 Lấy đơn hàng theo Id kèm thông tin User
        /// </summary>
        public static BaseSpecification<Order> ByIdWithUser(int id)
        {
            var spec = new BaseSpecification<Order>(x => x.Id == id);

            spec.AddInclude(x => x.User);
            spec.ApplyAsNoTracking();
            return spec;
        }

        /// <summary>
        /// 🔹 Lấy tất cả đơn hàng kèm User và danh sách sản phẩm (đầy đủ thông tin)
        /// </summary>
        public static BaseSpecification<Order> AllWithUserAndProducts()
        {
            var spec = new BaseSpecification<Order>(x => true);

            spec.AddInclude(x => x.User);
            spec.AddInclude(x => x.OrderProducts);
            spec.AddInclude(x => x.OrderProducts.Select(op => op.Product));
            spec.ApplyAsNoTracking();
            return spec;
        }

        /// <summary>
        /// 🔹 Lấy đơn hàng theo Id kèm User và danh sách sản phẩm
        /// </summary>
        public static BaseSpecification<Order> ByIdWithUserAndProducts(int id)
        {
            var spec = new BaseSpecification<Order>(x => x.Id == id);
            spec.AddInclude(x => x.User);
            spec.AddInclude(x => x.OrderProducts);
            spec.AddInclude(x => x.OrderProducts.Select(op => op.Product));
            spec.ApplyAsNoTracking();
            return spec;
        }

        public static BaseSpecification<Order> GetOrdersWithUserAndProductPage(int pageNumber , int pageSize)
        {
            var spec = new BaseSpecification<Order>(x => true);

            spec.AddInclude(x => x.User);
            spec.AddInclude(x => x.OrderProducts);
            spec.AddInclude(x => x.OrderProducts.Select(op => op.Product));
            spec.ApplyPaging((pageNumber -1) * pageSize , pageSize);
            spec.ApplyAsNoTracking();
            return spec;
        }
    }
}
