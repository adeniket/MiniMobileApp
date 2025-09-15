using Login_Registration.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Login_Registration.Services
{
    public class UserDataService
    {
        private readonly SQLiteAsyncConnection _db;
        public UserDataService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<UserModel>().Wait();
        }
        public Task<List<UserModel>> GetUsersAsync() => _db.Table<UserModel>().ToListAsync();
        public Task<UserModel> GetUserByUsernameAsync(string username) => _db.Table<UserModel>().FirstOrDefaultAsync(u => u.Username == username);
        public Task<int> AddUserAsync(UserModel user) => _db.InsertAsync(user);
        public Task<int> UpdateUserAsync(UserModel user) => _db.UpdateAsync(user);
        public Task<int> DeleteUserAsync(UserModel user) => _db.DeleteAsync(user);
    }
}
