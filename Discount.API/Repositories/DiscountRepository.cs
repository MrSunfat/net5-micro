using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetValue<string>("DatabaseSettings:ConnectionStrings");
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = $"Insert into \"{nameof(Coupon)}\" (\"ProductName\", \"Description\", \"Amount\") Values (@ProductName, @Description, @Amount)";
            var affected = await connection.ExecuteAsync
                (query,
                new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var affected = await connection.ExecuteAsync
                ($"DELETE FROM \"{nameof(Coupon)}\" WHERE \"ProductName\"=@ProductName",
                new
                {
                    ProductName = productName
                });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ($"SELECT * FROM \"{nameof(Coupon)}\" WHERE \"ProductName\" = @ProductName", 
                new
                {
                    ProductName = productName
                });
            if (coupon == null)
            {
                return new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Description" };
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon couponItem)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = $"Update \"{nameof(Coupon)}\"" + " SET \"ProductName\"=@ProductName,\"Description\"=@Description,\"Amount\"=@Amount WHERE \"ID\" = @ID";
            var affected = await connection.ExecuteAsync
                (query,
                new
                {
                    ProductName = couponItem.ProductName,
                    Description = couponItem.Description,
                    Amount = couponItem.Amount,
                    ID = couponItem.ID
                });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
