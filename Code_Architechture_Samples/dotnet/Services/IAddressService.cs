using Sabio.Models.Domain.Addresses;
using Sabio.Models.Requests.Addresses;
using System.Collections.Generic;

namespace Sabio.Services
{
    public interface IAddressService
    {
        int Add(AddressAddRequest model, int currentUserId);
        
        void Delete(int id);


        Address Get(int id);


        List<Address> SelectRandom50();


        void Update(AddressUpdateRequest model, int userId);
    }
}