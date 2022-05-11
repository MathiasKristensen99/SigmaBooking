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
        private readonly ITableService _tableService;

        public TableLayoutsController(ITableLayoutService service, ITableService tableService)
        {
            _tableLayoutService = service;
            _tableService = tableService;
        }

        [HttpPost]
        public ActionResult<CreateTableLayoutDto> CreateTableLayout(CreateTableLayoutDto dto)
        {
            var tables = new List<Table>();
            
            foreach (var table in dto.Tables)
            {
                tables.Add(_tableService.CreateTable(new Table
                {
                    Static = table.Static,
                    X = table.X,
                    Y = table.Y,
                    W = table.W,
                    H = table.H,
                    I = table.I
                }));
            }
            
            var tableLayoutFromDto = new TableLayout
            {
                IsDefault = dto.IsDefault,
                Date = dto.Date,
                Tables = tables
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

        [HttpGet("{date}")]
        public ActionResult<GetTableLayoutDto> GetTableLayout(string date)
        {
            try
            {
                var tableLayout = _tableLayoutService.GetTableLayout(date);

                var tables = tableLayout.Tables.Select(table => new TableDto
                    {
                        Id = table.Id,
                        Static = table.Static,
                        X = table.X,
                        Y = table.Y,
                        W = table.W,
                        H = table.H,
                        I = table.I
                    }).ToList();
                
                return Ok(new GetTableLayoutDto
                {
                    Id = tableLayout.Id,
                    IsDefault = tableLayout.IsDefault,
                    Date = tableLayout.Date,
                    Tables = tables
                });

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
