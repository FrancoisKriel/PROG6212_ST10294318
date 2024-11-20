using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using ST10294318_PROG6212_POE.Models;
using System;
using System.Diagnostics;
using System.IO;

namespace ST10294318_PROG6212_POE.Controllers
{
    public class ErrorController : Controller
    {
        // Display the SubmitClaim form
        public IActionResult SubmitClaim()
        {
            return View();
        }

        // Handle the submission of the claim
        [HttpPost]
        public IActionResult SubmitClaim(ClaimModel claim)
        {
            if (ModelState.IsValid)
            {
                // Calculate the total amount based on hours worked and a fixed hourly rate
                decimal hourlyRate = 150.00m; // Example hourly rate
                claim. TotalAmount = claim.HoursWorked * hourlyRate;

                // Logic to save claim data to the database (to be implemented)
                ViewBag.Message = "Claim submitted successfully!";
                return View("SubmitClaimConfirmation", claim);
            }
            else
            {
                ViewBag.Message = "Please fill in all required fields.";
                return View();
            }
        }

        // Display all submitted claims
        public IActionResult ViewClaims()
        {
            // Logic to retrieve claims from the database (to be implemented)
            return View();
        }

        // Display the form for uploading documents
        public IActionResult UploadDocuments()
        {
            return View();
        }

        // POST: Handle document upload
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

        // Improved error handling for various scenarios
        [HttpGet]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(errorViewModel);
        }
    }
}