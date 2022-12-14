using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Application;
using PetClinic.Application.Dtos;
using PetClinic.Application.Interfaces;
using PetClinic.Domain.Common.Interfaces;
using PetClinic.Infrastructure.Persistence;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace PetClinic.Api.Controllers
{
    [ApiController]
    [Route("api/owners")]
    public class OwnerRestController : ControllerBase
    {
        private readonly IAddPetService _appService;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerRestController(IAddPetService appService,
            IUnitOfWork unitOfWork)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified List&lt;OwnerDTO&gt;.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<OwnerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<List<OwnerDTO>>> getOwners(CancellationToken cancellationToken)
        {
            var result = default(List<OwnerDTO>);

            result = await _appService.GetOwners();

            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="201">Successfully created.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> addOwner([FromBody] OwnerCreateDTO dto, CancellationToken cancellationToken)
        {

            await _appService.AddOwner(dto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Created(string.Empty, null);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified OwnerDTO.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">Can't find an OwnerDTO with the parameters provided.</response>
        [HttpGet("{ownerId}")]
        [ProducesResponseType(typeof(OwnerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<OwnerDTO>> getOwner([FromRoute] int ownerId, CancellationToken cancellationToken)
        {
            var result = default(OwnerDTO);

            result = await _appService.GetOwner(ownerId);

            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="204">Successfully updated.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpPut("{ownerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> updateOwner([FromRoute] int ownerId, [FromBody] OwnerUpdateDTO dto, CancellationToken cancellationToken)
        {

            await _appService.UpdateOwner(ownerId, dto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Successfully deleted.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpDelete("{ownerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> deleteOwner([FromRoute] int ownerId, CancellationToken cancellationToken)
        {

            await _appService.DeleteOwner(ownerId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified List&lt;OwnerDTO&gt;.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpGet("*/lastname/{lastName}")]
        [ProducesResponseType(typeof(List<OwnerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<List<OwnerDTO>>> getOwnersList([FromRoute] string lastName, CancellationToken cancellationToken)
        {
            var result = default(List<OwnerDTO>);

            result = await _appService.GetOwnersList(lastName);

            return Ok(result);
        }


    }
}