/*
 * Copyright (c) 2019 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Models;
using Piranha.Manager.Services;

namespace Piranha.Manager.Controllers
{
    /// <summary>
    /// Api controller for alias management.
    /// </summary>
    [Area("Manager")]
    [Route("manager/api/comment")]
    [Authorize(Policy = Permission.Admin)]
    [ApiController]
    public class CommentApiController : Controller
    {
        private readonly CommentService _service;
        private readonly ManagerLocalizer _localizer;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CommentApiController(CommentService service, ManagerLocalizer localizer)
        {
            _service = service;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets the list model.
        /// </summary>
        /// <returns>The list model</returns>
        [Route("")]
        [HttpGet]
        //[Authorize(Policy = Permission.Config)]
        public Task<CommentListModel> List()
        {
            return _service.Get();
        }

        [Route("approve/{id}")]
        [HttpGet]
        public async Task<CommentListModel> Approve(Guid id)
        {
            await _service.ApproveAsync(id);

            var result = await List();

            result.Status = new StatusMessage
            {
                Type = StatusMessage.Success,
                Body = "The comment was successfully approved"
            };
            return result;
        }

        [Route("unapprove/{id}")]
        [HttpGet]
        public async Task<CommentListModel> UnApprove(Guid id)
        {
            await _service.UnApproveAsync(id);

            var result = await List();

            result.Status = new StatusMessage
            {
                Type = StatusMessage.Success,
                Body = "The comment was successfully unapproved"
            };
            return result;
        }
    }
}