// Controllers/SSQCustomersController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BMSBT.Models;
using BMSBT.ViewModels;

using System.Linq;
using System.Threading.Tasks;

namespace BMSBT.Controllers
{
    public class SSQCustomersController : Controller
    {
        private readonly BmsbtContext _context;

        public SSQCustomersController(BmsbtContext context)
        {
            _context = context;
        }

        // GET: SSQCustomers
        // GET: SSQCustomers
        public async Task<IActionResult> Index(string searchString, string sortOrder, int page = 1, int pageSize = 10)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BtnoSortParm"] = sortOrder == "btno" ? "btno_desc" : "btno"; // CHANGED: CustomerNoSortParm to BtnoSortParm
            ViewData["CurrentFilter"] = searchString;

            var customers = from c in _context.CustomersDetails
                            select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c =>
                    (c.Btno != null && c.Btno.Contains(searchString)) ||      // CHANGED: c.CustomerNo to c.Btno
                    (c.CustomerName != null && c.CustomerName.Contains(searchString)) ||
                    (c.Cnicno != null && c.Cnicno.Contains(searchString)) ||
                    (c.MobileNo != null && c.MobileNo.Contains(searchString)) ||
                    (c.City != null && c.City.Contains(searchString)) ||
                    (c.Sector != null && c.Sector.Contains(searchString)) ||
                    (c.Block != null && c.Block.Contains(searchString)) ||
                    (c.PloNo != null && c.PloNo.Contains(searchString)));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.CustomerName);
                    break;
                case "btno":                                      // CHANGED: "customerNo" to "btno"
                    customers = customers.OrderBy(c => c.Btno);   // CHANGED: c.CustomerNo to c.Btno
                    break;
                case "btno_desc":                                 // CHANGED: "customerNo_desc" to "btno_desc"
                    customers = customers.OrderByDescending(c => c.Btno); // CHANGED: c.CustomerNo to c.Btno
                    break;
                default:
                    customers = customers.OrderBy(c => c.Uid);
                    break;
            }

            var totalRecords = await customers.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var paginatedCustomers = await customers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;
            ViewData["PageSize"] = pageSize;

            return View(paginatedCustomers);
        }

        // GET: SSQCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customersDetail = await _context.CustomersDetails
                .FirstOrDefaultAsync(m => m.Uid == id);

            if (customersDetail == null)
            {
                return NotFound();
            }

            return View(customersDetail);
        }

        // GET: SSQCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SSQCustomers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerNo,Btno,CustomerName,GeneratedMonthYear,LocationSeqNo,Cnicno,FatherName,InstalledOn,MobileNo,TelephoneNo,MeterType,Ntnnumber,City,Project,SubProject,TariffName,BankNo,BtnoMaintenance,Category,Block,PlotType,Size,Sector,PloNo,BillStatusMaint,BillStatus,BillGenerationStatus,History,MeterNo")] CustomersDetail customersDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customersDetail);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Customer created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(customersDetail);
        }

        // GET: SSQCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customersDetail = await _context.CustomersDetails.FindAsync(id);
            if (customersDetail == null)
            {
                return NotFound();
            }
            return View(customersDetail);
        }

        // POST: SSQCustomers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Uid,CustomerNo,Btno,CustomerName,GeneratedMonthYear,LocationSeqNo,Cnicno,FatherName,InstalledOn,MobileNo,TelephoneNo,MeterType,Ntnnumber,City,Project,SubProject,TariffName,BankNo,BtnoMaintenance,Category,Block,PlotType,Size,Sector,PloNo,BillStatusMaint,BillStatus,BillGenerationStatus,History,MeterNo")] CustomersDetail customersDetail)
        {
            if (id != customersDetail.Uid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customersDetail);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Customer updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersDetailExists(customersDetail.Uid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customersDetail);
        }

        // GET: SSQCustomers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var customersDetail = await _context.CustomersDetails
        //        .FirstOrDefaultAsync(m => m.Uid == id);

        //    if (customersDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customersDetail);
        //}

        //// POST: SSQCustomers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var customersDetail = await _context.CustomersDetails.FindAsync(id);
        //    if (customersDetail != null)
        //    {
        //        _context.CustomersDetails.Remove(customersDetail);
        //        await _context.SaveChangesAsync();
        //        TempData["SuccessMessage"] = "Customer deleted successfully!";
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        //// AJAX Delete
        //[HttpPost]
        //public async Task<IActionResult> DeleteAjax(int id)
        //{
        //    var customersDetail = await _context.CustomersDetails.FindAsync(id);
        //    if (customersDetail == null)
        //    {
        //        return Json(new { success = false, message = "Customer not found." });
        //    }

        //    try
        //    {
        //        _context.CustomersDetails.Remove(customersDetail);
        //        await _context.SaveChangesAsync();
        //        return Json(new { success = true, message = "Customer deleted successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = $"Error: {ex.Message}" });
        //    }
        //}

        private bool CustomersDetailExists(int id)
        {
            return _context.CustomersDetails.Any(e => e.Uid == id);
        }












        public async Task<IActionResult> Dashboard()
        {
            try
            {
                // Get all projects from Configurations table where ConfigKey = "project"
                var projects = await _context.Configurations
                    .Where(c => c.ConfigKey == "project")
                    .Select(c => c.ConfigValue)
                    .Distinct()
                    .OrderBy(p => p)
                    .ToListAsync();

                // Get blocks from Configurations table for specific keys
                var mohlanwalBlocks = await _context.Configurations
                    .Where(c => c.ConfigKey == "BlockMohlanwal")
                    .Select(c => c.ConfigValue)
                    .Distinct()
                    .ToListAsync();

                var orchardsBlocks = await _context.Configurations
                    .Where(c => c.ConfigKey == "BlockOrchards")
                    .Select(c => c.ConfigValue)
                    .Distinct()
                    .ToListAsync();

                // Combine all blocks
                var allBlocks = new List<string>();
                allBlocks.AddRange(mohlanwalBlocks ?? new List<string>());
                allBlocks.AddRange(orchardsBlocks ?? new List<string>());

                // Remove duplicates and sort
                var blocks = allBlocks.Distinct().OrderBy(b => b).ToList();

                // Get total customers count for all projects
                var totalCustomers = await _context.CustomersDetails.CountAsync();

                // Get customers count by project
                var projectStats = await _context.CustomersDetails
                    .Where(c => !string.IsNullOrEmpty(c.Project))
                    .GroupBy(c => c.Project)
                    .Select(g => new ProjectStatisticsViewModel
                    {
                        ProjectName = g.Key,
                        TotalCustomers = g.Count()
                    })
                    .OrderBy(p => p.ProjectName)
                    .ToListAsync();

                // Get customers count by block
                var blockStats = await _context.CustomersDetails
                    .Where(c => !string.IsNullOrEmpty(c.Block))
                    .GroupBy(c => c.Block)
                    .Select(g => new BlockStatisticsViewModel
                    {
                        BlockName = g.Key,
                        TotalCustomers = g.Count()
                    })
                    .OrderBy(b => b.BlockName)
                    .ToListAsync();

                // Prepare data for view
                ViewBag.Projects = projects ?? new List<string>();
                ViewBag.Blocks = blocks ?? new List<string>();
                ViewBag.MohlanwalBlocks = mohlanwalBlocks ?? new List<string>();
                ViewBag.OrchardsBlocks = orchardsBlocks ?? new List<string>();
                ViewBag.TotalAllCustomers = totalCustomers;
                ViewBag.ProjectStatistics = projectStats ?? new List<ProjectStatisticsViewModel>();
                ViewBag.BlockStatistics = blockStats ?? new List<BlockStatisticsViewModel>();

                return View();
            }
            catch (Exception ex)
            {
                // Log error if needed
                ViewBag.ErrorMessage = "Error loading dashboard data: " + ex.Message;
                return View();
            }
        }

        // Update the GetAllBlocksStatistics method
        // AJAX action to get all blocks statistics
        [HttpGet]
        public async Task<IActionResult> GetAllBlocksStatistics()
        {
            try
            {
                // Get blocks from both configuration keys
                var mohlanwalBlocks = await _context.Configurations
                    .Where(c => c.ConfigKey == "BlockMohlanwal")
                    .Select(c => c.ConfigValue)
                    .Distinct()
                    .ToListAsync();

                var orchardsBlocks = await _context.Configurations
                    .Where(c => c.ConfigKey == "BlockOrchards")
                    .Select(c => c.ConfigValue)
                    .Distinct()
                    .ToListAsync();

                // Combine all blocks
                var allBlocks = new List<string>();
                allBlocks.AddRange(mohlanwalBlocks ?? new List<string>());
                allBlocks.AddRange(orchardsBlocks ?? new List<string>());
                var blocks = allBlocks.Distinct().OrderBy(b => b).ToList();

                var statistics = new List<object>();

                foreach (var block in blocks)
                {
                    var totalCustomers = await _context.CustomersDetails
                        .Where(c => c.Block == block)
                        .CountAsync();

                    // Get projects distribution for this block
                    var projectsInBlock = await _context.CustomersDetails
                        .Where(c => c.Block == block)
                        .GroupBy(c => c.Project)
                        .Select(g => new
                        {
                            ProjectName = g.Key,
                            Count = g.Count()
                        })
                        .OrderByDescending(p => p.Count)
                        .Take(3) // Top 3 projects
                        .ToListAsync();

                    // Determine which configuration the block belongs to
                    var blockType = "Unknown";
                    if (mohlanwalBlocks != null && mohlanwalBlocks.Contains(block))
                        blockType = "Mohlanwal";
                    else if (orchardsBlocks != null && orchardsBlocks.Contains(block))
                        blockType = "Orchards";

                    statistics.Add(new
                    {
                        BlockName = block,
                        BlockType = blockType,
                        TotalCustomers = totalCustomers,
                        TopProjects = projectsInBlock
                    });
                }

                return Json(new
                {
                    success = true,
                    statistics = statistics
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Error: {ex.Message}"
                });
            }
        }






        // Controllers/SSQCustomersController.cs (add these methods)

        // GET: SSQCustomers/CustomersSelection
        public async Task<IActionResult> CustomersSelection()
        {
            try
            {
                // Get all projects from Configurations table where ConfigKey = "Project" or "Projects"
                var projects = await _context.Configurations
                    .Where(c => c.ConfigKey == "Project" || c.ConfigKey == "Projects")
                    .Select(c => c.ConfigValue)
                    .Distinct()
                    .OrderBy(p => p)
                    .ToListAsync();

                // Get blocks from both configuration keys
                var mohlanwalBlocks = await _context.Configurations
                    .Where(c => c.ConfigKey == "BlockMohlanwal")
                    .Select(c => c.ConfigValue)
                    .Distinct()
                    .ToListAsync();

                var orchardsBlocks = await _context.Configurations
                    .Where(c => c.ConfigKey == "BlockOrchards")
                    .Select(c => c.ConfigValue)
                    .Distinct()
                    .ToListAsync();

                // Combine all blocks
                var allBlocks = new List<string>();
                allBlocks.AddRange(mohlanwalBlocks ?? new List<string>());
                allBlocks.AddRange(orchardsBlocks ?? new List<string>());
                var blocks = allBlocks.Distinct().OrderBy(b => b).ToList();

                // Get categories from Configurations table where ConfigKey = "Category" or "Categories"
                var plotTypes = await _context.Configurations
      .Where(c => c.ConfigKey == "PlotType") // Key change here
      .Select(c => c.ConfigValue)
      .Distinct()
      .OrderBy(p => p)
      .ToListAsync();

                ViewBag.Projects = projects ?? new List<string>();
                ViewBag.Blocks = blocks ?? new List<string>();
                ViewBag.Categories = plotTypes ?? new List<string>(); // Renamed for clarity, but keep 'Categories' 
                ViewBag.MohlanwalBlocks = mohlanwalBlocks ?? new List<string>();
                ViewBag.OrchardsBlocks = orchardsBlocks ?? new List<string>();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error loading selection data: " + ex.Message;
                return View();
            }
        }

        // POST: SSQCustomers/
        // 

        
        [HttpPost]
        public async Task<IActionResult> GetCustomersBySelection(string project, string block, string category)
        {
            try
            {
                var query = _context.CustomersDetails.AsQueryable();

                // Apply filters...
                if (!string.IsNullOrEmpty(project) && project != "All")
                    query = query.Where(c => c.Project == project);

                if (!string.IsNullOrEmpty(block) && block != "All")
                    query = query.Where(c => c.Block == block);

                if (!string.IsNullOrEmpty(category) && category != "All")
                    query = query.Where(c => c.PlotType == category); // Updated to PlotType

                var totalRecords = await query.CountAsync();

                // NO LONGER FETCHING CUSTOMER DETAILS
                return Json(new
                {
                    success = true,
                    totalRecords = totalRecords,  // Only returning count
                    message = $"Found {totalRecords} customer(s) matching the criteria"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // POST: SSQCustomers/GetAllCustomersBySelection
        [HttpPost]
        public async Task<IActionResult> GetAllCustomersBySelection(string project, string block, string category)
        {
            try
            {
                // Start with base query
                var query = _context.CustomersDetails.AsQueryable();

                // Apply filters if provided
                if (!string.IsNullOrEmpty(project) && project != "All")
                {
                    query = query.Where(c => c.Project == project);
                }

                if (!string.IsNullOrEmpty(block) && block != "All")
                {
                    query = query.Where(c => c.Block == block);
                }

                if (!string.IsNullOrEmpty(category) && category != "All")
                {
                    query = query.Where(c => c.Category == category);
                }

                // Get all customer details
                var customerDetails = await query
                    .Select(c => new CustomerDetailResult
                    {
                        CustomerNo = c.CustomerNo,
                        CustomerName = c.CustomerName,
                        CNICNo = c.Cnicno,
                        MobileNo = c.MobileNo,
                        City = c.City,
                        Sector = c.Sector,
                        Block = c.Block,
                        PlotNo = c.PloNo,
                        Project = c.Project,
                        Category = c.Category
                    })
                    .ToListAsync();

                return Json(new
                {
                    success = true,
                    customerDetails = customerDetails,
                    message = $"Retrieved {customerDetails.Count} customer(s)"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Error: {ex.Message}"
                });
            }
        }





    }
}