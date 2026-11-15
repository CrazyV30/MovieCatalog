using Bll.DTOs;
using Domain.Models;
using Dll.Repository;
using AutoMapper;

namespace Bll.Services
{
    public class MovieService(MovieRepository _movieRepository, IMapper _mapper)
    {
        public enum MovieResult
        {
            Ok,
            NullMovieError,
            NotFoundError
        }
        public List<MovieDTO> GetAllMovies() => _movieRepository.GetAll().Select(m => _mapper.Map<MovieDTO>(m)).ToList();
        public MovieDTO? GetMovieByName(string name)
        {
            var movie = _movieRepository.GetByName(name);
            if (movie == null) return null;
            return _mapper.Map<MovieDTO>(movie);
        }
        public MovieDTO? GetMovieById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null) return null;
            var dto = _mapper.Map<MovieDTO>(movie);
            dto.Comments = movie.Comments.Select(c => _mapper.Map<CommentDTO>(c)).ToList();
            return dto;
        }
        public MovieResult AddMovie(MovieDTO movieDTO)
        {
            if (movieDTO == null) return MovieResult.NullMovieError;

            var movie = _mapper.Map<Movie>(movieDTO);
            movie.Comments = new List<MovieComment>();
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
            var upd = _mapper.Map<Movie>(movieDTO);
            upd.Id = movie.Id;
            _movieRepository.Update(upd);
            return MovieResult.Ok;
        }

    }
}
