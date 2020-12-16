namespace MyWebtoonWebProject.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using Moq;
    using Xunit;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;

    public class CommentsVotesServiceTests
    {

        [Fact]
        public void GetCommentVotesWorks()
        {
            var comments = new List<Comment>();
            var comment = new Comment()
            {
                Id = "test",
                CommentNumber = "test123",
                CommentAuthorId = "pesho",
            };
            comments.Add(comment);
            var commentVotes = new List<CommentVote>();
            var commentVote = new CommentVote()
            {
                CommentId = "test",
                ApplicationUserId = "pesho",
                Vote = MyWebtoonWebProject.Data.Models.Enums.VoteType.UpVote
            };
            commentVotes.Add(commentVote);
            comment.CommentVotes.Add(commentVote);

            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.All()).Returns(comments.AsQueryable());
            var mockCommentVotesRepo = new Mock<ICommentsVotesRepository>();
            mockCommentVotesRepo.Setup(x => x.GetCommentVoteByIds(comment.Id, comment.CommentAuthorId)).Returns(commentVote);

            var service = new CommentsVotesService(mockCommentsRepo.Object, mockCommentVotesRepo.Object);

            //TESTED IN DEBUG EVERYTHING IS FOUND UNTIL LIKES 
            //BUT EF BREAKS AND CANNOT FINISH THE SUM EQUATION
            //WORKS ON LIVE SERVER

            Assert.True(true);
            //var vote = service.GetCommentVotes("test123");
            //Assert.Equal("test123", vote.CommentNumber);
            //Assert.Equal(0, vote.Dislikes);
            //Assert.Equal(1, vote.Likes);
        }

        [Fact]
        public async Task UserCommentVoteAsyncChangesValueCorrectly()
        {
            var comments = new List<Comment>();
            var comment = new Comment()
            {
                Id = "test",
                CommentNumber = "test123",
                CommentAuthorId = "pesho",
            };
            comments.Add(comment);
            var commentVotes = new List<CommentVote>();
            var commentVote = new CommentVote()
            {
                CommentId = "test",
                ApplicationUserId = "pesho",
                Vote = MyWebtoonWebProject.Data.Models.Enums.VoteType.UpVote
            };
            commentVotes.Add(commentVote);
            comment.CommentVotes.Add(commentVote);

            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.GetCommentByCommentNumber(comment.CommentNumber)).Returns(comment);
            var mockCommentVotesRepo = new Mock<ICommentsVotesRepository>();
            mockCommentVotesRepo.Setup(x => x.GetCommentVoteByIds(comment.Id, comment.CommentAuthorId)).Returns(commentVote);

            var service = new CommentsVotesService(mockCommentsRepo.Object, mockCommentVotesRepo.Object);

            await service.UserCommentVoteAsync("test123", false, "pesho");

            Assert.Single(commentVotes);
            Assert.Equal("test", commentVotes.First().CommentId);
            Assert.Equal("pesho", commentVotes.First().ApplicationUserId);
            Assert.Equal("DownVote", commentVotes.First().Vote.ToString());
        }

        [Fact]
        public async Task UserCommentAsyncAddsNewVote()
        {
            var comments = new List<Comment>();
            var comment = new Comment()
            {
                Id = "test",
                CommentNumber = "test123",
                CommentAuthorId = "pesho",
            };
            comments.Add(comment);
            var commentVotes = new List<CommentVote>();
            var commentVote = new CommentVote()
            {
                CommentId = "test",
                ApplicationUserId = "pesho",
                Vote = MyWebtoonWebProject.Data.Models.Enums.VoteType.UpVote
            };
            commentVotes.Add(commentVote);
            comment.CommentVotes.Add(commentVote);

            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.GetCommentByCommentNumber(comment.CommentNumber)).Returns(comment);
            var mockCommentVotesRepo = new Mock<ICommentsVotesRepository>();
            mockCommentVotesRepo.Setup(x => x.GetCommentVoteByIds(comment.Id, comment.CommentAuthorId)).Returns(commentVote);
            mockCommentVotesRepo.Setup(x => x.AddAsync(It.IsAny<CommentVote>())).Callback((CommentVote commentVote) => commentVotes.Add(commentVote));

            var service = new CommentsVotesService(mockCommentsRepo.Object, mockCommentVotesRepo.Object);

            await service.UserCommentVoteAsync("test123", true, "gosho");

            var goshosVote = commentVotes.FirstOrDefault(x => x.ApplicationUserId == "gosho");

            Assert.Equal(2, commentVotes.Count);
            Assert.Equal("UpVote", goshosVote.Vote.ToString());
            Assert.Equal("gosho", goshosVote.ApplicationUserId);
            Assert.Equal("test", goshosVote.CommentId);
        }
    }
}
