﻿namespace MyWebtoonWebProject.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Comments;

    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> CreateComment(CommentInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.commentsService.CreateCommentAsync(input, userId);
            return true;
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteComment(CommentDeleteInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.commentsService.DeleteCommentAsync(input.CommentNumber, userId);
            return true;
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<bool>> EditComment(CommentEditInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.commentsService.EditCommentAsync(input.CommentNumber, userId, input.CommentInfo);
            return true;
        }
    }
}
