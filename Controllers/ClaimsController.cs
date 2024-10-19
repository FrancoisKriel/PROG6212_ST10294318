using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ST10294318_PROG6212_POE.Controllers
{
    public class ClaimsController : Controller { 
     
        private static List<ClaimModel> claims = new List<ClaimModel>(); // Simulated in-memory data store

        // Display the claim submission form
        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View();
        }

        // Handle claim submission
        [HttpPost]
        public IActionResult SubmitClaim(ClaimModel claim)
        {
            if (ModelState.IsValid)
            {
                // Simulate saving to database (in-memory list)
                claim.Id = claims.Count + 1; // Simulated auto-increment ID
                claims.Add(claim);
                ViewBag.Message = "Claim submitted successfully!";
            }
            return View();
        }

        // View all submitted claims
        public IActionResult ViewClaims()
        {
            return View(claims); // Pass the list of claims to the view
        }

        // Approve a claim
        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Approved";
            }
            return RedirectToAction("ViewClaims");
        }

        // Reject a claim
        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Rejected";
            }
            return RedirectToAction("ViewClaims");
        }

        // Display the document upload form
        public IActionResult UploadDocuments()
        {
            return View();
        }

        // Handle document upload
        [HttpPost]
        public IActionResult UploadDocument(IFormFile document)
        {
            if (document != null && document.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/documents", document.FileName);
                try
                {
                    // Save the file to the specified location
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        document.CopyTo(stream);
                    }
                    ViewBag.Message = "Document uploaded successfully!";
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during file upload
                    ViewBag.Message = $"Error uploading document: {ex.Message}";
                }
            }
            else
            {
                ViewBag.Message = "Please select a document to upload.";
            }
            return View("UploadDocuments");
        }
    }
}