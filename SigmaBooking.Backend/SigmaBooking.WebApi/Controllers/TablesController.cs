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
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService service)
        {
            _tableService = service;
        }

        [HttpPost]
        public ActionResult<TableDto> CreateTable([FromBody] TableDto dto)
        {
            var tableFromDto = new Table
            {
                Static = dto.Static,
                X = dto.X,
                Y = dto.Y,
                W = dto.W,
                H = dto.H,
                I = dto.I
            };

            try
            {
                var newTable = _tableService.CreateTable(tableFromDto);
                return Created($"https://localhost:7026/api/tables/{newTable.Id}", newTable);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        [HttpGet]
        public TableDto[] GetAllTables()
        {
            var tables = _tableService.GetAllTables().Select(table => new TableDto
            {
                Id = table.Id,
                Static = table.Static,
                X = table.X,
                Y = table.Y,
                W = table.W,
                H = table.H,
                I = table.I
            }).ToArray();

            return tables;
        }
    }
}
