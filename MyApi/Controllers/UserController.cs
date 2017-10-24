using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace MyApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly ApiDBContext _context;

        public UserController(ApiDBContext context)
        {
            _context = context;
        }

        /*
         *GET: api/users test
         *Get all users
         */
        [HttpGet]
        public IEnumerable<Users> GetUsers()
        {
            return _context.Users;
        }

        /*
         *GET: api/users/1 
         *Get user by id
         */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.UserId == id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        /*
         *GET: api/users/1 
         *Edit user by id
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers([FromRoute] int id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.UserId)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /*
         *GET: api/users/create
         *Create new user
         */
        [HttpPost("create")]
        public async Task<IActionResult> PostUsers([FromBody] Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //encrypte password                        
            System.Diagnostics.Debug.WriteLine("KEY-GENERATOR", this.KeyGenerator());
            System.Diagnostics.Debug.WriteLine("PASSWORD-HASHED", this.SHA1(user.Password));
            var hashedPassword = this.SHA1(user.Password + this.KeyGenerator());           
            user.Skey = this.KeyGenerator();
            user.Password = hashedPassword;            

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.UserId }, user);
        }

        /*
         *GET: api/users/1 
         *Delete user by id
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return Ok(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        private string EncryptPassword(string password)
        {           
            return password;
        }

        private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string        
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        private string KeyGenerator()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return this.CalculateMD5Hash(GuidString);
        }

        private string SHA1(string input)
        {
            byte[] hash;

            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
            }
            var sb = new StringBuilder();

            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);

            return sb.ToString();

        }
    }
}