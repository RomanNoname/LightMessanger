using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using LightMessanger.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace LightMessanger.BLL.Services
{
    public class UsersService : IUsersService
    {
        private IUsersRepository _context;
        private string _folder;
        public UsersService(IUsersRepository context, string folder = "wwwroot")
        {
            _context = context;
            _folder = folder;
        }
        public async Task AddAsync(User item)
        {
            if (item is null)
                throw new ArgumentNullException("item");
            if (string.IsNullOrWhiteSpace(item.Name) || item.Name.Length < 4 || item.Name.Length > 50)
                throw new ArgumentException("Invalid Username");
            if (string.IsNullOrWhiteSpace(item.Password) || item.Password.Length < 3 || item.Password.Length > 50)
                throw new ArgumentException("Invalid Password");
            if (!Regex.IsMatch(item.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                throw new ArgumentException("Invalid Email");
            item.Password = SHA256Managed(item.Password);

            await _context.AddAsync(item);
        }
        private string SHA256Managed(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = new SHA256Managed().ComputeHash(passwordBytes);
            string hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hash.Remove(50);
        }
        public async Task DeleteAsync(User item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            await _context.DeleteAsync(item);
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _context.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task UpdateAsync(User item)
        {
            if (item is null)
                throw new ArgumentNullException("item");
            if (string.IsNullOrWhiteSpace(item.Name) || item.Name.Length > 30)
                throw new ArgumentException("Invalid Username");
            if (string.IsNullOrWhiteSpace(item.Password) || item.Name.Length < 3 || item.Name.Length > 30)
                throw new ArgumentException("Invalid Password");
            if (!Regex.IsMatch(item.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                throw new ArgumentException("Invalid Email");
           

            await _context.UpdateAsync(item);
        }

        public async Task<User> GetValueByСonditionAsync<T>(Func<User, T> valueSelector, T value)
        {
            return await _context.GetValueByСonditionAsync(valueSelector, value);
        }

        public async Task<User> GetUserByLoginPasswordAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            if (!string.IsNullOrEmpty(password))
                password = SHA256Managed(password);
            return await _context.GetUserByLoginPasswordAsync(login, password);
        }

        public async Task<User> GetAllIncludes<T>(Func<User, T> valueSelector, T value)
        {
            return await _context.GetValueByСonditionAsync(valueSelector, value, new List<string>() 
            { "Groups", "CreatedGroups" }
            );
        }

        public async Task<User> GetUserGroupsAsync<T>(Func<User, T> valueSelector, T value)
        {
            return await _context.GetValueByСonditionAsync(valueSelector, value, new List<string>()
            {
                "Groups"
            });
        }

        //public async Task<string> AddImage(string nameFolder, string name, IFormFile file)
        //{
        //    if (File.Exists(_folder + (await _context.GetByName(name)).Image))
        //    {
        //        File.Delete(_folder + (await _context.GetByName(name)).Image);
        //    }
        //    if (string.IsNullOrWhiteSpace(nameFolder))
        //        throw new ArgumentNullException(nameof(nameFolder));
        //    if (string.IsNullOrWhiteSpace(name))
        //        throw new ArgumentNullException(nameof(name));
        //    if (file is null)
        //        throw new ArgumentNullException(nameof(file));

        //    string uploadPath = _folder + "/" + nameFolder;

        //    Directory.CreateDirectory(uploadPath);


        //    var fileName = $"{name}.{Path.GetExtension(file.FileName).Replace(".", "")}";

        //    string fullPath = $"{uploadPath}/{fileName}";


        //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }

        //    await _context.AddImage("/" + nameFolder + "/" + fileName, name);

        //    return "/" + nameFolder + "/" + fileName;
        //}

        //public async Task<User> GetByName(string name)
        //{
        //    return await _context.GetByName(name);
        //}
    }
}

