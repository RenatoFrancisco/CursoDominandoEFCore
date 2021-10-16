using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.Data;
using src.Data.Repositories;
using src.Domain;

namespace EFCore.UowRepsitory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentosController : ControllerBase
    {
        private readonly ILogger<DepartamentosController> _logger;
        // private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IUnitOfWork _uow;


        public DepartamentosController(ILogger<DepartamentosController> logger,
                                    //    IDepartamentoRepository departamentoRepository,
                                       IUnitOfWork uow)
        {
            _logger = logger;
            // _departamentoRepository = departamentoRepository;
            _uow = uow;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var departamento = await _uow.DepartamentoRepository.GetByIdAsync(id);

            return Ok(departamento);
        }

        [HttpPost]
        public ActionResult CreateDepartamento(Departamento departamento)
        {
            _uow.DepartamentoRepository.Add(departamento);
            // var saved = _departamentoRepository.Save();
            _uow.Commit();

            return Ok(departamento);
        }
    }
}
