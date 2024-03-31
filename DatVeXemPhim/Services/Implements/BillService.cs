using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.BillRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Services.Implements
{
    public class BillService : BaseService, IBillService
    {
        private readonly ResponseObject<DataResponseBill> _responseObject;
        private readonly BillConverter _converter;

        public BillService(ResponseObject<DataResponseBill> responseObject, BillConverter converter)
        {
            _responseObject = responseObject;
            _converter = converter;
        }

        public async Task<List<DataResponseBill>> GetAlls()
        {
            List<DataResponseBill> billLst = _context.bills.Select(x => _converter.EntityToDTO(x)).ToList();
            return billLst;
        }

        public async Task<ResponseObject<DataResponseBill>> GetBillById(int billId)
        {
            Bill bill = await _context.bills.SingleOrDefaultAsync(x => x.Id == billId);
            if (bill == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy hóa đơn");
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(bill));
        }

        /*public async Task<ResponseObject<DataResponseBill>> CreateBill(Request_CreateBill request)
        {
            var bill = new Bill
            {
                CustomerId = request.CustomerId,
                CreateTime = DateTime.UtcNow,
                // Other bill properties (TradingCode, Name, PromotionId, BillStatusId) can be set as needed
                IsActive = true
            };

            await _context.bills.AddAsync(bill);

            try
            {
                // Handle tickets
                if (request.SelectedTicketIds?.Any() == true)
                {
                    var tickets = await _context.Tickets
                        .Where(t => request.SelectedTicketIds.Contains(t.Id) && t.IsActive)
                        .ToListAsync();

                    bill.Subtotal += tickets.Sum(t => t.PriceTicket); // Assuming a PriceTicket property for Ticket

                    foreach (var ticket in tickets)
                    {
                        var billTicket = new BillTicket
                        {
                            BillId = bill.Id,
                            TicketId = ticket.Id,
                            Quantity = 1 // Adjust quantity logic if needed
                        };

                        await _context.BillTickets.AddAsync(billTicket);
                    }
                }

                // Handle foods
                if (request.SelectedFoodIds?.Any() == true)
                {
                    var foods = await _context.Foods
                        .Where(f => request.SelectedFoodIds.Contains(f.Id) && f.IsActive)
                        .ToListAsync();

                    bill.Subtotal += foods.Sum(f => f.Price * f.Quantity); // Assuming a Quantity property for Food

                    foreach (var food in foods)
                    {
                        var billFood = new BillFood
                        {
                            BillId = bill.Id,
                            FoodId = food.Id,
                            Quantity = 1 
                };

                        await _context.BillFoods.AddAsync(billFood);
                    }
                }

                // Apply discount (adjust logic as needed)
                bill.Discount = CalculateDiscount(request.SelectedTicketIds, request.SelectedFoodIds);

                // Calculate total money
                bill.TotalMoney = bill.Subtotal - bill.Discount;

                await _context.SaveChangesAsync();

                return _responseObject.ResponseSuccess("Tạo hóa đơn thành công", _converter.EntityToDTO(bill));
            }
            catch (Exception ex)
            {
                // Handle errors gracefully (e.g., rollback changes, log error, notify user)
                throw; // Rethrow for further handling
            }
            
        }

        private double CalculateSubtotal(int[] selectedTicketIds, int[] selectedFoodIds)
        {
            var totalTicketPrice = billTickets.Sum(t => t.Quantity * t.Ticket.Price);
            var totalFoodPrice = billFoods.Sum(f => f.Quantity * f.Food.Price);
            return totalTicketPrice + totalFoodPrice;
        }

        private double CalculateDiscount(int[] selectedTicketIds, int[] selectedFoodIds, Promotion? promotion)
        {
            // Thực hiện logic tính toán giảm giá dựa trên khuyến mãi, loại khách hàng, v.v.
            // Ví dụ này giả sử không có giảm giá

            if (promotion == null)
                return 0;

            if (promotion.RankCustomerId != bill.CustomerId)
                return 0;

            if (bill.Subtotal < promotion.Quantity)
                return 0;

            return bill.Subtotal * promotion.Percent / 100;
        }*/

        public async Task<string> RemoveBill(int billId)
        {
            try
            {
                var bill = await _context.bills.SingleOrDefaultAsync(x => x.Id == billId);
                if (bill == null)
                {
                    return "Hóa đơn không tồn tại";
                }
                _context.bills.Remove(bill);
                _context.SaveChanges();
                return "Xóa hóa đơn thành công";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
