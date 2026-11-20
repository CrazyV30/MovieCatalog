using AutoMapper;
using Bll.DTOs;
using BLL.DTOs;
using Dll.Repository;
using Domain.Models;
using static System.Net.WebRequestMethods;

namespace Bll.Services
{
    public class MovieService(MovieRepository _movieRepository, IMapper _mapper)
    {
        public async Task<List<MovieDTO>> GetAllMovies() => (await _movieRepository.GetAll()).Select(m => _mapper.Map<MovieDTO>(m)).ToList();
        public async Task<MovieDTO?> GetMovieByName(string name)
        {
            var movie = await _movieRepository.GetByName(name);
            if (movie == null) return null;
            return _mapper.Map<MovieDTO>(movie);
        }
        public async Task<MovieDTO?> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return null;
            var dto = _mapper.Map<MovieDTO>(movie);
            dto.Comments = movie.Comments.Select(c => _mapper.Map<CommentDTO>(c)).ToList();
            return dto;
        }
        public async Task<bool> AddMovie(MovieDTO movieDTO)
        {
            if (movieDTO == null) return false;

            var movie = _mapper.Map<Movie>(movieDTO);
            movie.Comments = new List<MovieComment>();
            await _movieRepository.Add(movie);
            await _movieRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteMovie(MovieDTO movieDTO)
        {
            var movie = await _movieRepository.GetById(movieDTO.Id);
            _movieRepository.Remove(movie!);
            await _movieRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateMovie(MovieDTO movieDTO)
        {
            if (!(await _movieRepository.ExistId(movieDTO.Id))) return false;
            var upd = _mapper.Map<Movie>(movieDTO);
            upd.Id = movieDTO.Id;
            await _movieRepository.Update(upd);
            await _movieRepository.SaveChangesAsync();
            return true;
        }
        public async Task<List<MovieDTO>> SearchByFilter(SearchFilterDTO searchFilter)
        {
            if (!string.IsNullOrEmpty(searchFilter.Title)) return _mapper.Map<List<MovieDTO>>(await _movieRepository.SearchBCondition(x => x.Title.ToLower().Contains(searchFilter.Title.ToLower())));
            if (!string.IsNullOrEmpty(searchFilter.Genre)) return _mapper.Map<List<MovieDTO>>(await _movieRepository.SearchBCondition(x => string.Equals(x.Genre.ToLower(), searchFilter.Genre.ToLower())));
            if (!string.IsNullOrEmpty(searchFilter.Country)) return _mapper.Map<List<MovieDTO>>(await _movieRepository.SearchBCondition(x => string.Equals(x.Country.ToLower(), searchFilter.Country.ToLower())));
            if (searchFilter.Id != null) return _mapper.Map<List<MovieDTO>>(await _movieRepository.SearchBCondition(x => x.Id == searchFilter.Id));
            if (searchFilter.Year!= null) return _mapper.Map<List<MovieDTO>>(await _movieRepository.SearchBCondition(x => x.ReleaseYear == searchFilter.Year));
            if (!string.IsNullOrEmpty(searchFilter.KeyWord)) return _mapper.Map<List<MovieDTO>>(await _movieRepository.SearchBCondition(x => x.Title.ToLower().Contains(searchFilter.KeyWord.ToLower()) || x.Description.ToLower().Contains(searchFilter.KeyWord.ToLower())));
            return await GetAllMovies();
        }
    }
}
