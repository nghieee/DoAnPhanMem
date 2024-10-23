using DoAnPhanMem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace DoAnPhanMem.Controllers
{
    public class AccountController : Controller
    {
        private readonly LongChauStoreContext _context;

        public AccountController(LongChauStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["LoginError"] = "Tài khoản và mật khẩu không được để trống";
                return RedirectToAction("Index", "Home"); 
            }

            var user = _context.Users.FirstOrDefault(u => u.FullName == username && u.Password == password);

            if (user != null)
            {
                //Thiết lập Session cho người dùng
                HttpContext.Session.SetString("username", user.FullName);
                HttpContext.Session.SetString("role", user.Role);

                if (user.Role == "user")
                {
                    return RedirectToAction("Index", "Home"); 
                }
                else if (user.Role == "admin")
                {
                    return RedirectToAction("Dashboard", "Admin"); 
                }
            }

            TempData["LoginError"] = "Tài khoản hoặc mật khẩu không đúng";
            return RedirectToAction("Index", "Home"); 
        }

        [HttpPost]
        public IActionResult Register(string fullName, string phone, string email, string password, string address)
        {
            Console.WriteLine($"Name: {fullName}, Phone: {phone}, Email: {email}, Address: {address}");
            // Kiểm tra xem các trường có bị bỏ trống không
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["RegisterError"] = "Vui lòng điền đầy đủ thông tin";
                return RedirectToAction("Index", "Home"); // Quay lại trang chính hoặc trang popup đăng ký
            }

            // Kiểm tra email đã tồn tại chưa
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                TempData["RegisterError"] = "Email đã được sử dụng";
                return RedirectToAction("Index", "Home");
            }

            // Tạo người dùng mới
            var newUser = new User
            {
                FullName = fullName,
                Phone = phone,
                Email = email,
                Password = HashPassword(password), 
                Address = address,
                Role = "user" // Mặc định là người dùng bình thường
            };

            _context.Users.Add(newUser); // Thêm vào database
            _context.SaveChanges(); // Lưu thay đổi

            // Sau khi đăng ký thành công, có thể tự động đăng nhập cho người dùng mới
            HttpContext.Session.SetString("username", newUser.FullName);
            HttpContext.Session.SetString("role", newUser.Role);

            return RedirectToAction("Index", "Home"); // Quay về trang chủ hoặc một trang khác
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home"); 
        }
    }
}
