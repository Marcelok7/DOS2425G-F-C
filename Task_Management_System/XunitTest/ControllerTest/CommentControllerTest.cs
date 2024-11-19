using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Controller;
using TMS.Models;
using static System.Net.Mime.MediaTypeNames;

namespace XunitTest.ControllerTest
{
    public class CommentControllerTest
    {
        /* ======================================================================== GET COMMENT ======================================================================== */
        [Fact]
        public void GetComment_CommentNotFound_ReturnsNotFound()
        {
            //Dados
            var commentController = new CommentController();
            int impossibleID = 9999;

            //Despoletar método
            var getComment = commentController.GetComment(impossibleID);

            //Verifica se retorna resultado não encontrado
            Assert.IsType<NotFoundResult>(getComment);
        }

        [Fact]
        public void GetComment_CommentFound_ReturnsOk()
        {
            //Dados
            var commentController = new CommentController();
            int existentID = 1;

            //Despoletar método
            var getComment = commentController.GetComment(existentID);

            //Verifica se retornou OK
            var okResult = Assert.IsType<OkObjectResult>(getComment);

            //Verifica o comentário retornado
            var returnedComment = Assert.IsType<Comment>(okResult.Value);

            //Verifica se tudo foi atualizado corretamente
            Assert.Equal(existentID, returnedComment.Id);
            Assert.Equal("Comentário ticket JS-1203", returnedComment.Text);
        }

        /* ======================================================================== CREATE COMMENT ======================================================================== */
        [Fact]
        public void CreateComment_DuplicateId_ReturnsBadRequest()
        {
            //Dados
            var commentController = new CommentController();
            var duplicatedComment = new Comment { Id = 1, Text = "Comentário ticket JS-1203" };

            //Despoletar método
            var createComment = commentController.CreateComment(duplicatedComment);

            //Verifica se retorna BadRequest
            Assert.IsType<BadRequestResult>(createComment);
        }

        [Fact]
        public void CreateComment_ValidComment_ReturnsCreatedAtAction()
        {
            //Dados
            var commentController = new CommentController();
            var newComment = new Comment { Text = "Commentário novo" };

            //Despoletar métodos
            var result = commentController.CreateComment(newComment);

            //Verifica se o resultado foi criado
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            
            //Verifica se retornou um comentário
            var returnedComment = Assert.IsType<Comment>(createdResult.Value);

            //Verifica se os dados foram criados corretamente
            Assert.Equal("Commentário novo", returnedComment.Text);
        }


        /* ======================================================================== UPDATE COMMENT ======================================================================== */

        [Fact]
        public void UpdateComment_DifferentIds_ReturnsBadRequest()
        {
            //Dados
            var controller = new CommentController();
            var updatedComment = new Comment { Id = 2, Text = "Updated Comment" };
            int id = 1;

            //Despoletar método
            var updateComment = controller.UpdateComment(id, updatedComment);

            //Verifica se retorna BadRequest
            Assert.IsType<BadRequestResult>(updateComment);
        }

        [Fact]
        public void UpdateComment_CommentNotFound_ReturnsNotFound()
        {
            //Dados
            var controller = new CommentController();
            var updatedComment = new Comment { Id = 99, Text = "Updated Comment" };
            int id = 99;

            //Despoletar método
            var updateComment = controller.UpdateComment(id, updatedComment);

            //Verifica se retorna resultado não encontrado
            Assert.IsType<NotFoundResult>(updateComment);
        }

        [Fact]
        public void UpdateComment_ValidRequest_ReturnsOkWithUpdatedComment()
        {
            //Dados
            var controller = new CommentController();

            var existingComment = new Comment
            {
                Id = 998,
                Task = null,
                Text = "Comentário ticket JS-1203"
            };
            
            controller.CreateComment(existingComment);

            int existentId = existingComment.Id;
            var updatedComment = new Comment
            {
                Id = existentId,
                Task = null,
                Text = "Comentário ticket JS-1203 - Editado"
            };

            //Despoleta método
            var result = controller.UpdateComment(existentId, updatedComment);

            //Verifica se retornou um objeto OK
            var okResult = Assert.IsType<OkObjectResult>(result);
            
            //Verifica se foi retornado um comentário
            var returnedComment = Assert.IsType<Comment>(okResult.Value);
            
            //Verifica se o texto do comentário foi realmente atualizado
            Assert.Equal(updatedComment.Text, returnedComment.Text);
        }

        /* ======================================================================== Delete COMMENT ======================================================================== */
        [Fact]
        public void DeleteComment_CommentNotFound_ReturnsNotFound()
        {
            //Dados
            var controller = new CommentController();
            int impossibleID = 9999;

            //Despoletar método
            var deleteComment = controller.DeleteComment(impossibleID);

            //Verifica se retorna resultado não encontrado
            Assert.IsType<NotFoundResult>(deleteComment);
        }

        [Fact]
        public void DeleteComment_ValidId_RemovesCommentAndReturnsNoContent()
        {
            //Dados
            var controller = new CommentController();
            int validId = 1;

            //Despoletar método
            var deleteComment = controller.DeleteComment(validId);

            //Verifica se não foi retornado nada
            Assert.IsType<NoContentResult>(deleteComment);
        }
    }
}
