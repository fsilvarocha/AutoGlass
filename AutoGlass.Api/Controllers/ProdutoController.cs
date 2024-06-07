using AutoGlass.Core.Application.Abstractions;
using AutoGlass.Core.Application.DTOs;
using AutoGlass.Core.Application.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AutoGlass.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService productService) => _produtoService = productService;

        [HttpGet("api/v1/{produtoId}")]
        public async Task<IActionResult> GetId(int produtoId)
        {
            try
            {
                var existing = await _produtoService.GetId(produtoId);

                return Ok(existing);

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("api/v1/produtos/")]
        public IActionResult Get()
        {
            try
            {
                var list = _produtoService.GetAll();

                return Ok(new { data = list });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/v1/produtos")]
        public async Task<IActionResult> Create(ProdutoRequest model)
        {
            if (await _produtoService.Add(model))
                return CreatedAtAction(nameof(Create), model);
            return BadRequest(new Result(400, $"Error", false, model));

        }

        [HttpPut("produtos/{produtoId:int}")]
        public async Task<IActionResult> Update(int produtoId, ProdutoRequest model)
        {
            try
            {
                await _produtoService.Update(produtoId, model);

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete("produtos/{produtoId:int}")]
        public async Task<IActionResult> Delete(int produtoId)
        {

            try
            {
                var existing = await _produtoService.Delete(produtoId);
                return NoContent();

            }
            catch (DbException)
            {
                return StatusCode(500);

            }

        }
    }
}
