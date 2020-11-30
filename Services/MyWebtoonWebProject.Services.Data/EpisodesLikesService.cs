namespace MyWebtoonWebProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;

    public class EpisodesLikesService : IEpisodesLikesService
    {
        private readonly IEpisodesLikesRepository episodesLikesRepository;

        public EpisodesLikesService(IEpisodesLikesRepository episodesLikesRepository)
        {
            this.episodesLikesRepository = episodesLikesRepository;
        }

        public int GetEpisodeLikes(string episodeId)
        {
            return this.episodesLikesRepository.All()
                .Where(e => e.EpisodeId == episodeId)
                .Sum(el => el.HasLiked ? 1 : 0);
        }

        public async Task UserLike(string episodeId, string userId)
        {
            var userLike = this.episodesLikesRepository.All().FirstOrDefault(e => e.EpisodeId == episodeId && e.ApplicationUserId == userId);
            if (userLike == null)
            {
                var like = new EpisodeLike
                {
                    EpisodeId = episodeId,
                    ApplicationUserId = userId,
                    HasLiked = true,
                };

                await this.episodesLikesRepository.AddAsync(like);
            }
            else
            {
                if (userLike.HasLiked == false)
                {
                    userLike.HasLiked = true;
                }
                else
                {
                    userLike.HasLiked = false;
                }
            }

            await this.episodesLikesRepository.SaveChangesAsync();
        }
    }
}
