using Discount.gRPC.Data;
using Discount.gRPC.Domain.Entities;
using Discount.gRPC.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly DiscountContext _context;
        public DiscountService(DiscountContext context)
        {
            _context = context;
        }

        public override async Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var existingCoupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (existingCoupon != null) {
                throw new RpcException(new Status(StatusCode.AlreadyExists, "Coupon already exists for this product.") );
            }
            var coupon = request.Adapt<Coupon>();
            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();
            var createDiscountResponse = coupon.Adapt<CreateDiscountResponse>();
            return createDiscountResponse;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ID == request.Id);
            if (coupon == null) { 
               return new DeleteDiscountResponse
                {
                    Success = false
                };
            }
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return new DeleteDiscountResponse
            {
                Success = true
            };
        }

        public override async Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null) {
                return new GetDiscountResponse
                {
                    Amount = 0,
                    Description = "No description",
                    ProductName = request.ProductName
                };
            }
            var discountResponse = coupon.Adapt<GetDiscountResponse>();
            return discountResponse;
        }

        public override async Task<UpdateDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var existingCoupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (existingCoupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon is not found."));
            }
            request.Adapt(existingCoupon);
            _context.Coupons.Update(existingCoupon);
            await _context.SaveChangesAsync();
            var updateDiscountResponse = existingCoupon.Adapt<UpdateDiscountResponse>();
            return updateDiscountResponse;
        }
    }
}
