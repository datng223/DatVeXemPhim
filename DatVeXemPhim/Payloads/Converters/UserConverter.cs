using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class UserConverter
    {
        private readonly AppDbContext _context;
        public UserConverter()
        {
            _context = new AppDbContext();
        }
        public DataResponseUser EntityToDTO(User user)
        {
            return new DataResponseUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                RankCustomerName = _context.rankCustomers.SingleOrDefault(x => x.Id == user.RankCustomerId).Name,
                UserStatusName = _context.userStatuses.SingleOrDefault(x => x.Id == user.UserStatusId).Name,
                IsActive = user.IsActive,
                RoleName = _context.roles.SingleOrDefault(x => x.Id == user.RoleId).RoleName
            };
        }
    }
}
