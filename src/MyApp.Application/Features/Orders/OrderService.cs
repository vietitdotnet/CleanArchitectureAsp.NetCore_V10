using AutoMapper;
using MyApp.Application.Common.Results;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Orders.DTOs;
using MyApp.Application.Features.Orders.Requests;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Entities.Districts;
using MyApp.Domain.Entities.Owns;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications.Communes;
using MyApp.Domain.Specifications.Orders;
using MyApp.Domain.Specifications.Products;
using MyApp.Domain.Specifications.Provinces;

namespace MyApp.Application.Features.Orders
{
    public class OrderService : BaseService, IOderService 
    {
        public OrderService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
        {
        }


        public async Task<IReadOnlyList<OrderDto>> GetAllOrdersAsync(CancellationToken ct = default)
        {
            var spec = new OrderSpec();
            var orders = await _unitOfWork.Repository<Order, int>().ListProjectedAsync<OrderDto>(spec, ct);
            return orders;
        }

        public async Task<PagedResponse<OrderDto, OrderParameters>> GetOrdersAsync(OrderParameters param, CancellationToken ct = default)
        {
            var spec = new OrderParamSpec(param);

            var orders = await _unitOfWork.Repository<Order, int>()
                .GetPagedAsync<OrderDto,OrderParameters>(spec, param, ct);
           
            return orders;
        }

        public async Task<OperationResult<OrderDto>> CreateOrderAsync(
        CreateOrderRequest request,
        string? userId,
        CancellationToken ct = default)
        {
            await ValidateAsync(request);

            // 1. Validate basic (FluentValidation đã xử lý rồi)
            if (request.Items == null || !request.Items.Any())
                throw new BadRequestException("Đơn hàng phải có ít nhất một mặt hàng.");

            // 2. Load Province
            var province = await _unitOfWork.Repository<Province, int>()
                .FirstOrDefaultAsync(new ProvinceByCodeSpec(request.ProvinceCode), ct);

            if (province == null)
                throw new NotFoundException("Province not found");

            // 3. Load Commune
            var commune = await _unitOfWork.Repository<Commune, int>()
                .FirstOrDefaultAsync(new CommuneByCodeSpec(request.CommuneCode), ct);

            if (commune == null)
                throw new NotFoundException("Commune not found");

            // 4. Validate relation
            if (commune.ProvinceCode != province.Code)
                throw new BadRequestException("Commune không thuộc Province");

            // 5. Load Products
            var productIds = request.Items.Select(x => x.ProductId).ToList();

            var products = await _unitOfWork.Repository<Product, int>()
                .ListAsync(new ProductByIdsSpec(productIds), ct);

            if (products.Count != productIds.Count)
                throw new NotFoundException("Some products not found");

            // 6. Create Order
            var order = Order.Create(userId);

            order.SetNote(request.Note);

            // 7. Customer
            var customer = AnonCustomer.Create(
                request.CustomerName,
                request.PhoneNumber,
                request.Email
            );

            order.SetCustomer(customer);


            // 8. Address (snapshot)
            var address = ShippingAddress.Create(
                province.Code,
                province.Name,
                commune.Code,
                commune.Name,
                request.AddressDetail
            );

            order.SetShippingAddress(address);

            // 9. Add products
            foreach (var item in request.Items)
            {
                var product = products.First(x => x.Id == item.ProductId);

                order.AddProduct(null, item.Quantity);
            }

            // 10. Domain validate
            order.Validate();

            // 11. Save
            await _unitOfWork.Repository<Order, int>().AddAsync(order, ct);
            
            await _unitOfWork.SaveChangesAsync(ct);

            // 12. Map DTO

            return OperationResult<OrderDto>.Ok(_mapper.Map<OrderDto>(order), "Order created successfully");
        }

    }
}
