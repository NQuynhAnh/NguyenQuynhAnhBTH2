using NguyenQuynhAnhBTH2.Data;
using NguyenQuynhAnhBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NguyenQuynhAnh.Controllers
{
    
    public class EmployeeController : Controller
    {
        //khai bao Dbcontext de lam viec voi database
        private readonly ApplicationDbContext _context;
        public EmployeeController (ApplicationDbContext context)
        {
            _context = context;
        }

        //Action tra ve view hien thi danh sach sinh vien
        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        //Action trả về view thêm mới danh sách sinh viên
        public IActionResult Create()
        {
            return View();
        }

        //Action xử lý dữ liệu sinh viên gửi lên từ view và lưu vào database
        [HttpPost]
        public async Task<IActionResult> Create(Employee std)
        {
            if(ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //kiem tra ma sinh vien co ton tai khong
        private bool EmployeeExists (string id)
        {
            return _context.Employees.Any(e => e.EmpID == id);
        }
        
        //Tạo phương thức Edit kiểm tra xem “id” của sinh viên có tồn tại trong cơ sở dữ liệu không? Nếu có thì trả về view “Edit” cho phép người dùng chỉnh sửa thông tin của Sinh viên đó.​
        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var Employee = await _context.Employees.FindAsync(id);
            if (Employee == null)
            {
                return View("NotFound");
            }
            return View(Employee);
        }

        //Tạo phương thức Edit cập nhật thông tin của sinh viên theo mã sinh viên.
        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmpID,EmpName")] Employee std)
        {
            if (id != std.EmpID)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(std);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(std.EmpID))
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
            return View(std);
        }

        //Tạo phương thức Delete kiểm tra xem “id” của sinh viên có tồn tại trong cơ sở dữ liệu không? Nếu có thì trả về view “Delete” cho phép người dùng xoá thông tin của Sinh viên đó.
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            var std = await _context.Employees.FirstOrDefaultAsync(m => m.EmpID == id);
            if (std == null)
            {
                return View("NotFound");
            }

            return View(std);
        }

        //Tạo phương thức Delete xoá thông tin của sinh viên theo mã sinh viên.
        //POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //upload file excel
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtenstion(file.FileName);
                if(fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    //rename file when upload to server
                    var fileNam = DataTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to sever
                        await file.CopyToAsync(stream);
                    }
                }
                return View();
            }
        }
    }
}