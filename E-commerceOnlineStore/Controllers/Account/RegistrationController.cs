using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Controllers.Account
{
    /// <summary>
    /// Controller for handling user registration for customers and employees.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RegistrationController"/> class.
    /// </remarks>
    /// <param name="registrationService">The registration service used to handle registration logic.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController(IRegistrationService registrationService) : ControllerBase
    {
        private readonly IRegistrationService _registrationService = registrationService;

        /// <summary>
        /// Registers a new customer.
        /// </summary>
        /// <param name="model">The model containing customer registration details.</param>
        /// <returns>An IActionResult indicating the result of the registration operation.</returns>
        /// <response code="200">Customer registered successfully.</response>
        /// <response code="400">Bad request if the model is invalid.</response>
        [HttpPost("customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegistrationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registrationService.RegisterCustomerAsync(model, ModelState);
            if (result.Succeeded)
                return Ok(new { Message = "Customer registered successfully" });

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Registers a new employee.
        /// </summary>
        /// <param name="model">The model containing employee registration details.</param>
        /// <returns>An IActionResult indicating the result of the registration operation.</returns>
        /// <response code="200">Employee registered successfully.</response>
        /// <response code="400">Bad request if the model is invalid.</response>
        [HttpPost("employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeRegistrationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registrationService.RegisterEmployeeAsync(model, ModelState);
            if (result.Succeeded)
                return Ok(new { Message = "Employee registered successfully" });

            return BadRequest(result.Errors);
        }
    }
}
