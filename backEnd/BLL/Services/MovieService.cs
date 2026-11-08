using Bll.DTOs;
using Domain.Models;
using Dll.Repository;

namespace Bll.Services
{
    public class MovieService(MovieRepository _movieRepository)
    {
        public enum MovieResult
        {
            Ok,
            NullMovieError,
            NotFoundError
        }
        public List<MovieDTO> GetAllMovies() => _movieRepository.GetAll().Select(m => new MovieDTO
        {            
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            PhotoPath = m.PhotoPath,
            Runtime = m.Runtime,
            Director = m.Director,
            ReleaseYear = m.ReleaseYear,
            MoviePath = m.MoviePath
        }).ToList();
        public MovieDTO? GetMovieByName(string name)
        {
            var movie = _movieRepository.GetByName(name);
            if (movie == null) return null;
            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                PhotoPath = movie.PhotoPath,
                Runtime = movie.Runtime,
                Director = movie.Director,
                ReleaseYear = movie.ReleaseYear,
                MoviePath = movie.MoviePath
            };
        }
        public MovieDTO? GetMovieById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null) return null;
            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                PhotoPath = movie.PhotoPath,
                Runtime = movie.Runtime,
                Director = movie.Director,
                ReleaseYear = movie.ReleaseYear,
                MoviePath = movie.MoviePath
            };
        }
        public MovieResult AddMovie(MovieDTO movieDTO)
        {
            if (movieDTO == null) return MovieResult.NullMovieError;

            var movie = new Movie
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                PhotoPath = movieDTO.PhotoPath,
                Runtime = movieDTO.Runtime,
                Director = movieDTO.Director,
                ReleaseYear = movieDTO.ReleaseYear,
                MoviePath = movieDTO.MoviePath
            };
            _movieRepository.Add(movie);
            return MovieResult.Ok;
        }
        public MovieResult DeleteMovie(MovieDTO movieDTO)
        {
            var movie = _movieRepository.GetById(movieDTO.Id);
            _movieRepository.Remove(movie!);
            return MovieResult.Ok;
        }
        public MovieResult UpdateMovie(MovieDTO movieDTO)
        {
            var movie = _movieRepository.GetById(movieDTO.Id);
            if (movie == null) return MovieResult.NotFoundError;
            movie.Title = movieDTO.Title;
            movie.Description = movieDTO.Description;
            movie.PhotoPath = movieDTO.PhotoPath;
            movie.Runtime = movieDTO.Runtime;
            movie.Director = movieDTO.Director;
            movie.ReleaseYear = movieDTO.ReleaseYear;
            movie.MoviePath = movieDTO.MoviePath;
            _movieRepository.Update(movie);
            return MovieResult.Ok;
        }

    }
}
