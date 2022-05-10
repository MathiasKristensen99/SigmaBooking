using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.WebApi.Dtos;

namespace SigmaBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableLayoutsController : ControllerBase
    {
        private readonly ITableLayoutService _tableLayoutService;

        public TableLayoutsController(ITableLayoutService service)
        {
            _tableLayoutService = service;
        }

        [HttpPost]
        public ActionResult<CreateTableLayoutDto> CreateTableLayout(CreateTableLayoutDto dto)
        {
            var tableLayoutFromDto = new TableLayout
            {
                IsDefault = dto.IsDefault,
                Date = dto.Date,
                TableIds = dto.TableIds
            };

            try
            {
                var newTableLayout = _tableLayoutService.CreateTableLayout(tableLayoutFromDto);
                return Created($"https://localhost:7026/api/TableLayouts/{newTableLayout.Id}", newTableLayout);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}
