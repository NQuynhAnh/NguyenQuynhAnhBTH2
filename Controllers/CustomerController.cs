using NguyenQuynhAnhBTH2.Data;
using NguyenQuynhAnhBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NguyenQuynhAnhBTH2.Controllers
{
    public class CustomerController : Controller
    {
        //khai báo DbContext để làm việc với database
        private readonly ApplicationDbContext _context;
        public CustomerController (ApplicationDbContext context)
        {
            _context = context;
        }

        //Action trả về view hiển thị danh sách sinh viên
        public async Task<IActionResult> Index()
        {
            var model = await _context.Customers.ToListAsync();
            return View(model);
        }

        //Phương thức kiểm tra xem mã sinh viên có tồn tại trong cơ sở dữ liệu hay không
        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        //Action trả về vierw thêm mới sinh viên
        public IActionResult Create()
        {
            return View();
        }

        //Action xử lý dữ liệu sinh viên gửi lên từ view và lưu vào database
        [HttpPost]
        public async Task<IActionResult> Create(Customer cus)
        {
            if(ModelState.IsValid)
            {
                _context.Add(cus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cus);
        }

        //Phương thức Edit kiểm tra xem id của sinh viên có tồn tại trong csdl không, có thì cho người dùng edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return View("NotFound");
            }
            return View(customer);
        }

        //Tạo phương thức edit cập nhật thông tin của sinh viên theo mã sinh viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerID,CustomerName")] Customer cus)
        {
            if(id != cus.CustomerID)
            {
                return View("NotFound");
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(cus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(cus.CustomerID))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cus);
        }

        //Phương thức Delete
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            var cus = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerID == id);
            if (cus == null)
            {
                return View("NotFound");
            }
            return View(cus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cus = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(cus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}