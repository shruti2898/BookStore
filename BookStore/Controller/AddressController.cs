using BookStoreManager.Interface;
using BookStoreModels.UserModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private IAddressManager manager;
        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAddress([FromBody] UserAddressModel address)
        {
            try
            {
                UserAddressModel data = await this.manager.AddAddress(address);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Address added succesfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to add address" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAddress([FromBody] UserAddressModel address)
        {
            try
            {
                UserAddressModel data = await this.manager.UpdateAddress(address);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Address updated succesfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to update address" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
