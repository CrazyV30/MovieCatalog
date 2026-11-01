using backEnd.DTOs;
using backEnd.Models;
using backEnd.Repository;

namespace backEnd.Services
{
    public class MovieService(MovieRepository _movieRepository)
    {
        public enum MovieResult
        {
            Ok,
            TitleError,
            DescriptionError,
            UrlError,
            RuntimeError,
            DirectorError,
            NullMovieError,
            NotFoundError
        }
        public List<MovieDTO> GetAllMovies() => _movieRepository.GetAll().Select(m => new MovieDTO
        {
            Title = m.Title,
            Description = m.Description,
            PhotoPath = m.PhotoPath,
            Runtime = m.Runtime,
            Director = m.Director
        }).ToList();
        public MovieDTO? GetMovieByName(string name)
        {
            var movie = _movieRepository.GetByName(name);
            if (movie == null) return null;
            return new MovieDTO
            {
                Title = movie.Title,
                Description = movie.Description,
                PhotoPath = movie.PhotoPath,
                Runtime = movie.Runtime,
                Director = movie.Director
            };
        }
        public MovieResult AddMovie(MovieDTO movieDTO)
        {
            if(string.IsNullOrEmpty(movieDTO.Title)) return MovieResult.TitleError;
            else if (string.IsNullOrEmpty(movieDTO.Description))  return MovieResult.DescriptionError;
            else if (string.IsNullOrEmpty(movieDTO.PhotoPath)) return MovieResult.UrlError;
            else if (string.IsNullOrEmpty(movieDTO.Runtime)) return MovieResult.RuntimeError;            
            else if (string.IsNullOrEmpty(movieDTO.Director))  return MovieResult.DirectorError;
            else if (_movieRepository.GetByName(movieDTO.Title) != null) return MovieResult.TitleError;
            else if (movieDTO == null) return MovieResult.NullMovieError;

            var movie = new Movie
            {
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                PhotoPath = movieDTO.PhotoPath,
                Runtime = movieDTO.Runtime,
                Director = movieDTO.Director
            };
            _movieRepository.Add(movie);
            return MovieResult.Ok;
        }
        public MovieResult DeleteMovie(MovieDTO movieDTO)
        {
            var movie = _movieRepository.GetByName(movieDTO.Title);
            _movieRepository.Remove(movie!);
            return MovieResult.Ok;
        }

    }
}
