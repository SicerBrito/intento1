using API.Controllers.Generic;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class CandidatoController : BaseApiController {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        public CandidatoController(IUnitOfWork unitOfWork, IMapper mapper) {

            _UnitOfWork = unitOfWork;
            _Mapper = mapper;

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CandidatoDto>>> Get(){
            var records = await _UnitOfWork.Candidatos!.GetAllAsync();
            return _Mapper.Map<List<CandidatoDto>>(records);
        }

        [HttpGet("pager")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pager<CandidatoDto>>> Get11([FromQuery] Params recordParams)
        {
            var record = await _UnitOfWork.Candidatos!.GetAllAsync(recordParams.PageIndex,recordParams.PageSize,recordParams.Search);
            var lstrecordsDto = _Mapper.Map<List<CandidatoDto>>(record.registros);
            return new Pager<CandidatoDto>(lstrecordsDto,record.totalRegistros,recordParams.PageIndex,recordParams.PageSize,recordParams.Search);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CandidatoDto>> Get(int id)
        {
            var record = await _UnitOfWork.Candidatos!.GetByIdAsync(id);
            if (record == null){
                return NotFound();
            }
            return _Mapper.Map<CandidatoDto>(record);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Candidato>> Post(CandidatoDto recordDto)
        {    
            if (recordDto == null)
            {
                return BadRequest();
            }
            var candidato = _Mapper.Map<Candidato>(recordDto);
            _UnitOfWork.Candidatos!.Add(candidato);
            await _UnitOfWork.SaveAsync();
            var createdRecordDto = _Mapper.Map<CandidatoDto>(candidato);
            return CreatedAtAction(nameof(Post), createdRecordDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CandidatoDto>> Put(string id, [FromBody]CandidatoDto recordDto){
            if(recordDto == null)
                return NotFound();
            var records = _Mapper.Map<Candidato>(recordDto);
            _UnitOfWork.Candidatos!.Update(records);
            await _UnitOfWork.SaveAsync();
            return recordDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id){
            try
            {
                var record = await _UnitOfWork.Candidatos!.GetByIdAsync(id);
                if(record == null){
                    return NotFound();
                }
                _UnitOfWork.Candidatos.Remove(record);
                await _UnitOfWork.SaveAsync();
                return StatusCode(StatusCodes.Status200OK, "Se ha borrado exitosamente");
            }
            catch (Exception)
            {
                // Manejo de excepciones
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor o no esta autorizado este servicio");
            }

        }

        // Consulta #1 Endpoint para buscar a los candidatos por ciudad
        [HttpGet("por-ciudad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Candidato>> ObtenerPerfilesPorPais([FromQuery] string ciudad)
        {
            var candidatos = _UnitOfWork.Candidatos!.ObtenerPerfilesPorCiudad(ciudad);
            if (candidatos == null || candidatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(candidatos);
        }

        // Consulta #2 Endpoint para buscar candidatos por nombre
        [HttpGet("buscar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Candidato>> BuscarPorNombre([FromQuery] string nombre)
        {
            var candidatos = _UnitOfWork.Candidatos!.BuscarPorNombre(nombre);
            if (candidatos == null || candidatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(candidatos);
        }

        // Consulta #3 Endpoint para buscar candidatos por género
        [HttpGet("por-genero")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Candidato>> BuscarPorGenero([FromQuery] string genero)
        {
            var candidatos = _UnitOfWork.Candidatos!.BuscarPorGenero(genero);
            if (candidatos == null || candidatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(candidatos);
        }

        // Consulta #4 Endpoint para buscar candidatos por experiencia
        [HttpGet("por-experiencia")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Candidato>> BuscarPorExperiencia([FromQuery] int experienciaMinima)
        {
            var candidatos = _UnitOfWork.Candidatos!.BuscarPorExperiencia(experienciaMinima);
            if (candidatos == null || candidatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(candidatos);
        }


        // Consulta #5 Endpoint para obtener la cantidad de candidatos en la base de datos
        [HttpGet("cantidad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> ObtenerCantidadCandidatos()
        {
            var cantidadCandidatos = _UnitOfWork.Candidatos!.ObtenerCantidadCandidatos();
            return Ok(cantidadCandidatos);
        }


        // Consulta #6 Endpoint para filtrar candidatos por edad
        [HttpGet("filtro-por-edad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<CandidatoDto>> FiltrarPorEdad(int edadMinima, int edadMaxima)
        {
            if (edadMinima < 0 || edadMaxima < 0 || edadMinima > edadMaxima)
            {
                return BadRequest("Edades inválidas");
            }

            var candidatos = _UnitOfWork.Candidatos!.FiltrarPorEdad(edadMinima, edadMaxima);
            var candidatosDto = _Mapper.Map<IEnumerable<CandidatoDto>>(candidatos);
            
            return Ok(candidatosDto);
        }

    }
