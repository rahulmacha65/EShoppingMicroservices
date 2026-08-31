using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext DbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await DbContext.Coupons.FirstOrDefaultAsync(coupon => coupon.ProductName == request.ProductName);

        if (coupon == null)
        {
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };
        }

        logger.LogInformation("Discount is retrived for ProductName: {productName} and the discount Amount :{amount}", coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }
    public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        DbContext.Coupons.Add(coupon);
        await DbContext.SaveChangesAsync();

        logger.LogInformation("The Dicount coupon is created successfully ProductName : {productName}", coupon.ProductName);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }
    public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        DbContext.Coupons.Update(coupon);
        await DbContext.SaveChangesAsync();

        logger.LogInformation("The Discount coupon is updated successfully ProductName : {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }
    public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        DbContext.Coupons.Remove(coupon);
        await DbContext.SaveChangesAsync();

        logger.LogInformation("The Discount coupon is deleted successfully ProductName : {productName}", coupon.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }
}
