using NguyenQuynhAnhBTH2.Data;
using Microsoft.AspNetCore.Mvc;
using NguyenQuynhAnhBTH2.Models;
using Microsoft.EntityFrameworkCore;
using NguyenQuynhAnhBTH2.Models.Process;

namespace NguyenQuynhAnhBTH2.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonController (ApplicationDbContext context)
        {
            _context = context;
        }

        //Khai báo class ExcelProcess trong PersonController
        private ExcelProcess _excelProcess = new ExcelProcess();

        public async Task<IActionResult> Index()
        {
            var model = await _context.Persons.ToListAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person ps)
        {
            if(ModelState.IsValid)
            {
                _context.Add(ps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private bool PersonExists (string id)
        {
            return _context.Persons.Any(e => e.PersonID == id);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return View("NotFound");
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonID,PersonName")] Person ps)
        {
            if (id != ps.PersonID)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(ps.PersonID))
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
            return View(ps);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            var ps = await _context.Persons.FirstOrDefaultAsync(m => m.PersonID == id);
            if (ps == null)
            {
                return View("NotFound");
            }

            return View(ps);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ps = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(ps);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upload(IFormFile file)
        {
            if (file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    //rename file when upload to sever
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //read data from file and write to database
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop to read data form dt
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create a new Person object
                            var per = new Person();
                            //set values for attribiutes
                            per.PersonID = dt.Rows[i][0].ToString();
                            per.PersonName = dt.Rows[i][1].ToString();
                            per.PersonAddress = dt.Rows[i][2].ToString();
                            //add oject to context
                            _context.Person.Add(per);
                        }
                        //save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
    }
}