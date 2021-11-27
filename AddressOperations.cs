using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace XF_Mid2_Lab1
{
    public class AddressOperations
    {
        readonly SQLiteAsyncConnection db;

        public AddressOperations(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Address>().Wait();
        }
        //Get all Address.
        public Task<List<Address>> GetAllAddressAsync()
        {

            return db.Table<Address>().ToListAsync();
        }       
        // Get a specific address by HomeNumber and City. 
        public Task<Address> GetAddressAsync(string home, string city)
        {
            return db.Table<Address>().Where(i => i.HomeNumber == home && i.City == city).FirstOrDefaultAsync();
        }

        // Get all people living in address by HomeNumber and City.
        public Task<List<Address>> GetAddressPeopleAsync(string home, string city)
        {
            return db.Table<Address>().Where(i => i.HomeNumber == home && i.City == city).ToListAsync();
        }

        public Task<int> SaveAddressAsync(Address address)
        {
            if (address.Id != 0)
            {
                // Update an existing address.
                return db.UpdateAsync(address);
            }
            else
            {
                // Save a new address.
                return db.InsertAsync(address);
            }
        }
        // Delete address.
        public Task<int> DeleteAddressAsync(Address address)
        {
            return db.DeleteAsync(address);
        }

    }
}
